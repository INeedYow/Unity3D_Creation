using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_GetMostHpTarget : ConditionMacro
{
    public EMost eMost;
    public EGroup eGroup;
    Character m_target;
    float m_value;

    public Condition_GetMostHpTarget(string desc, Hero hero, EMost eMost, EGroup eGroup) 
        : base(desc, hero) 
    {
        this.eMost = eMost;
        this.eGroup = eGroup;
        DungeonManager.instance.onChangeAnyHP += GetTarget;
    }

    public override bool IsSatisfy(){
        if (null == m_target) GetTarget();
        return true;
    }

    void GetTarget()   // 처음 null일때, 누군가의 HP가 변했을 때 타겟 재설정
    {
        if (eGroup == EGroup.Ally)
        {   // 아군
            m_target = PartyManager.instance.GetAliveHero();
            if (m_target == null) return;

            if (eMost == EMost.Least)
            {   // 최소
                m_value = 1f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (m_value > ch.curHp / ch.maxHp)
                    {
                        m_value = ch.curHp / ch.maxHp;
                        m_target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (m_value < ch.curHp / ch.maxHp)
                    {
                        m_value = ch.curHp / ch.maxHp;
                        m_target = ch;
                    }
                }
            }
        }
        else{   // 적군
            m_target = DungeonManager.instance.curDungeon.GetAliveMonster();
            if (m_target == null) return;

            if (eMost == EMost.Least)
            {   // 최소
                m_value = 1f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (m_value > ch.curHp / ch.maxHp)
                    {
                        m_value = ch.curHp / ch.maxHp;
                        m_target = ch;
                    }
                }
            }
            else{   // 최대
                m_value = 0f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (m_value < ch.curHp / ch.maxHp)
                    {
                        m_value = ch.curHp / ch.maxHp;
                        m_target = ch;
                    }
                }
            }
        }
        owner.target = m_target;
    }
}
