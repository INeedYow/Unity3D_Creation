using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindDistanceTarget : ConditionMacro
{
    public EMost eMost;
    public EGroup eGroup;
    [Tooltip("distance")]
    public float value;
    float m_sqrValue;
    float m_sqrDist;

    private void Start() {
        m_sqrValue = value * value;
    }

    public override bool IsSatisfy(){  
        return FindTarget();
    }

    bool FindTarget()
    {   

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
                    //Debug.Log("sqrDist : " + m_sqrDist + " / sqrValue : " + m_sqrValue);
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
