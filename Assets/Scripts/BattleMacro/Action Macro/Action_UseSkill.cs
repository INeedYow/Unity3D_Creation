using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_UseSkill : ActionMacro
{
    [Tooltip("몇 번째 스킬인지")] [Range(1, 4)] 
    public int skillNumber;

    public override bool IsReady()
    {   //Debug.Log("IsReady()");
        if (owner.skills[skillNumber - 1] == null) return false;
        //Debug.Log("1");
        if (owner.silence > 0 || owner.madness > 0) return false;
        //Debug.Log("2");
        return owner.skills[skillNumber - 1].IsReady();
    }

    public override void Execute(Character target)
    {   //Debug.Log("Execute()");
        
        owner.skills[skillNumber - 1].Use(target);

        if (target != null)
        {LookTarget(target.transform);}
    }
}