using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindHpTarget : ConditionMacro
{
    [Tooltip("Least : 최소 / Most : 최대")]
    public EMost eMost;
    public EGroup eTargetGroup;
    [Range(0f, 100f)] public float value;

    bool m_hasAnyHpChange;
    bool m_isSatisfy;

    private void OnEnable()     { DungeonManager.instance.onChangeAnyHP += AnyHpChange; }
    private void OnDisable()    { DungeonManager.instance.onChangeAnyHP -= AnyHpChange; }
    
    void AnyHpChange() { m_hasAnyHpChange = true; }

    public override bool IsSatisfy()
    {   //누군가의 체력 변경이 일어나면 다시 검사
        if (m_hasAnyHpChange)  { m_isSatisfy = FindTarget(); }
    
        return m_isSatisfy;
    }

    bool FindTarget()
    {
        m_hasAnyHpChange = false;
        
        if (eTargetGroup == EGroup.Hero)
        {   // 아군
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    if (ch.curHp / ch.maxHp * 100f >= value)
                    {
                        target = ch;  
                        return true;
                    }
                }
                else{   // value 이하
                    if (ch.curHp / ch.maxHp * 100f <= value)
                    {
                        target = ch;  
                        return true;
                    }
                }
            }
        }
        else{
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (ch.isDead) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    if (ch.curHp / ch.maxHp * 100f >= value)
                    {
                        target = ch;  
                        return true;
                    }
                }
                else{   // value 이하
                    if (ch.curHp / ch.maxHp * 100f <= value)
                    {
                        target = ch;  
                        return true;
                    }
                }
            }
        }
        return false;
    }
}