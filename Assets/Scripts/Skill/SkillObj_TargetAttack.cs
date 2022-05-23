using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetAttack : SkillObject
{
    IDamagable target;
    public override void Works()
    {
        if (skill.owner.target == null) return;
        
        target = skill.owner.target.GetComponent<IDamagable>();

        if (target == null) { FinishWorks(); }

        for (int i = 0; i < data.count; i++)
        {
            if (data.isMagic)
            {
                target.Damaged(
                    data.powerRatio * skill.owner.magicDamage,
                    skill.owner.powerRate,
                    skill.owner,
                true
                );
            }
            else
            {   Debug.Log("??target Att");
                target.Damaged(
                    data.powerRatio * skill.owner.curDamage,
                    skill.owner.powerRate,
                    skill.owner,
                    false
                );
            }
        }
       
        target = null;
        FinishWorks();
    }
}
