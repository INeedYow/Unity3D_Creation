using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand_Instant : SkillCommand
{
    public SkillCommand_Instant(Skill skill) : base(skill){}
    
    public override void Use()
    {   
        if (isUsing) return;
        
        if (!skill.owner.nav.isStopped) { skill.owner.nav.isStopped = true; }
        
        isUsing = true;
        skill.owner.anim.SetBool(string.Format("Skill {0}", skill.ID), true);
    }
}
