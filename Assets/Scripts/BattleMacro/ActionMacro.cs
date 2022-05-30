using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionMacro : BattleMacro
{
    public abstract bool IsReady();
    public abstract void Execute(Character target);
}
