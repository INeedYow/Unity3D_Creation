using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand_Targeting : SkillCommand
{
    public SkillCommand_Targeting(Skill skill) : base(skill){}
   
    public override void Use()
    {
        if (isUsing) return;

        if (skill.target == null) return;

        if (!skill.owner.nav.isOnNavMesh) return;

        if (!skill.owner.IsTargetInRange(skill.target, skill.data.skillRange))
        { 
            skill.owner.MoveToTarget(skill.target);
        }
        else
        {
            if (!skill.owner.nav.isStopped) { skill.owner.nav.isStopped = true; }

            skill.owner.ResetNavDistance();

            isUsing = true;
            skill.owner.anim.SetBool(string.Format("Skill {0}", skill.ID), true);

            DungeonManager.instance.onWaveEnd += skill.QuitSkill;
        }
        
    }
}
