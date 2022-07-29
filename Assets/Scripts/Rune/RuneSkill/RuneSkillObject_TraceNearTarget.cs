using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillObject_TraceNearTarget : RuneSkillObject
{
    public EGroup eTargetGroup;
    public float speed;
    public ParticleSystem particle;
    Collider[] m_colls;
    Vector3 m_moveVec;
    Character m_target;
    float m_sqrDist;
    float m_value;

    private void OnDisable() {
        if (particle != null)
        {
            particle.Stop();
        }
        StopCoroutine("Damage");
        StopCoroutine("SetDestination");
    }

    public override void Works()
    {
        transform.position = DungeonManager.instance.worldEffectTF.position;
        if (particle != null)
        {
            particle.Play();
        }
        StartCoroutine("Damage");
        StartCoroutine("SetDestination");
        Invoke("FinishWorks", data.duration);
    }

    private void FixedUpdate() {
        Trace();
    }

    IEnumerator SetDestination()
    {
        while (true)
        {
            if (eTargetGroup == EGroup.Hero)
            {   // 아군
                m_target = null;
            
                m_value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;
                    m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                    if (m_value > m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        m_target = ch;
                    }
                }
            
            }
            else{   // 적군
                m_target = null;

                m_value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;
                    m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;
                    if (m_value > m_sqrDist)
                    {
                        m_value = m_sqrDist;
                        m_target = ch;
                    }
                }
            
            }

            if (m_target == null)
            {
                m_moveVec = DungeonManager.instance.worldEffectTF.position - transform.position;
            }
            else{
                m_moveVec = m_target.transform.position - transform.position;
            }
        
            yield return new WaitForSeconds(0.7f);
        }
    }

    void Trace()
    {   
        transform.Translate(m_moveVec * Time.deltaTime * speed);
    }

    IEnumerator Damage()
    {
        while (true)
        {
            if (eTargetGroup == EGroup.Hero)
            {   // 아군
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;

                    m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;

                    if (data.area * data.area >= m_sqrDist)
                    {
                        ch.Damaged(data.power);

                        if (data.eOnWorksEffect != EEffect.None)
                        {
                            eff = ObjectPool.instance.GetEffect((int)data.eOnWorksEffect);
                            eff.transform.position = ch.targetTF.position;
                        }
                    }
                }
            }
            else{   // 적군
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {   
                    if (ch.isDead || ch.isStop) continue;
                
                    m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;

                    if (data.area * data.area >= m_sqrDist)
                    {
                        ch.Damaged(data.power);

                         if (data.eOnWorksEffect != EEffect.None)
                        {
                            eff = ObjectPool.instance.GetEffect((int)data.eOnWorksEffect);
                            eff.transform.position = ch.targetTF.position;
                        }
                    }

                }
            }



            yield return new WaitForSeconds(data.repeatInterval);
        }
        
    }
}