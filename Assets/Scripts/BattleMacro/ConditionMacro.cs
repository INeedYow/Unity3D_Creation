using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EMost { Least, Most };
public enum EGroup { Ally, Enemy };
public abstract class ConditionMacro : BattleMacro
{
    protected bool isSatisfy;
    public abstract bool IsSatisfy();

    //public ConditionMacro(Hero hero) : base(hero) {}
}
