using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MoveToTarget : ActionMacro
{

    public override bool IsReady()
    {   
        return true;
    }

    public override void Execute(Character target)
    {   
        if (target == null) return;
        
        owner.MoveToTarget(target);
    }
}
