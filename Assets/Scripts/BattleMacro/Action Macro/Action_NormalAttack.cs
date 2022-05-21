using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action_NormalAttack : ActionMacro
{
    public override bool Execute(){
        
        if (owner.target == null)
        {   
            if (owner.eGroup == EGroup.Ally){
                owner.target = DungeonManager.instance.curDungeon.GetRandMonster();
                if (owner.target == null) return false;
            }
            else { return false; }
        }
        
        if (owner.IsTargetInRange(owner.attackRange))
        {   
            owner.nav.isStopped = true;
            owner.AttackInit();
        }
        else{   
            owner.Move(owner.target.transform.position);
        }
        return true;
    }
}