using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAbility_Dead : RuneAbility
{
    public override void OnMonsterDead() {}
    public override void OnHeroDead()
    {   //Debug.Log("영웅 사망으로 효과 발동 : 모든 스킬 쿨타임 0");
        PlayerManager.instance.runeSkillUI.ResetCooldown();
    }
    public override void OnWaveStart(){}

    public override int GetCurValue()
    {
        return 0;
    }
}
