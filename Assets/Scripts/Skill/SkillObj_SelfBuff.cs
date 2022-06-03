using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_SelfBuff : SkillObject
{
    public override void Works()
    {
        if (data.eUserEffect != EEffect.None)
        {
            eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
            eff.SetPosition(skill.owner);
        }

        AddBuff(skill.owner);
    }
}
