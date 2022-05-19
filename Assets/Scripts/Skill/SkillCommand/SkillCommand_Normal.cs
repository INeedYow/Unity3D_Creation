using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand_Normal : SkillCommand
{
    public override bool Use()
    {
        if (isUsing) return true;
        if (Time.time < lastSkillTime + skill.data.cooldown) return false;

        // TODO anim 스킬 종료 시 lastSkillTime = Time.time;, isUsing = false; 해주기
        isUsing = true;
        skill.owner.anim.SetTrigger(string.Format("Skill {0}", skill.data.ID));
        return true;
    }
}
