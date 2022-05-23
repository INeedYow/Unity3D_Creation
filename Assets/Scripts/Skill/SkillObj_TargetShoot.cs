using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetShoot : SkillObject
{
    public EProjectile eProjectile;
    Projectile m_proj;

    public override void Works()
    {
        m_proj = ObjectPool.instance.GetProjectile((int)eProjectile);
        
        for (int i = 0; i < data.count; i++)
        {
            if (data.isMagic)
            {
                m_proj.Launch(
                    skill.owner.target, 
                    skill.owner.magicDamage * data.powerRatio, 
                    skill.owner.powerRate, 
                    data.area);
            }
            else
            {
                m_proj.Launch(
                    skill.owner.target,
                    skill.owner.curDamage * data.powerRatio,
                    skill.owner.powerRate,
                    data.area);
            }
        
        }

        Return();
    }
}
