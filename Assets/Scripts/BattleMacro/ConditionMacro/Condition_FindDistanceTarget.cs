using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindDistanceTarget : ConditionMacro
{
    public EMost eMost;
    public EGroup eGroup;
    float m_sqrValue;
    float m_sqrDist;
    float m_lastFindTime;
    float m_findDelay = 0.2f;

    public Condition_FindDistanceTarget(string desc, Hero hero, EMost eMost, EGroup eGroup, float value) 
        : base(desc, hero)
    {
        this.eMost = eMost;
        this.eGroup = eGroup;
        this.m_sqrValue = value * value;
    }

    public override bool IsSatisfy(){  
        return FindTarget();
    }

    bool FindTarget()
    {   // 최적화 방법.?
        // if (Time.time < m_lastFindTime + m_findDelay) {
        //     return true;    // 그 사이 죽었거나 없어진 경우에 예외처리 필요할듯..
        // }

        // m_lastFindTime = Time.time;

        if (eGroup == EGroup.Ally)
        {   // 아군
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue <= m_sqrDist)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
                else{   // value 이하
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue >= m_sqrDist)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
            }
        }
        else{   // 적군
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {   
                if (ch.isDead) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    Debug.Log("sqrDist : " + m_sqrDist + " / sqrValue : " + m_sqrValue);
                    if (m_sqrValue <= m_sqrDist)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
                else{   // value 이하
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue >= m_sqrDist)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
