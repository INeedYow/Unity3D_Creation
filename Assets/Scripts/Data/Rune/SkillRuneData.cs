using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRuneData" , menuName = "RuneData/SkillRuneData")]
public class SkillRuneData : RuneData
{
    //public ESkillObj eSkillObj;
    public Skill skill;

    
    [Tooltip("몇 번째 스킬 칸에 들어갈 스킬인지")]
    [Range(1,4)] public int ID;  

    public override int GetMax()
    {
        //TODO
        return 99;
    }
    public override bool IsMax(int point)
    {
        // TODO
        return false;
    }
    public override void Apply(int point)
    {
        // TODO 던전 RuneSkill UI 갱신, 플레이어 스킬 추가
                
        //PlayerManager.instance.RuneSkillUI.AddSkill(this);
    }

    public override void Release(int point)
    {
        // 딱히 해줄 건 없는 듯?
    }

    public override int GetCurValue(int point)
    {
        return 1;
    }
    public override int GetNextValue(int point)
    {
        return 1;
    }
}
