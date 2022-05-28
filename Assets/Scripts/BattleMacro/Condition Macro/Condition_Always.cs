using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Always : ConditionMacro
{
    public override bool IsSatisfy(bool hasChange) { return true; }
}
