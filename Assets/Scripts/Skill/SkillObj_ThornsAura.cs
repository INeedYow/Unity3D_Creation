using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_ThornsAura : SkillObject
{
    [Range(1f, 10f)] public float duration;
    
    Character m_attacker;   // 


    public override void Works()
    {
        skill.owner.onDead += Quit;
        skill.owner.onDamagedGetAttacker += Thorns;

        Invoke("Quit", duration);
    }

    void Quit()
    {
        CancelInvoke("Quit");

        skill.owner.onDead -= Quit;
        skill.owner.onDamagedGetAttacker -= Thorns;
        FinishWorks();
    }

    void Thorns(Character attacker)
    {
        if (m_attacker == attacker || attacker.isDead)
        {
            m_attacker = null;
            return;
        }

        m_attacker = attacker;

        if (data.isMagic)
        {
            attacker.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
        }
        else{
            attacker.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner);
        }

        AddBuff(attacker);

        if (data.eTargetEffect != EEffect.None)
        {
            eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
            eff.SetPosition(attacker);
        }

        if (data.eUserEffect != EEffect.None)
        {
            eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
            eff.SetPosition(skill.owner);
        }

        m_attacker = null;
        
    }
}
