using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

public class RuneSkillObject_AreaStasis : RuneSkillObject
{
    //public UnityAction onFinishStasis;
    float m_sqrDist;
    float m_timer;
    float m_sqrArea;

    public override void Works()
    {
        m_timer = 0f;
        m_sqrArea = data.area * data.area;
        StartCoroutine("OnWorks");
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, data.area);
    }

    IEnumerator OnWorks()
    {
        while (m_timer < data.duration)
        {
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (ch.isDead || ch.isStop) continue;

                m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                
                if (m_sqrArea < m_sqrDist) continue;
                
                ch.SetStasis(true);

            }

            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;

                m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                
                if (m_sqrArea < m_sqrDist) continue;

                ch.SetStasis(true);

            }

            m_timer += Time.deltaTime; 
            yield return null;
        }

       

        // 해제 // todo 현재 제대로 해제 안 되는 경우 있음
        foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
        {
            if (!ch.IsStasis()) continue;  // stasis 아닌 경우

            m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                
            if (m_sqrArea < m_sqrDist) continue;
                
            ch.SetStasis(false);

        }

        foreach (Character ch in PartyManager.instance.heroParty)
        {
            if (!ch.IsStasis()) continue;

            m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                
            if (m_sqrArea < m_sqrDist) continue;

            ch.SetStasis(false);

        }
        
        FinishWorks();
    }
}
