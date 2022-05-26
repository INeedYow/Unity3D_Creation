using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetAreaAttack : SkillObject
{
    public EGroup eTargetGroup;
    
    IDamagable m_target;
    Collider[] colls;

    public override void Works()
    {
        if (skill.owner.target == null) return;

        if (eTargetGroup == EGroup.Monster)
        {
            colls = Physics.OverlapSphere(skill.owner.target.transform.position, data.area, LayerMask.GetMask("Monster"));
        }
         else{
            colls = Physics.OverlapSphere(skill.owner.target.transform.position, data.area, LayerMask.GetMask("Hero"));
        }

        //Debug.Log("SkillObj_AreaAtt : " + colls.Length);

        foreach (Collider coll in colls)
        {
            m_target = coll.gameObject.GetComponent<IDamagable>();

            if (data.isMagic)
            {
                m_target?.Damaged(data.powerRatio * skill.owner.magicDamage, skill.owner.powerRate, skill.owner, true);
            }
            else{
                m_target?.Damaged(data.powerRatio * skill.owner.curDamage, skill.owner.powerRate, skill.owner, true);
            }
            
            
        }
        FinishWorks();
    }
}
