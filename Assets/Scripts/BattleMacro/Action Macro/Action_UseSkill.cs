using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_UseSkill : ActionMacro
{
    [Tooltip("몇 번째 스킬인지")] [Range(1, 4)] 
    public int skillNumber;

    public override bool IsReady()
    {
        if (owner.skills[skillNumber - 1] == null) return false;
        return owner.skills[skillNumber - 1].IsReady();
    }

    public override void Execute(Character target)
    {
        //if (owner.skills[skillNumber - 1] == null) return false;  // IsReady()에서 걸러짐

        owner.skills[skillNumber - 1].Use(target);
    }
}