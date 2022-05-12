using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionMacro : BattleMacro
{
    protected bool isSatisfy;
    public abstract bool IsSatisfy();

    public ConditionMacro(string desc, Hero hero) : base(desc, hero) {}
}
