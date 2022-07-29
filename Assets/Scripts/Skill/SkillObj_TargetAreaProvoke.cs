using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetAreaProvoke : SkillObject
{
    public EGroup eTargetGroup;
    [Range(0.1f, 10f)] public float duration;

    float m_sqrDist;
    float m_sqrArea;

    private void Awake() { m_sqrArea = data.area * data.area; }

    public override void Works()
    {
        if (skill.target == null) 
        {
            FinishWorks(); 
            return;
        }

        if (eTargetGroup == EGroup.Monster)
        {   
            foreach (Monster mon in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (mon.isDead || mon.isStop) continue;

                m_sqrDist = (skill.target.transform.position - mon.transform.position).sqrMagnitude;
                
                if (m_sqrArea < m_sqrDist) continue;

                mon.SetProvoke(skill.owner, duration);
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
            
        }

        else
        {
            foreach (Hero hero in PartyManager.instance.heroParty)
            {
                if (hero.isDead || hero.isStop) continue;

                m_sqrDist = (skill.target.transform.position - hero.transform.position).sqrMagnitude;

                if (m_sqrArea < m_sqrDist) continue;

                hero.SetProvoke(skill.owner, duration);
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
            
        }

        FinishWorks();
    }
}
