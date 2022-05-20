using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackCommand : AttackCommand
{

    public NormalAttackCommand(Character owner) : base(owner){}

    public override void Attack()
    {
        if (null == owner.target) return;
        IDamagable target = owner.target.GetComponent<IDamagable>();
        target?.Damaged(owner.curDamage, owner.powerRate);
    }
}