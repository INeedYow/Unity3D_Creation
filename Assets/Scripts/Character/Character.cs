using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태 이상?
public abstract class Character : MonoBehaviour, IDamagable
{
    new public string name;
    [Header("GFX")]
    public GameObject grx;      // 전투 중에 보여질 캐릭터

    [Header("Spec")]
    public float curHp;
    public float maxHp;
    public float curDamage{ 
        get{ 
            if (IsCritical()) 
                return Random.Range(minDamage, maxDamage) * 0.01f * (100f + criticalRate);
            return Random.Range(minDamage, maxDamage); 
        }
    }
    public float minDamage;
    public float maxDamage;
    public float lastAttackTime;
    public float attackDelay;
    public float attackRange;                   // 사정거리
    public float aoeRange;                      // 공격범위
    public float moveSpeed = 3f;

    public float criticalChance = 10f;
    public float dodgeChance = 1f;

    public float criticalRate = 50f;            // 치명타 피해량 (%)
    public float powerRate = 0f;                // 추가 공격피해량 (%)
    public float magicPowerRate = 0f;           // 추가 마법피해량 (%)
    public float armorRate = 0f;                // 방어율 (%)
    public float magicArmorRate = 0f;           // 마법방어율 (%)

    [Header("Additional")]
    public bool isBattleStart;  // TODO 전투 개시
    public Character target;
    public bool isDead;

    bool IsDodge()      { return Random.Range(1f, 100f) <= dodgeChance; }
    bool IsCritical()   { return Random.Range(1f, 100f) <= criticalChance; }
    public void SetTarget(Character target)
    { 
        this.target = target;
        if (target != this) transform.LookAt(target.transform);
    }

    public void Damaged(float damage, float damageRate, bool isMagic = false)
    {                // 공격자의 공격력, 공격자의 공격력 증가량, 물리마법공격 구분
        if (isMagic)
        {
            curHp -= (int)(damage * 0.01f * (100f + damageRate - magicArmorRate));
        }
        else {
            if (IsDodge())
                {// TODO 회피효과 출력
                Debug.Log(name + "가 회피");
            } 
            else{
                curHp -= (int)(damage * 0.01f * (100f + damageRate - armorRate));
                Debug.Log(name + "가 " + (int)(damage * 0.01f * (100f + damageRate - armorRate)) + " 피해 입음");
            }
        }

        if (curHp <= 0) Death();
    }

    public virtual void Death(){
        isDead = true;
        grx.SetActive(false);
    }

    public void Revive(float rateHP_0f_to_1f)
    {
        isDead = false;
        grx.SetActive(true);
        curHp = rateHP_0f_to_1f * maxHp;
    }

    public void Move(Transform destTransform)
    {
        gameObject.transform.LookAt(destTransform);
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
