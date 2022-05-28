﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_GetMostDistanceTarget : ConditionMacro
{
    [Tooltip("Least : 최소 / Most : 최대")]
    public EMost eMost;
    public EGroup eTargetGroup;
    Character m_target;
    float m_value;
    float m_sqrDist;

    private void OnEnable() {   // 웨이브 진행 중에만 거리 검사하도록.(isStop과 별개로 Invoke는 돌지 않나?)
        //DungeonManager.instance.onWaveStart += OnBattle; 
        //DungeonManager.instance.onWaveEnd += OffBattle;  
    }
    private void OnDisable() {   
        //DungeonManager.instance.onWaveStart -= OnBattle; 
        //DungeonManager.instance.onWaveEnd -= OffBattle;  
    }

    public override bool IsSatisfy(){  
        //if (null == owner.target) 
        //{
        GetTarget(); 
        //}
        
        return true;
    }

    //public void OnBattle() { InvokeRepeating("GetTarget", 0f, 0.2f); }
    //public void OffBattle() { CancelInvoke("GetTarget"); }
    
    void GetTarget()
    {   //Debug.Log("get dist target()");
       if (eTargetGroup == EGroup.Hero)
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
