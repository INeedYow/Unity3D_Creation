using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetHeal : SkillObject
{
    IDamagable m_target;
    public override void Works()
    {
        if (skill.target == null) return;

        m_target = skill.target.GetComponent<IDamagable>(); 

        for (int i = 0; i < data.count; i++)
        {
            if (m_target == null) break;
            
            m_target.Healed(data.powerRatio * skill.owner.magicDamage);

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

        AddBuff(skill.target);
            
        m_target = null;
    }
}
