using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionMacro : BattleMacro
{
    public abstract bool IsReady();
    public abstract void Execute(Character target);
    public void LookTarget(Transform tf)
    { if (tf != null) owner.transform.LookAt(tf); }
}
