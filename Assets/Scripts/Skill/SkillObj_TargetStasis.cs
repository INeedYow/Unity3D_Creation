using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetStasis : SkillObject
{
    public override void Works()
    {
        if (skill.target == null) 
        {
            FinishWorks();
            return;
        }

        skill.target.SetStasis(true, data.duration);

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

        AddBuff(skill.target);
        FinishWorks();
    }
}
