using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour, IDamagable
{
    public UnityAction onHpChange;
    public UnityAction<Character> onDead;
    public UnityAction<int> onMacroChange;
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
            if (IsCritical()) return Random.Range(minDamage, maxDamage) * criticalRate;
            return Random.Range(minDamage, maxDamage);
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
   
    [Header("Transform")]
    public Transform targetTF;                          // 투사체 도착 위치(맞을 위치)
    public Transform projectileTF;                      // 투사체 생성 위치
    
    //[HideInInspector]
    public Character targetForDebug;                    // 임시 디버그용
    Character _target;
    public Character target{
        get { return _target; } 
        set { 
            _target = value;        targetForDebug = _target;
            if (null != _target)
            { _target.onDead += TargetDead; }
        }
    }      
    [HideInInspector]   public NavMeshAgent nav;

    [Header("Macro")]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;

    [Header("Skill")]
    public Skill[] skills;

    [HideInInspector] public AttackCommand attackCommand;
    [HideInInspector] public Animator anim;
    [HideInInspector] public bool isStop;    
    [HideInInspector] public bool isDead;
    protected int getDamage;
    protected Character attacker;

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
    }

    public void Damaged(float damage, float damageRate, Character newAttacker, bool isMagic = false)
    {                   // 공격자의 공격력, 공격자의 공격력 증가량, 공격자, 물리vs마법
        if (isMagic)
        {   Debug.Log("반사");
            getDamage = Mathf.RoundToInt(damage * damageRate * (1f - magicArmorRate));
            curHp -= getDamage;

            onHpChange?.Invoke();
            onDamagedGetAttacker?.Invoke(newAttacker);
            DungeonManager.instance.onChangeAnyHP?.Invoke();
            if (newAttacker != null) newAttacker.onAttackGetDamage?.Invoke((float)getDamage);    // 원거리의 경우 죽었을 수 있어서

            //Debug.Log(name + "가 " + getDamage + " 마법피해 입음");
        }
        else {
            if (IsDodge()) { 
                // TODO 회피효과 출력
                Debug.Log(name + "가 회피함");
            }
            else{
                getDamage = Mathf.RoundToInt(damage * damageRate * (1f - armorRate));
                curHp -= getDamage;

                onHpChange?.Invoke();
                onDamagedGetAttacker?.Invoke(newAttacker);
                DungeonManager.instance.onChangeAnyHP?.Invoke();
                if (newAttacker != null) newAttacker.onAttackGetDamage?.Invoke((float)getDamage);

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

    void TargetDead(Character character) { 
        character.onDead -= TargetDead;
        target = null; 
    }

    public void Revive(float rateHp) {
        isDead = false;
        curHp = 0.01f * rateHp * maxHp;
    }

    public void Move(Vector3 destPos)
    {   
        if(nav.velocity == Vector3.zero) 
        { nav.SetDestination(destPos); }
        if (nav.isStopped) 
        { nav.isStopped = false; }
    }

    public void AttackInit(){           // ActionMacro_NormalAttack 에서 호출
        if (Time.time < lastAttackTime + attackDelay) return;
        lastAttackTime = Time.time;
        anim.SetTrigger("Attack");      // 애니메이션에서 Event함수로 GFX의 Attack() 호출하게 됨
    }

    public bool IsTargetInRange(float range)
    {   //Debug.Log("isTargetInRange()");
        if (target == null) return false;
        //Debug.Log(string.Format("target pos : {0} // my pos : {1} // rng : {2}", target.transform.position, transform.position, range));
        return (target.transform.position - transform.position).sqrMagnitude <= range * range;
    }

    public void MoveToTarget(){
        if (target == null) return;
        if (nav.velocity == Vector3.zero)
        { nav.SetDestination(target.transform.position); }
        if (nav.isStopped) 
        { nav.isStopped = false; }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
