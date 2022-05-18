using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action_NormalAttack : ActionMacro
{
    public override bool Execute(){
        //Debug.Log("Action_NormalAttack.Execute()");
        if (owner.target == null)
        {   
            owner.target = DungeonManager.instance.curDungeon.GetRandMonster();
            if (owner.target == null) return false;
        }
        
        if (owner.IsTargetInRange())
        {
            owner.attackCommand.Attack();
        }
        else{
            MoveToTarget();
        }

        return true;
    }

    void MoveToTarget(){
        //Debug.Log("MoveToTarget()");
        owner.Move(owner.target.transform);
    }

}
