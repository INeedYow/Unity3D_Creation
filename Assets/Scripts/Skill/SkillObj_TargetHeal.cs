using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetHeal : SkillObject
{
    IDamagable m_target;
    public override void Works()
    {
        StartCoroutine("OnWorks");
    }

    IEnumerator OnWorks()
    {
        if (skill.target == null)
        {
            FinishWorks();
            yield break;
        }


        m_target = skill.target.GetComponent<IDamagable>(); 
        
        if (data.eStartEffect != EEffect.None)
        {
            eff = ObjectPool.instance.GetEffect((int)data.eStartEffect);
            eff.SetPosition(skill.owner);
        }

        for (int i = 0; i < data.repeat; i++)
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

            yield return new WaitForSeconds(data.interval);
        }

        AddBuff(skill.target);
            
        m_target = null;
        FinishWorks();
    }
}
