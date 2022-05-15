using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_GetMostDistanceTarget : ConditionMacro
{
    public EMost eMost;
    public EGroup eGroup;
    Character m_target;
    float m_value;
    float m_sqrDist;

    public Condition_GetMostDistanceTarget(Hero hero, EMost eMost, EGroup eGroup) 
        : base(hero) 
    {
        this.eMost = eMost;
        this.eGroup = eGroup;
    }

    public override bool IsSatisfy(){   // 거리는 전투 중 매 순간 확인해야
        UpdateDistance();
        owner.target = m_target;
        return true;
    }
    
    void UpdateDistance()
    {
       if (eGroup == EGroup.Ally)
        {   // 아군
            m_target = PartyManager.instance.GetAliveHero();
            if (m_target == null) return;
            
            if (eMost == EMost.Least)
            {   // 최소
                m_value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value > m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        m_target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value  < m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        m_target = ch;
                    }
                }
            }
        }
        else{   // 적군
           m_target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (m_target == null) return;
            
            if (eMost == EMost.Least)
            {   // 최소
                m_value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value > m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        m_target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value < m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        m_target = ch;
                    }
                }
            }
        }
        owner.target = m_target;
    }
}
