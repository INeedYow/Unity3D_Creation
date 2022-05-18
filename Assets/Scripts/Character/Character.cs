using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 상태 이상?
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
    public Transform targetTF;          // 발사체 도착 위치(맞을 위치)
    [HideInInspector]
    public Character target;      

    [Header("Macro")]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;

    [Header("Projectile")]
    public Projectile projectile;
    public Transform projectileTF;      // 발사체 생성 위치

    [HideInInspector] public AttackCommand attackCommand;
    [HideInInspector] public Animator anim;
    [HideInInspector] public bool isStop;    
    [HideInInspector] public bool isDead;

    bool IsDodge()      { return Random.Range(1f, 100f) <= dodgeChance; }
    bool IsCritical()   { return Random.Range(1f, 100f) <= criticalChance; }

    public void Pause() { isStop = true; }
    public void Resume() { isStop = false; }

    public void Pause(float duration) { Invoke("Pause", duration); }
    public void Resume(float duration) { Invoke("Resume", duration); }

    protected void Awake() {
        DungeonManager.instance.onWaveEnd += Pause;
        anim = GetComponentInChildren<Animator>();
        conditionMacros = new ConditionMacro[MacroManager.instance.maxMacroCount];
        actionMacros = new ActionMacro[MacroManager.instance.maxMacroCount];
    }

    public void Damaged(float damage, float damageRate, bool isMagic = false)
    {                // 공격자의 공격력, 공격자의 공격력 증가량, 물리마법공격 구분
        if (isMagic)
        {
            curHp -= (int)(damage * 0.01f * (100f + damageRate - magicArmorRate));
            onHpChange?.Invoke();
            DungeonManager.instance.onChangeAnyHP?.Invoke();
        }
        else {
            if (IsDodge()) { 
                // TODO 회피효과 출력
                Debug.Log(name + "가 회피");
            } 
            else{
                curHp -= (int)(damage * 0.01f * (100f + damageRate - armorRate));
                onHpChange?.Invoke();
                DungeonManager.instance.onChangeAnyHP?.Invoke();
                Debug.Log(name + "가 " + (int)(damage * 0.01f * (100f + damageRate - armorRate)) + " 물리피해 입음");
            }
        }

        if (curHp <= 0) Death();
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

    public void Move(Transform destTransform)
    {
        gameObject.transform.LookAt(destTransform);
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void Attack(){
        if (Time.time < lastAttackTime + attackDelay) return;

        lastAttackTime = Time.time;
        anim.SetTrigger("Attack");
    }

    public void LaunchProjectile()
    {   
        if (target == null) return;
        Projectile proj = Instantiate(projectile, projectileTF.position, Quaternion.identity);
        proj.Launch(target, curDamage, powerRate, aoeRange);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
