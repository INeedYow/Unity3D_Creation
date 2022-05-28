using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetAreaAttack : SkillObject
{
    public EGroup eTargetGroup;
    IDamagable m_target;
    
    // Collider[] colls; 

    float m_sqrDist;
    //float m_sqrArea;

    //private void Start() { m_sqrArea = data.area * data.area;  Debug.Log("data.area : " + data.area + " / m_sqr : " + m_sqrArea); }

    public override void Works()
    {
        // before
        // if (skill.owner.target == null) return;

        // if (eTargetGroup == EGroup.Monster)
        // {
        //     colls = Physics.OverlapSphere(skill.owner.target.transform.position, data.area, LayerMask.GetMask("Monster"));
        // }
        // else{
        //     colls = Physics.OverlapSphere(skill.owner.target.transform.position, data.area, LayerMask.GetMask("Hero"));
        // }

        // //Debug.Log("SkillObj_AreaAtt : " + colls.Length);

        // foreach (Collider coll in colls)
        // {
        //     m_target = coll.gameObject.GetComponent<IDamagable>();

        //     if (data.isMagic)
        //     {
        //         m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
        //     }
        //     else{
        //         m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
        //     }
            
            
        // }
        // FinishWorks();

        // after
        if (eTargetGroup == EGroup.Monster)
        {
            foreach (Monster mon in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (mon.isDead) continue;
                if (skill.owner.target == null) break;

                m_sqrDist = (skill.owner.target.transform.position - mon.transform.position).sqrMagnitude;

                // if (m_sqrArea < m_sqrDist) continue;
                if (data.area * data.area < m_sqrDist) continue;

                m_target = mon.GetComponent<IDamagable>();

                if (data.isMagic)
                {
                    m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                }
                else{
                    m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                }
                Buff buff = ObjectPool.instance.GetBuff((int)data.eBuff);
                buff?.Add(mon, data.duration, data.buffRatio);
            }
        }
        else
        {
            foreach (Hero hero in PartyManager.instance.heroParty)
            {
                if (hero.isDead) continue;
                
                m_sqrDist = (skill.owner.target.transform.position - hero.transform.position).sqrMagnitude;

                // if (m_sqrArea < m_sqrDist) continue;
                if (data.area * data.area < m_sqrDist) continue;

                m_target = hero.GetComponent<IDamagable>();

                if (data.isMagic)
                {
                    m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                }
                else{
                    m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                }
                Buff buff = ObjectPool.instance.GetBuff((int)data.eBuff);
                buff?.Add(hero, data.duration, data.buffRatio);
            }
        }
    }
}
