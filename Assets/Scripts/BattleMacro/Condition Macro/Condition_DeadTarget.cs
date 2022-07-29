using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_DeadTarget : ConditionMacro
{
    public EGroup eTargetGroup;
    bool m_hasChanged;

    private void OnEnable() { 
        if (eTargetGroup == EGroup.Hero)
        {
            DungeonManager.instance.onHeroDead += SomeoneChanged; 
        }
        else{
            DungeonManager.instance.onMonsterDead += SomeoneChanged; 
        }
        DungeonManager.instance.onSomeoneAdd += SomeoneChanged;    
    }

    private void OnDisable() {
        if (eTargetGroup == EGroup.Hero)
        {
            DungeonManager.instance.onHeroDead -= SomeoneChanged; 
        }
        else{
            DungeonManager.instance.onMonsterDead -= SomeoneChanged; 
        }
        DungeonManager.instance.onSomeoneAdd -= SomeoneChanged;    
    }

    void SomeoneChanged() { m_hasChanged = true; }
    
    public override bool IsSatisfy()
    {
        if (m_hasChanged) FindTarget();

        if (target != null) return true;

        return false;
    }

    public void FindTarget()
    {
        target = null;

        if (eTargetGroup == EGroup.Hero)
        {   // 아군영웅
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead)
                {
                    target = ch;
                }
            }
        }
        else{   // 적군
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            { 
                if (ch.isDead)
                {
                    target = ch;
                }
            }
        }
    }
}
