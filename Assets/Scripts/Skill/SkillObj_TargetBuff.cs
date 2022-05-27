using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_TargetBuff : SkillObject
{
    public EBuff eBuff;
    Buff m_buff;

    public override void Works()
    {
        if (skill.owner.target != null)
        {   //Debug.Log("(int)eBuff : " + (int)eBuff + " // " + "eBuff : " + eBuff);
            m_buff = ObjectPool.instance.GetBuff((int)eBuff);
            m_buff.Add(skill.owner.target, data.duration, data.buffRatio);
        }
        
        FinishWorks();
    }
}
