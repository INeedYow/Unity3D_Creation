using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_NearCount : ConditionMacro
{
    public EGroup       eCountGroup;
    [Range(0, 13)]      public int count;
    [Range(0f, 20f)]    public float distance;

    int     m_count;
    float   m_sqrGoalDist;
    float   m_sqrDist;
    bool    m_isSatisfy;
    float   m_lastCountTime;

    private void Start() {
        m_sqrGoalDist = distance * distance;
    }

    public override bool IsSatisfy()
    {
        CheckCount();
        return m_isSatisfy;
    }

    void CheckCount()
    {
        if (Time.time < m_lastCountTime + 0.2f) return;

        m_lastCountTime = Time.time;
        m_count = 0;

        if (eCountGroup == EGroup.Hero)
        {   // 영웅
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;

                m_sqrDist = (owner.transform.position - ch.transform.position).sqrMagnitude;

                if (m_sqrDist <= m_sqrGoalDist)
                {
                    m_count++;
                }
            }

            m_isSatisfy = m_count >= count;
        }

        else{
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (ch.isDead || ch.isStop) continue;

                m_sqrDist = (owner.transform.position - ch.transform.position).sqrMagnitude;

                if (m_sqrDist <= m_sqrGoalDist)
                {
                    m_count++;
                }
            }

            m_isSatisfy = m_count >= count;
        }
    }
}
