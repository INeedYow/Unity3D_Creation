using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetAttack : SkillObject
{
    IDamagable m_target;

    public override void Works()
    {
        if (skill.target == null) return;
        
        m_target = skill.target.GetComponent<IDamagable>();

        for (int i = 0; i < data.count; i++)
        {
            if (m_target == null) { FinishWorks(); }

            if (data.isMagic)
            {
                m_target.Damaged(
                    data.powerRatio * skill.owner.magicDamage,
                    skill.owner.powerRate,
                    skill.owner,
                    true
                );
            }
            else
            {   
                m_target.Damaged(
                    data.powerRatio * skill.owner.curDamage,
                    skill.owner.powerRate,
                    skill.owner,
                    false
                );
            }

            if (data.eUserEffect != EEffect.None)
            {
                eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                eff.SetPosition(skill.owner);
            }

            if (data.eTargetEffect != EEffect.None)
            {
                eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                eff.SetPosition(skill.target);
            }
        }
        
        if (skill.target != null) AddBuff(skill.target);
            
        m_target = null;
    }
}
