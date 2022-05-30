using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStat {
    Damage, Magic, Armor, MagicArmor, Hp, 
    Cri_Chance, Cri_Power, Dodge,
    Range, MoveSpeed, 
}

[CreateAssetMenu(fileName = "StatRuneData" , menuName = "RuneData/StatRuneData")]
public class StatRuneData : RuneData
{
    public EStat eStat;
    public int[] values;

    public override int GetMax() { return values.Length; }
    public override bool IsMax(int point) { return point == values.Length; }

    public override void Apply(int point)
    {
        foreach (Hero hero in PartyManager.instance.heroParty)
        {
            switch(eStat)
            {
                case EStat.Damage:
                {
                    hero.minDamage += values[point - 1];
                    hero.maxDamage += values[point - 1];
                    break;
                }
                case EStat.Armor: hero.armorRate += values[point - 1] * 0.01f; break;
                case EStat.Magic: hero.magicDamage += values[point - 1]; break;
                case EStat.MagicArmor: hero.magicArmorRate += values[point - 1] * 0.01f; break;
                case EStat.Hp: 
                {
                    hero.maxHp += values[point - 1]; 
                    hero.curHp = hero.maxHp;
                    break;
                }

                case EStat.Cri_Chance: hero.criticalChance += values[point - 1]; break;
                case EStat.Cri_Power: hero.criticalRate += values[point - 1] * 0.01f; break;
                case EStat.Dodge: hero.criticalChance += values[point - 1]; break;

                case EStat.Range: hero.attackRange += values[point - 1]; break;
                case EStat.MoveSpeed: hero.moveSpeed += values[point - 1]; break;
            }
        }
    }

    public override void Release(int point)
    {
         foreach (Hero hero in PartyManager.instance.heroParty)
        {
            switch(eStat)
            {
                case EStat.Damage:
                {
                    hero.minDamage -= values[point - 1];
                    hero.maxDamage -= values[point - 1];
                    break;
                }
                case EStat.Armor: hero.armorRate -= values[point - 1] * 0.01f; break;
                case EStat.Magic: hero.magicDamage -= values[point - 1]; break;
                case EStat.MagicArmor: hero.magicArmorRate -= values[point - 1] * 0.01f; break;
                case EStat.Hp: 
                {
                    hero.maxHp -= values[point - 1]; 
                    hero.curHp = hero.maxHp;
                    break;
                }

                case EStat.Cri_Chance: hero.criticalChance -= values[point - 1]; break;
                case EStat.Cri_Power: hero.criticalRate -= values[point - 1] * 0.01f; break;
                case EStat.Dodge: hero.criticalChance -= values[point - 1]; break;

                case EStat.Range: hero.attackRange -= values[point - 1]; break;
                case EStat.MoveSpeed: hero.moveSpeed -= values[point - 1]; break;
            }
        }
    }

    public override int GetCurValue(int point)
    {
        if (point == 0) return 0;
        return values[point - 1];
    }
    public override int GetNextValue(int point)
    {
        if (point == values.Length) return 0;
        return values[point];

    }
}
