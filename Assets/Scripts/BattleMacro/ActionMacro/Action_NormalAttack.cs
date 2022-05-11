﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 범위 공격 layermask enemy 써서 몬스터에는 이 코드 못씀
public class Action_NormalAttack : ActionMacro
{
    public Action_NormalAttack(string desc, Hero hero) : base(desc, hero) {}

    public override void Execute(){
        //Debug.Log("Action_NormalAttack.Execute()");
        if (owner.target == null)
        {   
            owner.target = DungeonManager.instance.curDungeon.GetRandMonster();
            return;
        }
        
        if (owner.IsTargetInRange())
        {
            if (Time.time < owner.lastAttackTime + owner.attackDelay) return;
            Attack();
        }
        else{
            MoveToTarget();
        }
    }

    void Attack(){
        //Debug.Log("Attack()");
        owner.lastAttackTime = Time.time;

        if (owner.aoeRange == 0f)
        {   // 단일 타겟 공격
            IDamagable target = owner.target.GetComponent<IDamagable>();
            target?.Damaged( owner.curDamage, owner.powerRate);
        }
        else{   // 범위 공격
            Collider[] colls = Physics.OverlapSphere(
                owner.target.transform.position, owner.aoeRange, LayerMask.GetMask("Monster"));

            if (colls.Length == 0) return;

            foreach (Collider coll in colls)
            {   // 빈 obj 밑에 둬서 부모의 IDamagable
                IDamagable target = coll.gameObject.GetComponentInParent<IDamagable>();
                target?.Damaged( owner.curDamage, owner.powerRate);
            }
            
        }
    }
    void MoveToTarget(){
        //Debug.Log("MoveToTarget()");
        owner.Move(owner.target.transform);
    }

}
