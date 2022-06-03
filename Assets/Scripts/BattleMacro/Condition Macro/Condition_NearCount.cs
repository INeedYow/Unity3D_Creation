using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_NearCount : ConditionMacro
{
    public EType        eType;
    public EGroup       eCountGroup;
    [Range(0, 13)]      public int count;
    [Range(0f, 20f)]    public float distance;

    int m_count;

    public override bool IsSatisfy()
    {
        throw new System.NotImplementedException();
    }

    void FindTarget()
    {
        // if (eCountGroup == EGroup.Hero)
        // {   // 영웅
        //     foreach (Character ch in PartyManager.instance.heroParty)
        //     {

        //     }
        // }
    }
}
