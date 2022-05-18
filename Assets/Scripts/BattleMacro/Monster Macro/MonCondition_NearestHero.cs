using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonCondition_NearestHero : ConditionMacro
{
    float m_dist;
    float m_sqrDist;
    Hero m_target;

    public override bool IsSatisfy()
    {   // 타겟이 없을 때 찾기
        if (owner.target == null) owner.target = FindHero();
        return true;
    }

    Character FindHero(){
        m_dist = Mathf.Infinity;
        m_target = null;
        foreach (Hero hero in HeroManager.instance.heroList)
        {
            if (hero.isDead) continue;
            m_sqrDist = (hero.transform.position - owner.transform.position).sqrMagnitude;
            if (m_dist > m_sqrDist)
            {
                m_dist = m_sqrDist;
                m_target = hero;
            }
        }
        return m_target;
    }
}
