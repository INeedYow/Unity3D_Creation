using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Wait : ActionMacro
{
    public override bool IsReady()
    {
        return true;
    }
    public override void Execute(Character target) { return; }
}
