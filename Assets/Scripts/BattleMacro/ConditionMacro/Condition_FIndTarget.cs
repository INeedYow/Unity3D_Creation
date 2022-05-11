using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO macro,, class 및 enum으로 나눌 영역 확실히 정하기
public abstract class Condition_FindTarget : ConditionMacro
{
    public enum EGroup { Ally, Enemy };
    public enum EValue { 
        HP,
        Power,
        Armor,
        MagicPower,
        MagicArmor,
        AttackRange,
        AttackSpeed,
        Distance,
        MoveSpeed,
    };

    public EGroup eGroup;
    public EValue eValue;

    protected Character target; 
    protected float value;

    public Condition_FindTarget(string desc, Hero hero, EGroup eGroup, EValue eValue) 
        : base(desc, hero)
    {
        this.eGroup = eGroup;
        this.eValue = eValue;
    }

    //public abstract bool IsSatisfy();
}
