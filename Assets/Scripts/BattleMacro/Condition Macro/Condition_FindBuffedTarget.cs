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

    bool m_hasAnyBuffChange;


    private void OnEnable()     { DungeonManager.instance.onChangeAnyBuff += AnyBuffChange; }
    private void OnDisable()    { DungeonManager.instance.onChangeAnyBuff -= AnyBuffChange; }
    
    void AnyBuffChange() { m_hasAnyBuffChange = true; }

    public override bool IsSatisfy()
    {
        if (m_hasAnyBuffChange) FindTarget();

        if (target != null) return true;

        return false;
    }

    void FindTarget()
    {
        m_hasAnyBuffChange = false;

        target = null;
        
        if (eTargetGroup == EGroup.Hero)
        {   //hero
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
        else{   //monster
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
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
