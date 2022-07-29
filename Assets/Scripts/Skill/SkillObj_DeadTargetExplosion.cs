using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_DeadTargetExplosion : SkillObject
{
    public EGroup eTargetGroup;
    IDamagable m_target;

    float m_sqrDist;

    public override void Works()
    {
        if (skill.target == null || !skill.target.isDead) return;

        if (eTargetGroup == EGroup.Monster)
        {   
            foreach (Monster mon in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (mon.isDead || mon.isStop) continue;

                m_sqrDist = (skill.target.transform.position - mon.transform.position).sqrMagnitude;

                if (data.area * data.area < m_sqrDist) continue;

                m_target = mon.GetComponent<IDamagable>();

                if (data.isMagic)
                {
                    m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                }
                else{
                    m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                }

                AddBuff(mon);
            }

            if (data.eTargetEffect != EEffect.None)
            {  
                eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                eff.SetPosition(skill.target);
            }

            if (data.eUserEffect != EEffect.None)
            {   
                eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                eff.SetPosition(skill.owner);
            }

            if (skill.target is Monster)
            {
                DungeonManager.instance.RemoveMonster(skill.target as Monster);
            }

        }

        else
        {
            foreach (Hero hero in PartyManager.instance.heroParty)
            {
                if (hero.isDead || hero.isStop) continue;
                
                m_sqrDist = (skill.target.transform.position - hero.transform.position).sqrMagnitude;

                if (data.area * data.area < m_sqrDist) continue;

                m_target = hero.GetComponent<IDamagable>();

                if (data.isMagic)
                {
                    m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                }
                else{
                    m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                }

                AddBuff(hero);
            }

            if (data.eTargetEffect != EEffect.None)
            {  
                eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                eff.SetPosition(skill.target); 
            }   

            if (data.eUserEffect != EEffect.None)
            {   
                eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                eff.SetPosition(skill.owner);
            }
        }

        FinishWorks();
    }
}
