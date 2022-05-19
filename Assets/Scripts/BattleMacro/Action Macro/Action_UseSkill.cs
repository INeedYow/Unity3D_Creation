using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_UseSkill : ActionMacro
{
    [Range(1, 4)]
    public int skillNumber;

    public override bool Execute()
    {
        return owner.skills[skillNumber].Use();
    }
}
