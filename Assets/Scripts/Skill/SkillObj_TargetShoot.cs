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

            if (skill.target == null) break;

            if (data.isMagic)           
            {
                m_proj.Launch(
                    skill.target, 
                    skill.owner,
                    skill.owner.magicDamage * data.powerRatio, 
                    skill.owner.powerRate, 
                    data.area);
            }
            
            else                        
            {
                m_proj.Launch(
                    skill.target,
                    skill.owner,
                    skill.owner.curDamage * data.powerRatio,
                    skill.owner.powerRate,
                    data.area);
            }

            if (data.eUserEffect != EEffect.None)
            {   // effect에서 따라다닐지, 높이 등 정보 가지고 있음
                eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                //eff.transform.position = skill.owner.transform.position;
                eff.SetPosition(skill.owner);
            }

            if (data.eTargetEffect != EEffect.None)
            {
                eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                eff.SetPosition(skill.target);
            }
        
        }
       
        if (skill.target != null) AddBuff(skill.target);
    }
}
