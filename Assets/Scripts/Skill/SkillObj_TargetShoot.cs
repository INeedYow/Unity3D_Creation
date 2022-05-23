using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetShoot : SkillObject
{
    public EProjectile eProjectile;
    Projectile m_proj;

    public override void Works()
    {   
        m_proj = ObjectPool.instance.GetProjectile((int)eProjectile);   //Debug.Log("proj : " + m_proj);
        m_proj.transform.position = skill.owner.projectileTF.position;
        
        for (int i = 0; i < data.count; i++)        //Debug.Log("data.count : " + data.count);
        {
            if (data.isMagic)           //Debug.Log("ismagic");
            {
                if (skill.owner.target == null) return;
                m_proj.Launch(
                    skill.owner.target, 
                    skill.owner.magicDamage * data.powerRatio, 
                    skill.owner.powerRate, 
                    data.area);
            }
            
            else                        //Debug.Log("else");
            {
                if (skill.owner.target == null) return;
                m_proj.Launch(
                    skill.owner.target,
                    skill.owner.curDamage * data.powerRatio,
                    skill.owner.powerRate,
                    data.area);
            }
        
        }

        FinishWorks();
    }
}
