using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetShoot : SkillObject
{
    public EProjectile eProjectile;
    Projectile m_proj;

    public override void Works()
    {   
        for (int i = 0; i < data.count; i++)        
        {
            m_proj = ObjectPool.instance.GetProjectile((int)eProjectile);  
            m_proj.transform.position = skill.owner.projectileTF.position;

            if (skill.owner.target == null) break;

            if (data.isMagic)           
            {
                m_proj.Launch(
                    skill.owner.target, 
                    skill.owner,
                    skill.owner.magicDamage * data.powerRatio, 
                    skill.owner.powerRate, 
                    data.area);
            }
            
            else                        
            {
                m_proj.Launch(
                    skill.owner.target,
                    skill.owner,
                    skill.owner.curDamage * data.powerRatio,
                    skill.owner.powerRate,
                    data.area);
            }
        
        }
       
        if (skill.owner.target != null) 
            AddBuff(skill.owner.target);
    }
}
