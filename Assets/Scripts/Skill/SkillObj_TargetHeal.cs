using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetHeal : SkillObject
{
    IDamagable m_target;
    public override void Works()
    {
        if (skill.target != null)
        { m_target = skill.target.GetComponent<IDamagable>(); }

        for (int i = 0; i < data.count; i++)
        {
            if (m_target == null) break;
            
            m_target.Healed(data.powerRatio * skill.owner.magicDamage);
        }

        if (skill.target != null) 
            AddBuff(skill.target);
            
        m_target = null;
    }
}
