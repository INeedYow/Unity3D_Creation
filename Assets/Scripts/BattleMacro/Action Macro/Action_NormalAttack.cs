using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action_NormalAttack : ActionMacro
{

    public override bool IsReady()
    {
        return true;
    }
    
    public override void Execute(Character target){ //Debug.Log("normal att");
        
        if (target == null) return;
        
        if (owner.IsTargetInRange(target, owner.attackRange))
        {   
            if (!owner.nav.isStopped)
            { owner.nav.isStopped = true; }
            LookTarget(target.transform);
            owner.AttackInit(target);
        }
        else{   
            owner.MoveToTarget(target);
        }

    }
}