using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public abstract class Character : MonoBehaviour, IDamagable
{
    public UnityAction onHpChange;
    public UnityAction onDeath;

    new public string name;
    public Sprite icon;

    [Header("Spec")]
    public float curHp;
    public float maxHp;
    public float curDamage{ 
        get{ 
            if (IsCritical()) 
                return Random.Range(minDamage, maxDamage) * (1f + 0.01f * criticalRate);
            return Random.Range(minDamage, maxDamage);
        }
    }
    public float minDamage;
    public float maxDamage;
    [HideInInspector]
    public float lastAttackTime;
    public float attackDelay;
    public float attackRange;                   // 사정거리
    public float aoeRange;                      // 범위공격
    public float moveSpeed = 3f;

    public float criticalChance = 10f;
    public float dodgeChance = 1f;

    public float criticalRate = 50f;            // 치명타 피해량 (%)
    public float powerRate = 0f;                // 추가 공격피해량 (%)
    public float magicPowerRate = 0f;           // 추가 마법피해량 (%)
    public float armorRate = 0f;                // 방어율 (%)
    public float magicArmorRate = 0f;           // 마법방어율 (%)
   
    [Header("Transform")]
    public Transform targetTF;                  // 발사체 도착 위치(맞을 위치)
    
    //[HideInInspector]
    public Character target;      
    [HideInInspector]   public NavMeshAgent nav;

    [Header("Macro")]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;

    [Header("Projectile")]
    public Projectile projectile;
    public Transform projectileTF;      // 투사체 생성 위치

    [Header("Skill")]
    public Skill[] skills;

    [HideInInspector] public AttackCommand attackCommand;
    [HideInInspector] public Animator anim;
    [HideInInspector] public bool isStop;    
    [HideInInspector] public bool isDead;
    int getDamage;

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

    public void Pause(float duration) { Invoke("Pause", duration); }
    public void Resume(float duration) { Invoke("Resume", duration); }

    protected void Awake() { InitCharacter(); }

    void InitCharacter(){
        // 이벤트 함수등록
        DungeonManager.instance.onWaveEnd += Pause;
        // get컴포넌트
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        // 컴포넌트 설정
        nav.enabled = false;
        nav.speed = moveSpeed;
        nav.stoppingDistance = attackRange;
        // 매크로 배열 초기화
        conditionMacros = new ConditionMacro[MacroManager.instance.maxMacroCount];
        actionMacros = new ActionMacro[MacroManager.instance.maxMacroCount];
    }

    public void Damaged(float damage, float damageRate, bool isMagic = false)
    {                // 공격자의 공격력, 공격자의 공격력 증가량, 물리마법공격 구분
        if (isMagic)
        {
            getDamage = Mathf.RoundToInt(damage * 0.01f * (100f + damageRate - magicArmorRate));
            curHp -= getDamage;
            onHpChange?.Invoke();
            DungeonManager.instance.onChangeAnyHP?.Invoke();
            Debug.Log(name + "가 " + getDamage + " 마법피해 입음");
        }
        else {
            if (IsDodge()) { 
                // TODO 회피효과 출력
                Debug.Log(name + "가 회피");
            } 
            else{
                getDamage = Mathf.RoundToInt(damage * 0.01f * (100f + damageRate - armorRate));
                curHp -= getDamage;
                onHpChange?.Invoke();
                DungeonManager.instance.onChangeAnyHP?.Invoke();
                Debug.Log(name + "가 " + getDamage + " 물리피해 입음");
            }
        }
        ShowDamageText();
        if (curHp <= 0) Death();
    }

    void ShowDamageText(){
        GameManager.instance.ShowBattleInfoText(
            BattleInfoType.Ally_Damage, //
            transform.position + Vector3.up * 5f,
            getDamage
        );
    }

    public void Healed(float heal)
    {
        curHp += heal;
        if (curHp > maxHp) curHp = maxHp;
        DungeonManager.instance.onChangeAnyHP?.Invoke();
    }

    public abstract void Death();

    public void Revive(float rateHp)
    {
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

    // public void LaunchProjectile()
    // {   
    //     if (target == null) return;
    //     //Projectile proj = Instantiate(projectile, projectileTF.position, Quaternion.LookRotation(targetTF.position));
    //     //proj.Launch(target, curDamage, powerRate, aoeRange);
    //     Projectile proj = ObjectPool.instance.GetArrow();
    //     proj.transform.position = projectileTF.position;
    //     proj.Launch(target, curDamage, powerRate, aoeRange);
    // }

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
