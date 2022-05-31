using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRuneData" , menuName = "RuneData/SkillRuneData")]
public class SkillRuneData : RuneData
{
    public RuneCursor cursor;

    
    [Tooltip("몇 번째 스킬 칸에 들어갈 스킬인지")]
    [Range(1,4)] public int ID;  

    public override int GetMax()
    {   // 원래랑 기획이 달라져서 스킬은 쿨타임, 데미지나 지속시간 같은 밸류값 보여주면 될 것 같음
        //TODO
        return 99;
    }
    public override bool IsMax(int point)
    {
        // TODO
        return true;
    }
    public override void Apply(int point)
    {
        // TODO 던전 RuneSkill UI 갱신, 플레이어 스킬 추가
                
        // PlayerManager.instance.runeSkillUI.SetSkillUnit(
        //     ID - 1,?
        // );
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
