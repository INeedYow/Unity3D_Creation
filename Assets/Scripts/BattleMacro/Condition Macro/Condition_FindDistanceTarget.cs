using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindDistanceTarget : ConditionMacro
{
    [Tooltip("Least : 최소 / Most : 최대")]
    public EMost        eMost;
    public EGroup       eTargetGroup;
    [Tooltip("distance")] 
    public float        value;
    
    float m_sqrValue;
    float m_sqrDist;

    private void Start() { m_sqrValue = value * value; }

    private void OnEnable() {   
        DungeonManager.instance.onWaveStart += OnBattle; 
        DungeonManager.instance.onWaveEnd += OffBattle;   
        DungeonManager.instance.onDungeonExit += OffBattle;
    }
    private void OnDisable() {   
        DungeonManager.instance.onWaveStart -= OnBattle; 
        DungeonManager.instance.onWaveEnd -= OffBattle;  
        DungeonManager.instance.onDungeonExit -= OffBattle;
    }

    public override bool IsSatisfy()
    {  
        if (target != null) return true;
        return false;
    }

    public void OnBattle() { InvokeRepeating("FindTarget", 0f, 0.2f); }
    public void OffBattle() { CancelInvoke("FindTarget"); }

    void FindTarget()
    {   
        if (eTargetGroup == EGroup.Hero)
        {   // 아군
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue <= m_sqrDist)
                    {
                        target = ch;
                    }
                }
                else{   // value 이하
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue >= m_sqrDist)
                    {
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {   
                if (ch.isDead || ch.isStop) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    //Debug.Log("sqrDist : " + m_sqrDist + " / sqrValue : " + m_sqrValue);
                    if (m_sqrValue <= m_sqrDist)
                    {
                        target = ch;
                    }
                }
                else{   // value 이하
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_sqrValue >= m_sqrDist)
                    {
                        target = ch;
                    }
                }
            }
        }
    }
}
