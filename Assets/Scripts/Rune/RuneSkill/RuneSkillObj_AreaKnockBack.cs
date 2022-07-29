using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillObj_AreaKnockBack : RuneSkillObject
{
    float m_sqrDist;
    float m_sqrArea;

    public override void Works()
    {
        m_sqrArea = data.area * data.area;
        StartCoroutine("OnWorks");
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, data.area);
    }

    IEnumerator OnWorks()
    {
        for (int i = 0; i < data.repeat; i++)
        {
            foreach (Monster mon in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (mon.isDead || mon.isStop) continue;

                m_sqrDist = (transform.position - mon.transform.position).sqrMagnitude;

                if (m_sqrArea < m_sqrDist) continue;

                mon.nav.Move((mon.transform.position - transform.position) * data.power * Time.deltaTime);

                AddBuff(mon);
            }

            if (data.eOnWorksEffect != EEffect.None)
            {
                eff = ObjectPool.instance.GetEffect((int)data.eOnWorksEffect);
                eff.transform.position = transform.position;
            }

            yield return new WaitForSeconds(data.repeatInterval);
        }

        FinishWorks();
    }
}
