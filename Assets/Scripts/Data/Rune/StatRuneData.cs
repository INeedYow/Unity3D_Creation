using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStat {
    Damage, Magic, Armor, MagicArmor, 
}
public class StatRuneData : RuneData
{
    public EStat eStat;
    public int[] values;


    public override bool IsMax(int point)
    {
        return point == values.Length;
    }
    
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
                //
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
                //
            }
        }
    }
}
