using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand_Instant : SkillCommand
{
    public SkillCommand_Instant(Skill skill) : base(skill){}
    
    public override bool Use()
    {
        if (isUsing) return true;
        if (Time.time < lastSkillTime + skill.data.cooldown) return false;

        isUsing = true;
        skill.owner.anim.SetBool(string.Format("Skill {0}", skill.ID), true);
        Debug.Log(skill.ID + "번 째 스킬, ["+ skill.data.skillName +"] 사용");
        return true;
    }
}
