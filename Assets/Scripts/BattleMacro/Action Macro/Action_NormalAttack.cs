﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action_NormalAttack : ActionMacro
{
    public override bool Execute(){ //Debug.Log("normal att");
        
        if (owner.target == null) return false;
        // {   
        //     if (owner.eGroup == EGroup.Hero){
        //         owner.target = DungeonManager.instance.curDungeon.GetRandMonster();
        //         if (owner.target == null) return false;
        //     }
        //     else { return false; }
        // }
        
        if (owner.IsTargetInRange(owner.attackRange))
        {   
            if (!owner.nav.isStopped)
            { owner.nav.isStopped = true; }
            owner.AttackInit();
        }
        else{   
            owner.Move(owner.target.transform.position);
        }
        return true;
    }
}