using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO macro,, class 및 enum으로 나눌 영역 확실히 정하기
public class Condition_FindHpTarget : ConditionMacro
{
    public EMost eMost;
    public EGroup eTargetGroup;
    [Tooltip("value (0f ~ 100f)")]
    public float value;

    public override bool IsSatisfy(){
        return FindTarget();
    }

    bool FindTarget()
    {
        if (eTargetGroup == EGroup.Ally)
        {   // 아군
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    if (ch.curHp / ch.maxHp * 100f >= value)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
                else{   // value 이하
                    if (ch.curHp / ch.maxHp * 100f <= value)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
            }
        }
        else{
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (ch.isDead) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    if (ch.curHp / ch.maxHp * 100f >= value)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
                else{   // value 이하
                    if (ch.curHp / ch.maxHp * 100f <= value)
                    {
                        owner.target = ch;
                        return true;
                    }
                }
            }
        }
        return false;
    }
}