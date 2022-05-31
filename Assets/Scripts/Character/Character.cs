using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public enum EBuff {
    None = -1,
    Armor, Damage, Speed,//Magic, MagicArmor, AttSpeed,
    Stun, Frozen,
    Size,
}
public abstract class Character : MonoBehaviour, IDamagable
{
    public UnityAction onHpChange;
    public UnityAction onDead;
    public UnityAction<Character> onDeadGetThis;
    public UnityAction<int> onMacroChangeGetIndex;
    public UnityAction<float> onAttackGetDamage;
    public UnityAction<Character> onDamagedGetAttacker;
    public UnityAction onKill;

    new public string name;
    public Sprite icon;
    [HideInInspector] public EGroup eGroup;

    [Header("Spec")]
    public float curHp;
    public float maxHp;
    public float curDamage{ 
        get{ 
            if (IsCritical()) {
                return Random.Range(minDamage, maxDamage) * criticalRate * buffDamage;
            }
            return Random.Range(minDamage, maxDamage) * buffDamage;
        }
    }
    public float minDamage;
    public float maxDamage;
    public float magicDamage;
    [HideInInspector] public float lastAttackTime;
    [Range(0.1f, 2f)] public float attackDelay;
    [Range(2f, 20f)] public float attackRange;                      // 사정거리
    [Range(0f, 10f)] public float aoeRange;                         // 범위공격
    [Range(1f, 20f)] public float moveSpeed;                        // 
    [Space(10f)]
    [Range(1f, 50f)] public float criticalChance = 10f;
    [Range(0f, 30f)] public float dodgeChance = 1f;
    [Space(10f)]
    [Range(1f, 4f)] public float criticalRate = 1.5f;               // 치명타 배율 (%)
    [Range(0f, 2f)] public float powerRate = 1f;                    // 추가 피해 (%)
    [Range(0f, 1f)] public float armorRate = 0f;                    // 방어율 (%)
    [Range(0f, 1f)] public float magicArmorRate = 0f;               // 마법방어율 (%)
   
    // buffs
    public LinkedList<Buff> buffs;              
    public float buffDamage = 1f;
    //public float buffArmor = 0f;
    float _buffArmor = 0f;
    public float buffArmor { 
        get { return _buffArmor; }
        set {
            _buffArmor = value; Debug.Log("buffArmor : " + _buffArmor + " / isDead : " + isDead);
        }
    }

    float _buffSpeed = 1f;
    public float buffSpeed {
        get { return _buffSpeed; }
        set {
            _buffSpeed = value;
            nav.speed = moveSpeed * _buffSpeed;
        }
    }
    float _buffStun = 0f;
    public float buffStun {
        get { return _buffStun; }
        set { 
            _buffStun = value; 
            if (_buffStun > 0.1f)
            {
                anim.SetBool("IsStuned", true); 
            }
            else
            {
                _buffStun = 0f;
                anim.SetBool("IsStuned", false);
            }
        }
    }
    
    
    [Header("Transform")]
    public Transform targetTF;                          // 투사체 도착 위치(맞을 위치)
    public Transform HpBarTF;                           // 체력바 위치
    public Transform projectileTF;                      // 투사체 생성 위치
    
    //[HideInInspector]
    //public Character targetForDebug;                    // 임시 디버그용
    //Character _target;
    // public Character target{
    //     get { return _target; } 
    //     set { 
    //         //if (_target != value && eGroup == EGroup.Hero) Debug.Log(string.Format("_T, value : {0}, {1}" , _target, value));
    //         _target = value;        targetForDebug = _target;
    //         if (null != _target)
    //         { _target.onDeadGetThis += TargetDead; }
    //     }
    // }      
    [HideInInspector]   public NavMeshAgent nav;

    [Header("Macro")]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;

    //public int prevMacro = -1;

    [Header("Skill")]
    public Skill[] skills;

    [HideInInspector] public AttackCommand attackCommand;
    [HideInInspector] public Animator anim;
    //[HideInInspector] 
    public bool isStop;    
    //[HideInInspector] 
    public bool isDead;
    protected int getDamage;
    protected Character attacker;
    float m_lastMoveOrderTime;
    // Vector3 defaultScale;

    bool IsDodge()      { return Random.Range(1f, 100f) <= dodgeChance; }
    bool IsCritical()   { return Random.Range(1f, 100f) <= criticalChance; }

    public void Pause() { 
        isStop = true;    
        if (nav != null) nav.enabled = false; 
    }

    public void Resume() { 
        isStop = false;  
        if (nav != null) nav.enabled = true; 
    }

    public void Pause(float duration)   { Invoke("Pause", duration); }
    public void Resume(float duration)  { Invoke("Resume", duration); }

    protected void Awake() { InitCharacter(); }

    void InitCharacter(){
        // 컴포넌트 초기화
        anim = GetComponentInChildren<Animator>();      
        nav = GetComponent<NavMeshAgent>();
        // 컴포넌트 설정
        anim.speed = 2f;
        nav.enabled = false;
        nav.speed = moveSpeed;
        nav.stoppingDistance = attackRange;
        nav.acceleration = 50f;
        nav.angularSpeed = 360f;
        // 버프 배열 초기화 
        buffs = new LinkedList<Buff>();
        
        //defaultScale = GetComponent<Transform>().localScale;
    }

    public void Damaged(float damage, float damageRate, Character newAttacker, bool isMagic = false)
    {                   // 공격자의 공격력, 공격자의 공격력 증가량, 공격자, 물리vs마법
        if (isDead) return; // 범위 공격에서 GetComponent<Character>해서 예외처리하는 것보다 이게 좋아보여서

        if (isMagic)
        {   
            getDamage = Mathf.RoundToInt(damage * damageRate * (1f - magicArmorRate  - buffArmor));
            if (getDamage < 1) getDamage = 1;
            curHp -= getDamage;

            onHpChange?.Invoke();
            onDamagedGetAttacker?.Invoke(newAttacker);
            DungeonManager.instance.onChangeAnyHP?.Invoke();
            if (newAttacker != null) newAttacker.onAttackGetDamage?.Invoke((float)getDamage);    // 원거리의 경우 죽었을 수 있어서

            Effect eff = ObjectPool.instance.GetEffect((int)EEffect.Blood);
            eff.transform.position = targetTF.position;
            eff.SetDuration(1f);

            //Debug.Log(name + "가 " + getDamage + " 마법피해 입음");
        }
        else {
            if (IsDodge()) { 
                GameManager.instance.ShowDodgeText(transform.position + Vector3.up * 5f);
            }
            else{
                getDamage = Mathf.RoundToInt(damage * damageRate * (1f - armorRate - buffArmor));
                if (getDamage < 1) getDamage = 1;
                curHp -= getDamage;

                onHpChange?.Invoke();
                onDamagedGetAttacker?.Invoke(newAttacker);
                DungeonManager.instance.onChangeAnyHP?.Invoke();
                if (newAttacker != null) newAttacker.onAttackGetDamage?.Invoke((float)getDamage);
                
                Effect eff = ObjectPool.instance.GetEffect((int)EEffect.Blood);
                eff.transform.position = targetTF.position;
                eff.SetDuration(1f);

                //Debug.Log(name + "가 " + getDamage + " 물리피해 입음");
            }
        }

        attacker = newAttacker; 
        ShowDamageText(getDamage, isMagic);
        if (curHp <= 0) {
            Death();
            if (attacker != null) attacker.onKill?.Invoke();
        }
    }


    public void Healed(float heal)
    {
        curHp += heal;
        if (curHp > maxHp) curHp = maxHp;
        onHpChange?.Invoke();
        DungeonManager.instance.onChangeAnyHP?.Invoke();
        ShowDamageText(heal, true, true);
    }
    protected abstract void ShowDamageText(float damage, bool isMagic = false, bool isHeal = false);

    public abstract void Death();

    // void TargetDead(Character character) { 
    //     character.onDeadGetThis -= TargetDead;
    //     target = null; 
    // }

    public void Revive(float rateHp) {
        isDead = false;
        curHp = 0.01f * rateHp * maxHp;
    }

    public void ResetBuffs()
    {
        if (buffs.Count == 0) return;
        
        // foreach (Buff buff in buffs)
        // {
        //     buff.Remove();
        // }
        buffs.Clear();

        buffStun = 0f;
        buffArmor = 0f;
        buffDamage = 1f;
    }

    public void AttackInit(Character target){          
        if (Time.time < lastAttackTime + attackDelay) return;
        lastAttackTime = Time.time;
        attackCommand.SetTarget(target);
        anim.SetTrigger("Attack");      

    }

    public void MoveToTarget(Character target)
    {
        if (target == null) return;
        
        if (Time.time < m_lastMoveOrderTime + 0.2f) return;

        nav.SetDestination(target.transform.position);
        if (nav.isStopped) nav.isStopped = false;
    }

    public bool IsTargetInRange(Character target, float range)
    {
        if (target == null) return false;

        return (target.transform.position - transform.position).sqrMagnitude <= range * range;

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //     private void OnMouseEnter() {
    //     Debug.Log("OnMouseEnter" + name);
    //     transform.localScale = new Vector3(3f, 3f, 3f);

    // } 

    // private void OnMouseExit() {
    //     Debug.Log("OnMouseExit" + name);
    //     transform.localScale = new Vector3(2f, 2f, 2f);
    // }  
}
