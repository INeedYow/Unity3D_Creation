using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCommand_SingleAttack : EffectCommand
{
    public EffectCommand_SingleAttack(Skill skill) : base(skill) {}
    public override void Works()
    {
        if (skill.owner.target == null) return;

        IDamagable target = skill.owner.target.GetComponentInChildren<IDamagable>();
        if (target != null)
        {   Debug.Log("Skill dmg");
            target.Damaged(
                skill.data.damage,
                skill.data.isMagic ? skill.owner.magicPowerRate : skill.owner.powerRate,
                skill.data.isMagic
            );
        }
    }
}
