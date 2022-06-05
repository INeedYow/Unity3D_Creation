using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillObj_Fairy : SkillObject
{
    public EGroup eTargetGroup;
    public EProjectile eProjectile;
    public ParticleSystem fairyParticle;

    public float rotRange;
    public float rotSpeed;

    Projectile m_proj;
    float m_sqrRange;
    float m_sqrDist;
    
    public override void Works()
    {
        skill.owner.onDead += OnDead;

        m_sqrRange = data.area * data.area;

        transform.SetParent(null);
        fairyParticle.Play();
        StartCoroutine("Move");
        StartCoroutine("OnWorks");
    }

    void OnDead()
    {
        skill.owner.onDead -= OnDead;
        fairyParticle.Stop();
        transform.SetParent(skill.transform);
        FinishWorks();
    }

    IEnumerator Move()
    {
        //float y = 0f;

        transform.position = skill.owner.HpBarTF.position + Vector3.right * rotRange;

        while (true)
        {
            transform.RotateAround(skill.owner.HpBarTF.position, Vector3.up, rotSpeed * Time.deltaTime);

            yield return null;
        }
    }

    IEnumerator OnWorks()
    {
        for (int i = 0; i < data.repeat; i++)
        {
            if (eTargetGroup == EGroup.Monster)
            {
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;

                    m_sqrDist = (ch.transform.position - skill.owner.transform.position).sqrMagnitude;

                    if (m_sqrDist > m_sqrRange) continue;

                    m_proj = m_proj = ObjectPool.instance.GetProjectile((int)eProjectile);  
                    m_proj.transform.position = transform.position;

                    m_proj.Launch(
                        ch,
                        skill.owner,
                        data.isMagic ? skill.owner.magicDamage * data.powerRatio : skill.owner.curDamage * data.powerRatio,
                        skill.owner.powerRate,
                        0f,
                        data.eTargetEffect,
                        data.eBuff,
                        data.duration,
                        data.buffRatio
                    );

                    break;
                }
            }   
            else{
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;

                    m_sqrDist = (ch.transform.position - skill.owner.transform.position).sqrMagnitude;

                    if (m_sqrDist > m_sqrRange) continue;

                    m_proj = m_proj = ObjectPool.instance.GetProjectile((int)eProjectile);  
                    m_proj.transform.position = transform.position;

                    m_proj.Launch(
                        ch,
                        skill.owner,
                        data.isMagic ? skill.owner.magicDamage * data.powerRatio : skill.owner.curDamage * data.powerRatio,
                        skill.owner.powerRate,
                        0f,
                        data.eTargetEffect,
                        data.eBuff,
                        data.duration,
                        data.buffRatio
                    );

                    break;
                }
            }

            yield return new WaitForSeconds(data.interval);
        }

        fairyParticle.Stop();
        transform.SetParent(skill.transform);
        StopCoroutine("Move");
        FinishWorks();
    }
}
