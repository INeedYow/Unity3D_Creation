using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRuneData" , menuName = "RuneData/SkillRuneData")]
public class SkillRuneData : RuneData
{
    public Sprite icon;
    [Range(1, 100)]public int cooldown;

    
    [Tooltip("몇 번째 스킬 칸에 들어갈 스킬인지")]
    [Range(1,4)] public int ID;  

    public override int GetMax()
    {   // 포인트 투자 개념이 아니라 스킬 선택 하는 식으로 해서 함수이름이나 의도가 안 맞아짐
        return cooldown;
    }
    public override bool IsMax(int point)
    {
        // TODO
        return true;
    }
    public override void Apply(int point)
    {  
        PlayerManager.instance.runeSkillUI.SetInfo(ID, this);
    }

    public override void Release(int point)
    {
        PlayerManager.instance.runeSkillUI.SetSkillUnit(ID, false);
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
