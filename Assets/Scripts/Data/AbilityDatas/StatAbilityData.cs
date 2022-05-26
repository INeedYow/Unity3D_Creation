using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStat {
    Damage, Magic, Armor, MagicArmor,
}

public class StatAbilityData : AbilityData
{
    public EStat eStat;
    public float[] statValues;

    public override void Apply(int level)
    {
        foreach (Hero hero in PartyManager.instance.heroParty)
        {
            switch (eStat)
            {
                case EStat.Damage: 
                {
                    hero.minDamage += statValues[level - 1];
                    hero.maxDamage += statValues[level - 1];
                    break;
                }

                case EStat.Magic        : hero.magicDamage += statValues[level - 1];    break;
                case EStat.Armor        : hero.armorRate += statValues[level - 1];      break;
                case EStat.MagicArmor   : hero.magicArmorRate += statValues[level - 1]; break;
            }
        }
    }

    public override void Release(int level)
    {
        foreach (Hero hero in PartyManager.instance.heroParty)
        {
            switch (eStat)
            {
                case EStat.Damage: 
                {
                    hero.minDamage -= statValues[level - 1];
                    hero.maxDamage -= statValues[level - 1];
                    break;
                }

                case EStat.Magic        : hero.magicDamage -= statValues[level - 1];    break;
                case EStat.Armor        : hero.armorRate -= statValues[level - 1];      break;
                case EStat.MagicArmor   : hero.magicArmorRate -= statValues[level - 1]; break;
            }
        }
    }
}
