using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_SelfHeal : SkillObject
{
    public override void Works()
    {
        StartCoroutine("OnWorks");
    }

    IEnumerator OnWorks()
    {
        if (skill.owner.isDead || skill.owner.isStop)
        {
            FinishWorks();
            yield break;
        }

        if (data.eStartEffect != EEffect.None)
        {
            eff = ObjectPool.instance.GetEffect((int)data.eStartEffect);
            eff.SetPosition(skill.owner);
        }

        AddBuff(skill.owner);
        
        for (int i = 0; i < data.repeat; i++)
        {
            skill.owner.Healed(data.powerRatio * skill.owner.magicDamage);

            if (data.eUserEffect != EEffect.None)
            {
                eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                eff.SetPosition(skill.owner);
            }

            yield return new WaitForSeconds(data.interval);
        }
            
        FinishWorks();
    }
}
