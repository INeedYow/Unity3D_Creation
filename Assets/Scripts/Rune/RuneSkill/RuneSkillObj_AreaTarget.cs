using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillObj_AreaTarget : RuneSkillObject
{
    public bool isHeal; 
    float m_sqrDist;

    public override void Works()
    {
        StartCoroutine("OnWorks");
        Invoke("FinishWorks", data.duration);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, data.area);
    }

    IEnumerator OnWorks()
    {
        while (true)
        {
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;

                    m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                
                    if (data.area * data.area < m_sqrDist) continue;
                
                    if (isHeal)
                    {
                        ch.Healed(data.power);
                    }
                    else{
                        ch.Damaged(data.power);
                    }
                
                    AddBuff(ch);

                    if (data.eOnWorksEffect != EEffect.None)
                    {
                        eff = ObjectPool.instance.GetEffect((int)data.eOnWorksEffect);
                        eff.transform.position = ch.targetTF.position;
                    }
                }


                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;

                    m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                
                    if (data.area * data.area < m_sqrDist) continue;

                    if (isHeal)
                    {
                        ch.Healed(data.power);
                    }
                    else{
                        ch.Damaged(data.power);
                    }

                    AddBuff(ch);

                    if (data.eOnWorksEffect != EEffect.None)
                    {
                        eff = ObjectPool.instance.GetEffect((int)data.eOnWorksEffect);
                        eff.transform.position = ch.targetTF.position;
                    }
                }

            yield return new WaitForSeconds(data.repeatInterval);
        }
    }
}
