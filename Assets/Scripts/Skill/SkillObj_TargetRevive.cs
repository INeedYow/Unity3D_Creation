using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetRevive : SkillObject
{
    IDamagable m_target;
    public override void Works()
    {
        if (skill.target == null) return;

        if (!skill.target.isDead) return;

        m_target = skill.target.GetComponent<IDamagable>(); 

        if (m_target == null) return;

        m_target.Revive(30 + data.powerRatio * skill.owner.magicDamage);
        
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

        FinishWorks();
    }
}
