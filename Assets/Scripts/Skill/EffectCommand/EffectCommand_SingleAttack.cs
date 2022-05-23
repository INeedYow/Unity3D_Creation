using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectCommand_SingleAttack : EffectCommand
{
    float m_dmg;
    IDamagable m_target;

    public EffectCommand_SingleAttack(Skill skill) : base(skill) {}
    public override void Works()
    {
        // if (skill.owner.target == null) return;

        // m_target = skill.owner.target.GetComponentInChildren<IDamagable>();
        // if (m_target != null)
        // {   //
        //     if (skill.data.isMagic)    { m_dmg = skill.owner.magicDamage * skill.data.powerRatio; }
        //     else                       { m_dmg = skill.owner.curDamage * skill.data.powerRatio; }

        //     m_target.Damaged(m_dmg, skill.owner.powerRate, skill.owner, skill.data.isMagic);
        // }
    }
}