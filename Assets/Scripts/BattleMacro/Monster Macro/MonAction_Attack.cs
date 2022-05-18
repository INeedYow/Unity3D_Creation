using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonAction_Attack : ActionMacro
{
    public override bool Execute()
    {
        if (owner.target == null)  return false;
        
        if (owner.IsTargetInRange())
        {
            owner.Attack();
        }
        else{
            MoveToTarget();
        }
        return true;
    }

    void MoveToTarget(){
        owner.Move(owner.target.transform);
    }
}