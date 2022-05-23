using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackCommand : AttackCommand
{
    EProjectile m_eProj;
    Projectile proj;
    public ProjectileAttackCommand(Character owner, EProjectile eProj) 
        : base(owner) { m_eProj = eProj; }

    public override void Attack()
    {
        if (null == owner.target) return;

        proj = null;
        
        proj = ObjectPool.instance.GetProjectile((int)m_eProj);

        if (null == proj) return;
        proj.transform.position = owner.projectileTF.position;
        proj.Launch(owner.target, owner.curDamage, owner.powerRate, owner.aoeRange);
    }
}