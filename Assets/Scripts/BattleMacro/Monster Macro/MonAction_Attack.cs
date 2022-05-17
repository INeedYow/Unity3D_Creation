using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonAction_Attack : ActionMacro
{
    public override bool Execute(){
        //Debug.Log("MonAction_Attack.Execute()");
        if (owner.target == null)  return false;
        
        if (owner.IsTargetInRange())
        {
            if (Time.time < owner.lastAttackTime + owner.attackDelay) return true;
            Attack();
        }
        else{
            MoveToTarget();
        }

        return true;
    }

    void Attack(){
        //Debug.Log("Attack()");
        owner.lastAttackTime = Time.time;

        if (owner.aoeRange == 0f)
        {   // 단일 타겟 공격
            IDamagable target = owner.target.GetComponent<IDamagable>();
            target?.Damaged(owner.curDamage, owner.powerRate);
        }
        else{   // 범위 공격
            Collider[] colls = Physics.OverlapSphere(
                owner.target.transform.position, owner.aoeRange, LayerMask.GetMask("Hero"));

            if (colls.Length == 0) return;

            foreach (Collider coll in colls)
            {   
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