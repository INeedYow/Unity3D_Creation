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
            
            target.Damaged(
                data.powerRatio * skill.owner.magicDamage,
                skill.owner.powerRate,
                skill.owner,
                true
            );
        }
       
        target = null;
        FinishWorks();
    }
}
