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
        {   //Debug.Log("isTargetInRng");
            owner.nav.isStopped = true;
            owner.AttackInit();
        }
        else{
            owner.Move(owner.target.transform.position);
        }
        return true;
    }
}