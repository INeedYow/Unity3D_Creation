using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackCommand : AttackCommand
{
    EProjectile m_eProj;
    public ProjectileAttackCommand(Character owner, EProjectile eProj) 
        : base(owner) { m_eProj = eProj; }

    public override void Attack()
    {
        if (null == owner.target) return;

        Projectile proj = null;
        switch (m_eProj)
        {
            case EProjectile.Arrow: proj = ObjectPool.instance.GetArrow(); break;
        }

        if (null == proj) return;
        proj.transform.position = owner.projectileTF.position;
        proj.Launch(owner.target, owner.curDamage, owner.powerRate, owner.aoeRange);
    }
}