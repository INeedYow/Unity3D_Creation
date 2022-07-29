using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillObj_TargetBuff : RuneSkillObject
{
    Character m_target;

    public void SetTarget(Character target)
    {
        m_target = target;
    }

    public override void Works()
    {
        if (m_target != null)
        {
            AddBuff(m_target);
        }

        FinishWorks();
    }
}
