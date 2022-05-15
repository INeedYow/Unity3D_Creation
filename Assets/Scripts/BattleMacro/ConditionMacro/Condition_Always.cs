using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Always : ConditionMacro
{
    public Condition_Always(Hero hero) : base(hero){}

    public override bool IsSatisfy() { return true; }
}
