using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindTarget_Most : Condition_FindTarget
{
    public enum EMost { Least, Most };

    public EMost eMost;

    float m_temp;

    public Condition_FindTarget_Most(string desc, Hero hero, EGroup eGroup, EValue eValue, EMost eMost) 
        : base(desc, hero, eGroup, eValue)
    {
        this.eMost = eMost;
    }

    // TODO 각 함수 제대로 작동하는지 확인
    public override bool IsSatisfy()
    {
        Debug.Log("Condition_FindTarget_Most.IsSatisfy()");
        switch (eValue)
        {
        case EValue.HP:
            Debug.Log("EValue.HP:");
            owner.target = FindTargetHP();
            break;

        case EValue.Power:
            owner.target = FindTargetPower();
            break;
            
        case EValue.Armor:
            owner.target = FindTargetArmor();
            break;
        
        case EValue.MagicPower:
            owner.target = FindTargetMagicPower();
            break;

        case EValue.MagicArmor:
            owner.target = FindTargetMagicArmor();
            break;

        case EValue.AttackRange:
            owner.target = FindTargetAttackRange();
            break;

        case EValue.AttackSpeed:
            owner.target = FindTargetAttackSpeed();
            break;
        
        case EValue.Distance:
            owner.target = FindTargetDistance();
            break;

        case EValue.MoveSpeed:
            owner.target = FindTargetMoveSpeed();
            break;
        }
        
        if (owner.target != null) return true;
        return false;
    }

    // TODO 살아있는 아군, 적군만 보관하는 배열 추가해야 할듯
        // or isDead 체크하고 continue;
    Character FindTargetHP()
    {   // HP비율
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;

            if (eMost == EMost.Least)
            {   // 최소
                value = 1f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.curHp / ch.maxHp)
                    {
                        value = ch.curHp / ch.maxHp;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.curHp / ch.maxHp)
                    {
                        value = ch.curHp / ch.maxHp;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
            target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;

            if (eMost == EMost.Least)
            {   // 최소
                value = 1f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.curHp / ch.maxHp)
                    {
                        value = ch.curHp / ch.maxHp;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.curHp / ch.maxHp)
                    {
                        value = ch.curHp / ch.maxHp;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetPower()
    {   //최소 최대 공격력 평균
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;

            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.minDamage + ch.maxDamage)
                    {
                        value = ch.minDamage + ch.maxDamage;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.minDamage + ch.maxDamage)
                    {
                        value = ch.minDamage + ch.maxDamage;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;

            if (eMost == EMost.Least)
            {   // 최소 최대 공격력 평균이 최소
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.minDamage + ch.maxDamage)
                    {
                        value = ch.minDamage + ch.maxDamage;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.minDamage + ch.maxDamage)
                    {
                        value = ch.minDamage + ch.maxDamage;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetArmor()
    {   // 아머
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.armorRate)
                    {
                        value = ch.armorRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.armorRate)
                    {
                        value = ch.armorRate;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.armorRate)
                    {
                        value = ch.armorRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.armorRate)
                    {
                        value = ch.armorRate;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetMagicPower()
    {   // 마법공격력
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.magicPowerRate)
                    {
                        value = ch.magicPowerRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.magicPowerRate)
                    {
                        value = ch.magicPowerRate;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.magicPowerRate)
                    {
                        value = ch.magicPowerRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.magicPowerRate)
                    {
                        value = ch.magicPowerRate;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetMagicArmor()
    {   // 마법방어력
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.magicArmorRate)
                    {
                        value = ch.magicArmorRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.magicArmorRate)
                    {
                        value = ch.magicArmorRate;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.magicArmorRate)
                    {
                        value = ch.magicArmorRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.magicArmorRate)
                    {
                        value = ch.magicArmorRate;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetAttackRange()
    {   // 사거리
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.attackRange)
                    {
                        value = ch.attackRange;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.attackRange)
                    {
                        value = ch.attackRange;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.attackRange)
                    {
                        value = ch.attackRange;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.attackRange)
                    {
                        value = ch.attackRange;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetAttackSpeed()
    {   // 공속
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 공속 최소 : delay가 가장 큰 경우 
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.attackDelay)
                    {
                        value = ch.attackDelay;
                        target = ch;
                    }
                }
            }
            else{   // 공속 최대 : delay가 가장 작은 경우
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.attackDelay)
                    {
                        value = ch.attackDelay;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.attackDelay)
                    {
                        value = ch.attackDelay;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.attackDelay)
                    {
                        value = ch.attackDelay;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetDistance()
    {   // 거리 // TODO
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (value * value > m_temp)
                    {
                        value = m_temp;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (value * value < m_temp)
                    {
                        value = m_temp;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (value * value > m_temp)
                    {
                        value = m_temp;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
                    if (value * value < m_temp)
                    {
                        value = m_temp;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }

    Character FindTargetMoveSpeed()
    {   // 사거리
        if (eGroup == EGroup.Ally)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value > ch.moveSpeed)
                    {
                        value = ch.moveSpeed;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (value < ch.moveSpeed)
                    {
                        value = ch.moveSpeed;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
           target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return null;
            
            if (eMost == EMost.Least)
            {   // 최소
                value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value > ch.moveSpeed)
                    {
                        value = ch.moveSpeed;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (value < ch.moveSpeed)
                    {
                        value = ch.moveSpeed;
                        target = ch;
                    }
                }
            }
        }
        return target;
    }
}
 