using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Drain : Ability
{
    [Range(0.01f, 1f)] public float drainRatio;
    
    public override void OnAttack(float damage)
    {   
        owner.Healed(damage * drainRatio);
    }

    public override bool SetOptionText1to3(int optionNumber, ItemOptionUnit optionUnit)
    {
        switch (optionNumber)
        {
            case 1:
            {
                optionUnit.option.text = "[ 흡혈 ] ";
                optionUnit.value.text = string.Format("입힌 피해의 {0}% 만큼 회복", Mathf.RoundToInt(drainRatio * 100f));
                return true;
            }
            default : return false;
        }
    }

    public override void OnDamagedGetAttacker(Character attacker){}
    public override void OnKill(){}

}
