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

        if (!skill.owner.nav.isStopped) { skill.owner.nav.isStopped = true; }
        
        isUsing = true;
        skill.owner.anim.SetBool(string.Format("Skill {0}", skill.ID), true);
        
        return true;
    }
}
