using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillObject_AreaStasis : RuneSkillObject
{
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
        //Debug.Log("OnWorks");
        while (m_timer < data.duration)
        {
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (ch.isDead || ch.isStop) continue;

                m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;

                if (m_sqrArea < m_sqrDist) continue;
                
                //float distance = Vector3.Distance(ch.transform.position, transform.position);
                //if (data.area < distance) continue;

                //Debug.Log(string.Format("적용 {0} : dist [{1}] / area [{2}]", ch.name, m_sqrDist, m_sqrArea));


                ch.SetStasis(true);

            }

            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;

                m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                
                if (m_sqrArea < m_sqrDist) continue;

                //float distance = Vector3.Distance(ch.transform.position, transform.position);
                //Debug.Log(string.Format("적용 {0} : dist [{1}] / area [{2}]", ch.name, distance, data.area));
                //if (data.area < distance) continue;

                //Debug.Log(string.Format("적용 {0} : dist [{1}] / area [{2}]", ch.name, m_sqrDist, m_sqrArea));

                ch.SetStasis(true);
            }

            m_timer += 0.1f; 
            yield return new WaitForSeconds(0.1f);
        }

       //onFinishStasis?.Invoke(this);

        // 해제 // todo 현재 제대로 해제 안 되는 경우 있음
        foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
        {
            if (!ch.IsStasis()) continue;  // stasis 아닌 경우

            m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;

            //float distance = Vector3.Distance(ch.transform.position, transform.position);
            //if (data.area < distance) continue;

            //Debug.Log(string.Format("해제 {0} : dist [{1}] / area [{2}]", ch.name, distance, data.area));

                
            //Debug.Log(string.Format("해제 {0} : dist [{1}] / area [{2}]", ch.name, m_sqrDist, m_sqrArea));
            if (m_sqrArea < m_sqrDist) continue;
                
            ch.SetStasis(false);

        }

        foreach (Character ch in PartyManager.instance.heroParty)
        {
            if (!ch.IsStasis()) continue;

            m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;

            //float distance = Vector3.Distance(ch.transform.position, transform.position);
            //if (data.area < distance) continue;

            //Debug.Log(string.Format("해제 {0} : dist [{1}] / area [{2}]", ch.name, distance, data.area));

            //Debug.Log(string.Format("해제 {0} : dist [{1}] / area [{2}]", ch.name, m_sqrDist, m_sqrArea));
                
            if (m_sqrArea < m_sqrDist) continue;

            ch.SetStasis(false);

        }
        
        FinishWorks();
    }
}
