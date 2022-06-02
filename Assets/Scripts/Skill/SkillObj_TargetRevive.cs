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
    }
}
