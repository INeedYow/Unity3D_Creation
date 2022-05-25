using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Thorns : Ability
{
    public int thornsDamageMin;
    public int thornsDamageMax;

    public override void OnDamagedGetAttacker(Character attacker){
        attacker.Damaged(Random.Range(thornsDamageMin, thornsDamageMax + 1), 1f, owner, true, true);
    }

    public override bool SetOptionText1to3(int optionNumber, ItemOptionUnit optionUnit)
    {
        switch (optionNumber)
        {
            case 1:
            {
                optionUnit.option.text = "[ 반사 ] ";
                optionUnit.value.text = string.Format("피해를 입으면 {0} ~ {1}의 피해를 되돌려 줌", thornsDamageMin, thornsDamageMax);
                return true;
            }
            
            default : return false;
        }
    }

    public override void OnAttack(float damage){}
    public override void OnEquip(){}
    public override void OnUnEquip(){}    
    public override void OnKill(){}
}
