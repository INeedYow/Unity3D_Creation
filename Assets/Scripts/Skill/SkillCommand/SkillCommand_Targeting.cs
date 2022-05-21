using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand_Targeting : SkillCommand
{
    public SkillCommand_Targeting(Skill skill) : base(skill){}
   
    public override bool Use()
    {
        //Debug.Log("1 " + isUsing);
        if (isUsing) return true;
        //Debug.Log("2");
        if (Time.time < lastSkillTime + skill.data.cooldown || skill.owner.target == null) return false;
        //Debug.Log("3 " + Time.time + " / " + lastSkillTime + " / " + skill.data.cooldown);
        if (!skill.owner.IsTargetInRange(skill.data.skillRange)) { 
            skill.owner.MoveToTarget();
            return true;
        }
        //Debug.Log("isUsing = true");
        isUsing = true;
        skill.owner.anim.SetBool(string.Format("Skill {0}", skill.ID), true);
        //Debug.Log(skill.ID + "번 째 스킬, ["+ skill.data.skillName +"] 사용");
        return true;
    }
}
