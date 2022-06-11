using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetCatch : SkillObject
{
    Character m_target;
    float m_sqrDist;
    Vector3 m_moveVec;
    Buff m_stun;
    float m_moveSpeed = 1.5f;

    public override void Works()
    {
        if (skill.target == null)
        {
            FinishWorks();
        }
        m_target = skill.target;
        StartCoroutine("OnWorks");
    }

    void EarlyQuit()
    {
        StopCoroutine("OnWorks");
        m_target.onDead -= EarlyQuit;
        m_target.onDead -= EarlyQuit;

        m_stun?.Remove();
        m_stun = null;

        FinishWorks();
    }

    IEnumerator OnWorks()
    {
        m_target.onDead += EarlyQuit;

        skill.owner.onDead += EarlyQuit;

        m_sqrDist = (skill.owner.transform.position - m_target.transform.position).sqrMagnitude;

        m_stun = ObjectPool.instance.GetBuff((int)EBuff.Stun);
        m_stun?.Add(m_target, 99f, 0f);

        while (m_sqrDist > 4f)
        {   
            if (!m_target.nav.isOnNavMesh || m_target.isDead || m_target.isStop)
            {
                FinishWorks();
                yield break;
            }

            m_sqrDist = (skill.owner.transform.position - m_target.transform.position).sqrMagnitude;

            m_moveVec = skill.owner.transform.position - m_target.transform.position;
            m_target.nav.Move(m_moveVec * m_moveSpeed * 0.1f);

            yield return new WaitForSeconds(0.1f);
        }

        m_stun?.Remove();
        m_stun = null;

        if (skill.owner.eGroup != m_target.eGroup)
        {   // 대상이 적인 경우 피해
            if (data.isMagic)
            {
                m_target.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
            }
            else{
                m_target.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
            }

            if (!m_target.isDead || !m_target.isStop)
            {
                if (data.eTargetEffect != EEffect.None)
                {
                    eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                    eff.SetPosition(m_target);
                }

                AddBuff(m_target);
            }
        }


        if (data.eUserEffect != EEffect.None)
        {
            eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
            eff.SetPosition(skill.owner);
        }

        
        m_target.onDead -= EarlyQuit;
        skill.owner.onDead -= EarlyQuit;
            
        FinishWorks();
    }

}
