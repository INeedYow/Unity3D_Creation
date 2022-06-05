using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetShoot : SkillObject
{
    public EProjectile eProjectile;
    Projectile m_proj;

    public override void Works()
    {   
        StartCoroutine("OnWorks");
    }

    IEnumerator OnWorks()
    {
        if (skill.target == null) 
        {
            FinishWorks();
            yield break;
        }

        for (int i = 0; i < data.repeat; i++)        
        {
            m_proj = ObjectPool.instance.GetProjectile((int)eProjectile);  
            m_proj.transform.position = skill.owner.projectileTF.position;

            if (skill.target == null) 
            {
                FinishWorks();
                yield break;
            }

            if (data.isMagic)           
            {
                m_proj.Launch(
                    skill.target, 
                    skill.owner,
                    skill.owner.magicDamage * data.powerRatio, 
                    skill.owner.powerRate, 
                    data.area,
                    data.eTargetEffect,
                    data.eBuff,
                    data.duration,
                    data.buffRatio);
            }
            
            else                        
            {
                m_proj.Launch(
                    skill.target,
                    skill.owner,
                    skill.owner.curDamage * data.powerRatio,
                    skill.owner.powerRate,
                    data.area,
                    data.eTargetEffect,
                    data.eBuff,
                    data.duration,
                    data.buffRatio);
            }

            if (data.eUserEffect != EEffect.None)
            {    
                eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                eff.SetPosition(skill.owner);
            }


            yield return new WaitForSeconds(data.interval);
        }
       
        // 투사체 맞을 때 버프 적용돼야 함
        //if (skill.target != null) AddBuff(skill.target);

        FinishWorks();
    }
}
