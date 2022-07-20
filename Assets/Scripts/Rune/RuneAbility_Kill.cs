using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAbility_Kill : RuneAbility
{
    [Range(0.01f, 0.1f)]
    public float powerGain;

    int curKillCount = 0;
    int reqKillCount = 10;

    void OnMonsterDead()
    {   
        curKillCount++;         //Debug.Log(string.Format("몬스터 처치 수 누적 {0} / {1}", curKillCount, reqKillCount));

        if (curKillCount < reqKillCount) return;

        foreach(Hero hero in PartyManager.instance.heroParty)
        {   //Debug.Log("몬스터 처치로 효과 발동 power + " + powerGain);
            hero.powerRate += powerGain;
        }

        curKillCount = 0;
    }

    public override int GetCurValue()
    {
        return Mathf.RoundToInt(powerGain * 100f);
    }

    public override void Apply()
    {   
        DungeonManager.instance.onMonsterDead += OnMonsterDead;
        curKillCount = 0;
    }

    public override void Release()
    {
        DungeonManager.instance.onMonsterDead -= OnMonsterDead;
        curKillCount = 0;
    }
}
