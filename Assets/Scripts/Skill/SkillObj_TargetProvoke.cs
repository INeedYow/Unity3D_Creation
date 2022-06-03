using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetProvoke : SkillObject
{
    [Range(0.1f, 10f)] public float duration;

    public override void Works()
    {
        if (skill.target == null) return;

        skill.target.SetProvoke(skill.owner, duration);

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
    }
}
