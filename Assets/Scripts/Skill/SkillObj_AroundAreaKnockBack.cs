using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_AroundAreaKnockBack : SkillObject
{
    public EGroup eOwnerGroup;
    public float knockBackSpeed;
    float m_sqrDist;
    float m_sqrArea;

    private void Start() { 
        m_sqrArea = data.area * data.area;
    }

    public override void Works()
    {   //Debug.Log("OnWorks");
        StartCoroutine("OnWorks");
    }

    IEnumerator OnWorks()
    {   
        if (eOwnerGroup == EGroup.Hero)
        {   
            for (int i = 0; i < data.repeat; i++)
            {
                foreach (Monster mon in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (mon.isDead || mon.isStop) continue;

                    m_sqrDist = (skill.owner.transform.position - mon.transform.position).sqrMagnitude;

                    if (m_sqrArea < m_sqrDist) continue;

                    if (data.isMagic)
                    {
                       mon.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                    }
                    else{
                        mon.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                    }

                    mon.nav.Move((mon.transform.position - skill.owner.transform.position) * knockBackSpeed * Time.deltaTime);

                    AddBuff(mon);

                    if (data.eTargetEffect != EEffect.None)
                    {  
                        eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                        eff.SetPosition(mon);
                    }

                    if (data.eUserEffect != EEffect.None)
                    {   
                        eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                        eff.SetPosition(skill.owner);
                    }
                }

                yield return new WaitForSeconds(data.interval);
            }
        }

        else
        {
            for (int i = 0; i < data.repeat; i++)
            {
                foreach (Hero hero in PartyManager.instance.heroParty)
                {
                    if (hero.isDead || hero.isStop) continue;
                
                    m_sqrDist = (skill.owner.transform.position - hero.transform.position).sqrMagnitude;

                    if (m_sqrArea < m_sqrDist) continue;

                    if (data.isMagic)
                    {
                        hero.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                    }
                    else{
                        hero.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                    }

                    hero.nav.Move((hero.transform.position - skill.owner.transform.position) * knockBackSpeed * Time.deltaTime);

                    AddBuff(hero);

                    if (data.eTargetEffect != EEffect.None)
                    {  
                        eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                        eff.SetPosition(hero); 
                    }   

                    if (data.eUserEffect != EEffect.None)
                    {   
                        eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                        eff.SetPosition(skill.owner);
                    }
                }

                yield return new WaitForSeconds(data.interval);
            }
        }

        FinishWorks();
    }
}
