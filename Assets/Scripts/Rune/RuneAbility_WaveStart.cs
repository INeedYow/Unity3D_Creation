using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneAbility_WaveStart : RuneAbility
{
    [Range(0.01f, 0.1f)]
    public float healRatio;

    public override void OnMonsterDead(){}
    public override void OnHeroDead(){}

    public override void OnWaveStart()
    {   //Debug.Log("스테이지 시작 회복");
        foreach(Hero hero in PartyManager.instance.heroParty)
        {   
            hero.Healed(hero.maxHp * healRatio);
        }
    }

    public override int GetCurValue()
    {
        return Mathf.RoundToInt(healRatio * 100f);
    }
}
