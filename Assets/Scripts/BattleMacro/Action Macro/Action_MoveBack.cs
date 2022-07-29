using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MoveBack : ActionMacro
{
    public override bool IsReady()
    {
        return true;
    }
    
    public override void Execute(Character target)
    {
        if (owner.eGroup == EGroup.Hero)
        {
            //owner.nav.Move(new Vector3(0f, 0f, -1f));
            //owner.nav.SetDestination(DungeonManager.instance.heroBackTF.position);
            owner.MoveToPos(DungeonManager.instance.heroBackTF.position);
        }
        else
        {
            //owner.nav.Move(new Vector3(0f, 0f, 1f));
            //owner.nav.SetDestination(DungeonManager.instance.monBackTF.position);
             owner.MoveToPos(DungeonManager.instance.monBackTF.position);
        }
    }
}
