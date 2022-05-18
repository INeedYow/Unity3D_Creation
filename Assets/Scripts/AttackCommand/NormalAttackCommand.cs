using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackCommand : AttackCommand
{

    public NormalAttackCommand(Character owner) : base(owner){}

    public override void Attack()
    {
        if (Time.time < lastAttackTime + owner.attackDelay) return;

        lastAttackTime = Time.time;

        IDamagable target = owner.target.GetComponent<IDamagable>();
        target?.Damaged(owner.curDamage, owner.powerRate);
    }
}
