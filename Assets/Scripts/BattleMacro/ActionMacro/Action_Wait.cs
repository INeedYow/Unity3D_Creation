using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Wait : ActionMacro
{
    public Action_Wait(Hero hero) : base(hero) {}

    public override bool Execute() { return true; }
}
