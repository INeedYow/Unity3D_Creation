using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_GetMostHpTarget : ConditionMacro
{
    [Tooltip("Least : 최소 / Most : 최대")]
    public EMost eMost;
    public EGroup eTargetGroup;
    float m_value;
    bool m_hasAnyHpChange;

    private void OnEnable()     { DungeonManager.instance.onChangeAnyHP += AnyHpChange; }
    private void OnDisable()    { DungeonManager.instance.onChangeAnyHP -= AnyHpChange; }
    
    void AnyHpChange() { m_hasAnyHpChange = true; }

    public override bool IsSatisfy( )
    {   // 체력 변경 or 타겟 null 이면 재검색
        if (m_hasAnyHpChange || target == null) FindTarget();
        return true;
    }

    void FindTarget()   // 처음 null일때, 누군가의 HP가 변했을 때 타겟 재설정 (좁은 곳에서 계속 싸우는 방식이라 HP는 거의 실시간으로 하는 거랑 다름 없는듯)
    {
        m_hasAnyHpChange = false;
        
        if (eTargetGroup == EGroup.Hero)
        {   // 아군
            target = PartyManager.instance.GetAliveHero();
            if (target == null) return;

            if (eMost == EMost.Least)
            {   // 최소
                m_value = 1f;
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead) continue;
                    if (m_value > ch.curHp / ch.maxHp)
                    {
                        m_value = ch.curHp / ch.maxHp;
                        target = ch;
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
                m_value = 1f;
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead) continue;
                    if (m_value > ch.curHp / ch.maxHp)
                    {
                        m_value = ch.curHp / ch.maxHp;
                        target = ch;
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
                        target = ch;
                    }
                }
            }
        }
    }
}
