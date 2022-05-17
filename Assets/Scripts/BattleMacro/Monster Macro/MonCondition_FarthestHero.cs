using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonCondition_FarthestHero : ConditionMacro
{
    float m_dist;
    float m_sqrDist;
    Hero m_target;

    public override bool IsSatisfy()
    {   
        if (owner.target == null) owner.target = FindHero();
        return true;
    }

    Character FindHero(){
        m_dist = 0f;
        m_target = null;
        foreach (Hero hero in HeroManager.instance.heroList)
        {
            if (hero.isDead) continue;
            m_sqrDist = (hero.transform.position - owner.transform.position).sqrMagnitude;
            if (m_dist < m_sqrDist)
            {
                m_dist = m_sqrDist;
                m_target = hero;
            }
        }
        return m_target;
    }
}