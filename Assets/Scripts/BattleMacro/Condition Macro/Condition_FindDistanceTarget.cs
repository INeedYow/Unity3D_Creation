using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindDistanceTarget : ConditionMacro
{
    public EMost eMost;
    public EGroup eTargetGroup;
    [Tooltip("distance")]
    public float value;
    float m_sqrValue;
    float m_sqrDist;

    private void Start() {
        m_sqrValue = value * value;
    }

    private void OnEnable() 
    {   
        DungeonManager.instance.onWaveStart += OnBattle; 
        DungeonManager.instance.onWaveEnd += OffBattle;   }
    private void OnDisable()    
    {   
        DungeonManager.instance.onWaveStart -= OnBattle; 
        DungeonManager.instance.onWaveEnd -= OffBattle;  }

    public override bool IsSatisfy()
    {  
        if (null == owner.target) { FindTarget(); }
        
        return isSatisfy;
    }

    public void OnBattle() { InvokeRepeating("FindTarget", 0f, 0.2f); }
    public void OffBattle() { CancelInvoke("FindTarget"); }

    void FindTarget()
    {   
        if (eTargetGroup == EGroup.Hero)
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
                        isSatisfy = true;
                    }
                }
                else{   // value 이하
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue >= m_sqrDist)
                    {
                        owner.target = ch;
                        isSatisfy = true;
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
                        isSatisfy = true;
                    }
                }
                else{   // value 이하
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue >= m_sqrDist)
                    {
                        owner.target = ch;
                        isSatisfy = true;
                    }
                }
            }
        }
        isSatisfy = false;
    }
}
