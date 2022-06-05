using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindBuffedTarget : ConditionMacro
{
    public EGroup eTargetGroup;
    [Tooltip("")]
    public EType eType;
    public bool isFindDebuff;
    [Tooltip("버프 개수 조건")]
    [Range(0, 20)] public int count = 1;

    public override bool IsSatisfy()
    {
        if (target == null) FindTarget();
        return target;
    }

    public override Character GetTarget()
    {
        if (owner.IsProvoked())
        {   
            return owner.GetProvoker();
        }

        return target; 
    }


    void FindTarget()
    {
        if (eTargetGroup == EGroup.Hero)
        {
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;


                if (isFindDebuff)
                {
                    if (eType == EType.Higher && ch.debuffCount >= count)
                    {
                        target = ch;
                    }
                    else if (eType == EType.Lower && ch.debuffCount <= count)
                    {
                        target = ch;
                    }
                    else if (eType == EType.Equal && ch.debuffCount == count)
                    {
                        target = ch;
                    }
                }
                else
                { 
                    if (eType == EType.Higher && ch.buffCount >= count)
                    {
                        target = ch;
                    }
                    else if (eType == EType.Lower && ch.buffCount <= count)
                    {
                        target = ch;
                    }
                    else if (eType == EType.Equal && ch.buffCount == count)
                    {
                        target = ch;
                    }
                }
                
            }
        }
    }
}
