using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Thorns : Ability
{
    public int thornsDamageMin;
    public int thornsDamageMax;
    Character m_attacker;

    public override void OnDamagedGetAttacker(Character attacker){
        if (m_attacker == attacker){                    // 기억하고 있는 공격자랑 같으면 탈출
            m_attacker = null; return;
        }

        m_attacker = attacker;                          // 반사 전에 공격자 기억
        attacker.Damaged(Random.Range(thornsDamageMin, thornsDamageMax + 1), 1f, owner, true);
        
        m_attacker = null;                              // 반사작업 후 초기화
    }

    public override bool SetOptionText1to3(int optionNumber, ItemOptionUnit optionUnit)
    {
        switch (optionNumber)
        {
            case 1:
            {
                optionUnit.option.text = "[ 반사 ] ";
                optionUnit.value.text = "착용자가 피해를 받으면";
                return true;
            }

            case 2:
            {
                optionUnit.option.text = " ";
                optionUnit.value.text = string.Format("{0} ~ {1}의 마법피해를 되돌려 줌", thornsDamageMin, thornsDamageMax);
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
