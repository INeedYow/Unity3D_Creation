using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_GetMostRangeTarget : ConditionMacro
{
    public EMost    eMost;
    public EGroup   eTargetGroup;

    float m_value;
    bool m_hasAnyRangeChange;

    private void OnEnable()     { // 누군가 사거리 변경, 웨이브 시작할 때 재검사
        DungeonManager.instance.onChangeAnyRange += AnyHpChange; 
        DungeonManager.instance.onWaveStart += AnyHpChange; 
    }
    private void OnDisable()    { 
        DungeonManager.instance.onChangeAnyRange -= AnyHpChange; 
        DungeonManager.instance.onWaveStart -= AnyHpChange;
    }

    void AnyHpChange() { m_hasAnyRangeChange = true; }


    public override bool IsSatisfy( )
    {   // 체력 변경 or 타겟 null 이면 재검색
        if (m_hasAnyRangeChange || target == null) FindTarget();
        return true;
    }

    void FindTarget()   
    {
        m_hasAnyRangeChange = false;
        
        if (eTargetGroup == EGroup.Hero)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return;

            if (eMost == EMost.Least)
            {   // 최소
                m_value = 100f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value > ch.attackRange)
                    {
                        m_value = ch.attackRange;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value < ch.attackRange)
                    {
                        m_value = ch.attackRange;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
            target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return;

            if (eMost == EMost.Least)
            {   // 최소
                m_value = 100f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value > ch.attackRange)
                    {
                        m_value = ch.attackRange;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value < ch.attackRange)
                    {
                        m_value = ch.attackRange;
                        target = ch;
                    }
                }
            }
        }
    }
}
