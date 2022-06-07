using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Killer : Ability
{
    public float duration;
    public float damageRatio;
    EBuff m_eBuff = EBuff.Damage;
    
    public override void OnAttack(float damage){}

    public override bool SetOptionText1to3(int optionNumber, ItemOptionUnit optionUnit)
    {
        switch (optionNumber)
        {
            case 1:
            {
                optionUnit.option.text = "[ 학살 ] ";
                optionUnit.value.text = string.Format("처치 시 {0} 초간 {1:N1}% 공격력 버프 획득", duration, damageRatio * 100f);
                return true;
            }
            default : return false;
        }
    }

    public override void OnEquip(){}
    public override void OnUnEquip(){}
    public override void OnDamagedGetAttacker(Character attacker){}
    public override void OnKill()
    {
        ObjectPool.instance.GetBuff((int)m_eBuff).Add(owner, duration, damageRatio);
    }
}
