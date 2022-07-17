using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Killer : Ability
{
    public float duration;
    public float damageRatio;
    EBuff m_eBuff = EBuff.Damage;

    public override void OnEquip(Character owner)
    {
        this.owner = owner;
        
        if (null == owner) return;

        owner.onKill += OnKill;
    }

    protected override void OnUnEquip()
    {
        if (null == owner) return;

        owner.onKill -= OnKill;
    }

    void OnKill()
    {
        ObjectPool.instance.GetBuff((int)m_eBuff).Add(owner, duration, damageRatio);
    }


    

    // public override void OnKill()
    // {
    //     ObjectPool.instance.GetBuff((int)m_eBuff).Add(owner, duration, damageRatio);
    // }
    // public override void OnAttackGetDamage(float damage){}
    // public override void OnDamagedGetAttacker(Character attacker){}
    

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

}
