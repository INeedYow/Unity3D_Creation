using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_GetMostDistanceTarget : ConditionMacro
{
    [Tooltip("Least : 최소 / Most : 최대")]
    public EMost eMost;
    public EGroup eTargetGroup;
    float m_value;
    float m_sqrDist;

    float m_lastFindTime;

    // private void OnEnable() {   
    //     DungeonManager.instance.onWaveStart += OnBattle; 
    //     DungeonManager.instance.onWaveEnd += OffBattle;  
    //     DungeonManager.instance.onDungeonExit += OffBattle;
    // }
    // private void OnDisable() {   
    //     DungeonManager.instance.onWaveStart -= OnBattle; 
    //     DungeonManager.instance.onWaveEnd -= OffBattle;  
    //     DungeonManager.instance.onDungeonExit -= OffBattle;
    // }

    public override bool IsSatisfy(){  
        if (target == null) 
        {
            FindTarget();
        }
        else if (Time.time >= m_lastFindTime + 0.2f) 
        {
            FindTarget();
        }
        return true;
    }

    // public void OnBattle() { InvokeRepeating("FindTarget", 0f, 0.2f); }  // 이러면 항상 찾고있음
    // public void OffBattle() { CancelInvoke("FindTarget"); }
    
    
    void FindTarget()
    {   //Debug.Log("get dist target()");
        m_lastFindTime = Time.time;

       if (eTargetGroup == EGroup.Hero)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return;
            
            if (eMost == EMost.Least)
            {   // 최소
                m_value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value > m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value  < m_sqrDist)
                    {
                        m_value = m_sqrDist;
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
                m_value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value > m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;
                    m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (m_value < m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        target = ch;
                    }
                }
            }
        }
    }
}
