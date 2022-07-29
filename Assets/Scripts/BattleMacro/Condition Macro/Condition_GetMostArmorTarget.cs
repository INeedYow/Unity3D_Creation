using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_GetMostArmorTarget : ConditionMacro
{
    public EMost    eMost;
    public EGroup   eTargetGroup;

    float m_value;
    bool m_hasAnyArmorChange;

    private void OnEnable()     { //
        DungeonManager.instance.onChangeAnyArmor += AnyArmorChange; 
        DungeonManager.instance.onWaveStart += AnyArmorChange; 
    }
    private void OnDisable()    { 
        DungeonManager.instance.onChangeAnyArmor -= AnyArmorChange; 
        DungeonManager.instance.onWaveStart -= AnyArmorChange;
    }

    void AnyArmorChange() { m_hasAnyArmorChange = true; }


    public override bool IsSatisfy( )
    {   // 체력 변경 or 타겟 null 이면 재검색
        if (m_hasAnyArmorChange || target == null) FindTarget();
        return true;
    }

    void FindTarget()   
    {
        m_hasAnyArmorChange = false;
        
        if (eTargetGroup == EGroup.Hero)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return;

            if (eMost == EMost.Least)
            {   // 최소
                m_value = Mathf.Infinity;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value > ch.armorRate)
                    {
                        m_value = ch.armorRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value < ch.armorRate)
                    {
                        m_value = ch.armorRate;
                        target = ch;
                    }
                }
            }
        }
        else{   // 적군
            target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (target == null) return;

            if (eMost == EMost.Least)
            {   // 최소
                m_value = Mathf.Infinity;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value > ch.armorRate)
                    {
                        m_value = ch.armorRate;
                        target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;
                    if (m_value < ch.armorRate)
                    {
                        m_value = ch.armorRate;
                        target = ch;
                    }
                }
            }
        }
    }
}
