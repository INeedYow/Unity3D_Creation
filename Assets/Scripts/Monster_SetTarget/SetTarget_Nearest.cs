using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTarget_Nearest : SetTarget
{
    float m_dist;
    float m_sqrDist;

    public SetTarget_Nearest(Character owner) : base(owner){}
    public override Character GetTarget()
    {
        return Find();
    }

    Character Find(){
        m_dist = Mathf.Infinity;
        target = null;
        foreach (Character ch in HeroManager.instance.heroList)
        {
            if (ch.isDead) continue;
            m_sqrDist = (ch.transform.position - owner.transform.position).sqrMagnitude;
            if (m_dist > m_sqrDist)
            {
                m_dist = m_sqrDist;
                target = ch;
            }
        }
        return target;
    }
}
