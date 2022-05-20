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
    
    [HideInInspector]   public Character target;      
    [HideInInspector]   public NavMeshAgent nav;

    [Header("Macro")]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;

    [Header("Projectile")]
    public Projectile projectile;
    public Transform projectileTF;      // 투사체 생성 위치

    [Header("Skill")]
    public Skill[] skills;

    //[HideInInspector] public AttackCommand attackCommand;
    [HideInInspector] public Animator anim;
    [HideInInspector] public bool isStop;    
    [HideInInspector] public bool isDead;
    float getDamage;

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
            getDamage = (int)(damage * 0.01f * (100f + damageRate - magicArmorRate));
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
                getDamage = (int)(damage * 0.01f * (100f + damageRate - armorRate));
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

    public virtual void Death(){
        isDead = true;
    }

    public void Revive(float rateHp)
    {
        isDead = false;
        curHp = 0.01f * rateHp * maxHp;
    }

    public void Move(Vector3 destPos)
    {   
        if(nav.velocity == Vector3.zero)
        {  
            nav.SetDestination(destPos);
        }
    }

    public void Attack(){ 
        if (Time.time < lastAttackTime + attackDelay) return;
        lastAttackTime = Time.time;
        anim.SetTrigger("Attack");
    }

    public void LaunchProjectile()
    {   
        if (target == null) return;
        Projectile proj = Instantiate(projectile, projectileTF.position, Quaternion.LookRotation(targetTF.position));
        proj.Launch(target, curDamage, powerRate, aoeRange);
    }

    public bool IsTargetInRange(float range = 0f)
    {   //Debug.Log("isTargetInRange()");
        if (target == null) return false;

        if (range == 0f)    // 파라미터 없으면 캐릭터 사거리랑 비교
        {   
            return (target.transform.position - transform.position).sqrMagnitude <= 
                attackRange * attackRange;
        }
        else{   // 입력해준 값이랑 비교
            return (target.transform.position - transform.position).sqrMagnitude <= 
                range * range;
        }
    }

    public void MoveToTarget(){
        if (target == null) return;
        if (nav.velocity == Vector3.zero){
            nav.SetDestination(target.transform.position);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
