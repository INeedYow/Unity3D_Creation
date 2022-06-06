using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAbility_Kill : RuneAbility
{
    [Range(0.001f, 0.1f)]
    public float powerGain;

    public override void OnMonsterDead()
    {   //Debug.Log("몬스터 처치로 효과 발동 power + " + powerGain);
        foreach(Hero hero in PartyManager.instance.heroParty)
        {   
            hero.powerRate += powerGain;
        }
    }


    public override void OnHeroDead(){}
    public override void OnWaveStart(){}

    public override int GetCurValue()
    {
        return Mathf.RoundToInt(powerGain * 100f);
    }
}
