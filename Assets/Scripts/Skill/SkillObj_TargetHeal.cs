using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetHeal : SkillObject
{
    IDamagable target;
    public override void Works()
    {
        if (skill.owner.target != null)
        { target = skill.owner.target.GetComponent<IDamagable>(); }

        for (int i = 0; i < data.count; i++)
        {
            if (target == null) break;
            
            target.Healed(data.powerRatio * skill.owner.magicDamage);
        }

       if (skill.owner.target != null) 
            AddBuff(skill.owner.target);
            
        target = null;
    }
}
