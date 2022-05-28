using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRuneData : RuneData
{
    //public ESkillObj eSkillObj;
    public Skill skill;

    public override bool IsMax(int point)
    {
        // TODO
        return true;
    }
    public override void Apply(int point)
    {
        // TODO 던전 RuneSkill UI에 해당 스킬 추가
    }

    public override void Release(int point)
    {
        // 딱히 해줄 건 없는 듯?
    }
}
