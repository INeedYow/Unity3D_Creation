﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public enum EBuff {
    None = -1,
    Armor, Damage, Speed, Power,  //Magic, , AttSpeed,
    Stun, Frozen, MagicArmor, Invincible,
    Size,
}
public abstract class Character : MonoBehaviour, IDamagable
{
    public UnityAction onHpChange;
    public UnityAction onDead;
    public UnityAction<Character> onUntouchableGetThis;
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
    public int buffCount;
    public int debuffCount;

    public float buffDamage = 1f;
    public float buffArmor = 0f;
    public float buffMagicArmor = 0f;

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
                
                if (nav.isOnNavMesh)
                {
                    nav.isStopped = true;
                    nav.velocity = Vector3.zero;
                }
                
                if (m_stunEff == null)
                {
                    m_stunEff = ObjectPool.instance.GetEffect((int)EEffect.Stun);
                    m_stunEff.SetPosition(this);
                }
            }
            else{
                _buffStun = 0f;
                anim.SetBool("IsStuned", false);

                if (nav.isOnNavMesh)
                {
                    nav.isStopped = false;
                }

                if (m_stunEff != null)
                {
                    m_stunEff.Return();
                    m_stunEff = null;
                }
               
            }
        }
    }
    Effect m_stunEff;

    float _buffFrozen = 0f;
    public float buffFrozen {
        get { return _buffFrozen; }
        set {
            _buffFrozen = value;
            if (_buffFrozen < -0.01f)
            {
                anim.speed = Mathf.Clamp(2f * (1f + _buffFrozen), 0f, 2f);
                if (anim.speed < 0.1f)
                {
                    nav.isStopped = true;
                    nav.velocity = Vector3.zero;
                }
            }
            else{
                anim.speed = 2f;
            }
        }
    }

    float _buffInvincible = 0f;
    public float buffInvincible {
        get { return _buffInvincible; }
        set
        {
            _buffInvincible = value;

            if (_buffInvincible > 0.1f)
            {
                if (!isInvincible) isInvincible = true;

                if (m_InvincibleEff == null)
                {
                    m_InvincibleEff = ObjectPool.instance.GetEffect((int)EEffect.Invincible);
                    m_InvincibleEff.SetPosition(this);
                }
            }
            else{
                if (isInvincible) isInvincible = false;

                if (m_InvincibleEff != null)
                {
                    m_InvincibleEff.Return();
                    m_InvincibleEff = null;
                }
            }
        }
    }
    Effect m_InvincibleEff;


    
    
    [Header("Transform")]
    public Transform targetTF;                          // 투사체 도착 위치(맞을 위치)
    public Transform HpBarTF;                           // 체력바 위치
    public Transform projectileTF;                      // 투사체 생성 위치
    

    [Header("Macro")]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;


    [Header("Skill")]
    public Skill[] skills;


    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public Animator anim;
    [HideInInspector] public AttackCommand attackCommand;
    //[HideInInspector] 
    public bool isStop;    
    //[HideInInspector] 
    public bool isDead;
    public Character attacker;
    protected Character provoker;                    // 도발자
    [SerializeField] protected bool isStasis;
    [SerializeField] protected bool isInvincible;
    protected int getDamage;
    float m_lastMoveOrderTime;

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
        anim.speed = 3f;
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
        if (isDead || isStop || isInvincible) return; 

        if (isMagic)
        {   
            getDamage = Mathf.RoundToInt(damage * damageRate * (1f - magicArmorRate  - buffMagicArmor));
            if (getDamage < 1) getDamage = 1;
            curHp -= getDamage;

            onHpChange?.Invoke();
            onDamagedGetAttacker?.Invoke(newAttacker);
            DungeonManager.instance.onChangeAnyHP?.Invoke();
            if (newAttacker != null) newAttacker.onAttackGetDamage?.Invoke((float)getDamage);    // 원거리의 경우 죽었을 수 있어서

            Effect blood = ObjectPool.instance.GetEffect((int)EEffect.Blood);
            blood.SetPosition(this);

            //Debug.Log(name + "가 " + getDamage + " 마법피해 입음");
        }
        else {
            if (IsDodge()) { 
                GameManager.instance.ShowDodgeText(targetTF.position + Vector3.up * 3f);
            }
            else{
                getDamage = Mathf.RoundToInt(damage * damageRate * (1f - armorRate - buffArmor));
                if (getDamage < 1) getDamage = 1;
                curHp -= getDamage;

                onHpChange?.Invoke();
                onDamagedGetAttacker?.Invoke(newAttacker);
                DungeonManager.instance.onChangeAnyHP?.Invoke();
                if (newAttacker != null) newAttacker.onAttackGetDamage?.Invoke((float)getDamage);
                
                Effect blood = ObjectPool.instance.GetEffect((int)EEffect.Blood);
                blood.SetPosition(this);

                //Debug.Log(name + "가 " + getDamage + " 물리피해 입음");
            }
        }

        if (newAttacker != null) { attacker = newAttacker; }
        
        ShowDamageText(getDamage, isMagic);

        if (curHp <= 0) 
        {
            Death();
            if (attacker != null) attacker.onKill?.Invoke();
        }
    }

    public void Damaged(float damage)
    {   // 플레이어 스킬용 Damaged
        if (isDead || isStop) return;
        if (damage <= 0f) return;

        curHp -= damage;    
        onHpChange?.Invoke();
        DungeonManager.instance.onChangeAnyHP?.Invoke();

        ShowDamageText(damage);

        if (curHp <= 0) { Death(); }
    }

    public void Healed(float heal)
    {
        if (isDead || isStop) return;

        curHp += heal;
        if (curHp > maxHp) curHp = maxHp;
        onHpChange?.Invoke();
        DungeonManager.instance.onChangeAnyHP?.Invoke();

        if (heal <= 0) return;
        ShowDamageText(heal, true, true);
    }

    // public void Effected(Effect effect, float duration = 0f)
    // {
    //     effect.transform.position = targetTF.position;
        
    //     if (duration != 0f)
    //     {
    //         effect.SetDuration(duration);
    //     }
    // }

    protected abstract void ShowDamageText(float damage, bool isMagic = false, bool isHeal = false);

    public abstract void Death();
    public abstract void Revive(float rateHp); 

    // void TargetDead(Character character) { 
    //     character.onDeadGetThis -= TargetDead;
    //     target = null; 
    // }


    public void AttackInit(Character target)
    {          
        if (Time.time < lastAttackTime + attackDelay) return;
        lastAttackTime = Time.time;
        attackCommand.SetTarget(target);
        anim.SetTrigger("Attack");      
    }

    public void MoveToTarget(Character target)
    {   
        if (target == null) return;
        
        if (Time.time < m_lastMoveOrderTime + 0.2f) return;
        
        if (anim.speed < 0.1f) return;

        if (isDead) return;

        if (!nav.isOnNavMesh) return;

        nav.SetDestination(target.transform.position);
        m_lastMoveOrderTime = Time.time;
        if (nav.isStopped) nav.isStopped = false;
    }

    public void MoveToPos(Vector3 pos)
    {
        if (Time.time < m_lastMoveOrderTime + 0.2f) return;

        if (anim.speed < 0.1f) return;

        if (isDead) return;

        nav.SetDestination(pos);
        m_lastMoveOrderTime = Time.time;
        if (nav.isStopped) nav.isStopped = false;
    }

    public bool IsTargetInRange(Character target, float range)
    {
        if (target == null) return false;

        if (nav.stoppingDistance > range)
        {
            nav.stoppingDistance = range;
        }

        return (target.transform.position - transform.position).sqrMagnitude <= range * range;

    }

    public void ResetNavDistance()
    {
        if (nav.stoppingDistance != attackRange)
        {
            nav.stoppingDistance = attackRange;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }



    // provoke
    public void SetProvoke(Character provoker, float duration)
    {
        if (this.provoker != null)
        {   // 이미 있는 경우
            CancelInvoke("FinishProvoke");
        }

        this.provoker = provoker;
        
        if (this.provoker != null)
        {
            Invoke("FinishProvoke", duration);
        }
    }

    void FinishProvoke()
    {
        provoker = null;
    }

    public bool IsProvoked()        { return provoker != null; }
    public Character GetProvoker()  { return provoker; }


    //// stasis
    public bool IsStasis() { return isStasis; }

    public void SetStasis(bool isOn, float duration = 0f)
    {
        if (isOn && !isStasis)
        {   // 정지 상태 처음 진입
            anim.StartPlayback();
            
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            isStop = true;
            isStasis = true;

            if (duration != 0f)
            {
                Invoke("FinishStasis" , duration);
            }

            // 매크로 타겟 초기화
            onUntouchableGetThis?.Invoke(this);
        }
        else if (!isOn && isStasis)
        {   // 정지 상태 해제
            anim.StopPlayback();

            nav.isStopped = false;
            isStop = false;
            isStasis = false;

            onUntouchableGetThis?.Invoke(this);
        }
    }

    public void FinishStasis()
    {
        SetStasis(false);
    }

}
