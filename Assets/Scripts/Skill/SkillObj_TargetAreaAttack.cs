using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetAreaAttack : SkillObject
{
    public EGroup eTargetGroup;
    IDamagable m_target;
    
    // Collider[] colls; 

    float m_sqrDist;

    //private void Start() { m_sqrArea = data.area * data.area;  Debug.Log("data.area : " + data.area + " / m_sqr : " + m_sqrArea); }

    public override void Works()
    {
        // before // Overlap 게임 컨셉상 다른 공격들 모두 충돌로 처리하지 않고, 버프 추가하려면 charater로 쓰는 게 좋아보임 (IDamagable에 버프를 넣어도 되긴 할듯)
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
                if (skill.target == null) break;

                m_sqrDist = (skill.target.transform.position - mon.transform.position).sqrMagnitude;

                if (data.area * data.area < m_sqrDist) continue;

                m_target = mon.GetComponent<IDamagable>();

                if (data.isMagic)
                {
                    m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                }
                else{
                    m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                }

                AddBuff(mon);
            }
        }
        else
        {
            foreach (Hero hero in PartyManager.instance.heroParty)
            {
                if (hero.isDead) continue;
                
                m_sqrDist = (skill.target.transform.position - hero.transform.position).sqrMagnitude;

                if (data.area * data.area < m_sqrDist) continue;

                m_target = hero.GetComponent<IDamagable>();

                if (data.isMagic)
                {
                    m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
                }
                else{
                    m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, false);
                }

                AddBuff(hero);
            }
        }
    }
}
