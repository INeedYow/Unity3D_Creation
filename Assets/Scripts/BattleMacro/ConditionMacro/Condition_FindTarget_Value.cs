using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindTarget_Value : Condition_FindTarget
{
    public Condition_FindTarget_Value(string desc, Hero hero, EGroup eGroup, EValue eValue) 
        : base(desc, hero, eGroup, eValue) {}

    public override bool IsSatisfy()
    {
        return false;
    }
}
