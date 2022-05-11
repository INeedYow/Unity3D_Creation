using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO macro,, class 및 enum으로 나눌 영역 확실히 정하기
public class Condition_FindTarget : ConditionMacro
{
    public enum ETarget { Self, Ally, Enemy };
    public enum EType { 
        LowestHP,
        HighestHP,
        LowestPower,
        HighestPower,
        LowestArmor,
        HighestArmor,
        LowestMagicPower,
        HighestMagicPower,
        LowestMagicArmor,
        HightestMagicArmor,
    };

    public ETarget eTarget;
    Character m_target; 
    float m_value;

    public Condition_FindTarget(string desc, Hero hero, ETarget eTarget) : base(desc, hero)
    {
        this.eTarget = eTarget;
    }

    public override bool IsSatisfy()
    {   // TODO
        if (eTarget == ETarget.Self)
        {
            owner.target = owner;
            return true;
        }
        else{
            return false;
        }
    }

    //
    bool SetTarget_LowestHP()
    {
        if (eTarget == ETarget.Ally)
        {
            return false;
        }
        else{   // Self인 경우는 IsSatisfy()에서 이미 제외
            return false;
        }
    }
}
