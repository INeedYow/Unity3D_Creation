using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Attacker : ConditionMacro
{
    public override bool IsSatisfy()
    {  
        target = owner.attacker;

        if (target == null) return false;
        return true;
    }


}
