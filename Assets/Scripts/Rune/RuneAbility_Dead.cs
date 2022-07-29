using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAbility_Dead : RuneAbility
{
    void OnHeroDead()
    {   //Debug.Log("영웅 사망으로 효과 발동 : 모든 스킬 쿨타임 0");
        PlayerManager.instance.runeSkillUI.ResetCooldown();
    }

    public override int GetCurValue()
    {
        return 0;
    }

    public override void Apply()
    {
        DungeonManager.instance.onHeroDead += OnHeroDead;
    }

    public override void Release()
    {
        DungeonManager.instance.onHeroDead -= OnHeroDead;
    }
}
