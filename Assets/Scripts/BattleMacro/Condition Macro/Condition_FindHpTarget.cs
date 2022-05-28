using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_FindHpTarget : ConditionMacro
{
    [Tooltip("Least : 최소 / Most : 최대")]
    public EMost eMost;
    public EGroup eTargetGroup;
    [Range(0f, 100f)] public float value;

    bool m_hasAnyHpChange;

    private void OnEnable()     { DungeonManager.instance.onChangeAnyHP += AnyHpChange; }
    private void OnDisable()    { DungeonManager.instance.onChangeAnyHP -= AnyHpChange; }

    void AnyHpChange() { m_hasAnyHpChange = true; }

    public override bool IsSatisfy(){
        // 처음 들어올 때 target null 돼서 들어오니까 검사, 타겟 죽어서 없는 등의 경우에도 검사
        Debug.Log("IsSatisfy() owner.target " + owner.target);
        if (owner.target == null)   { return FindTarget(); }
        
        // 누군가의 체력 변경이 일어나면 다시 검사
        else if (m_hasAnyHpChange)  { return FindTarget(); }
        Debug.Log("Find" + owner.target);
        
        // 그럼에도 null 이면 조건에 부합X
        if (owner.target == null)   { return false; }
        else                        { return true; }
    }

    bool FindTarget()
    {
        m_hasAnyHpChange = false;
        if (eTargetGroup == EGroup.Hero)
        {   // 아군
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead) continue;
                if (eMost == EMost.Least)
                {   // value 이상
                    if (ch.curHp / ch.maxHp * 100f >= value)
                    {
                        owner.target = ch;  Debug.Log("Find" + owner.target);
                        return true;
                    }
                }
                else{   // value 이하
                    if (ch.curHp / ch.maxHp * 100f <= value)
                    {
                        owner.target = ch;  Debug.Log("Find" + owner.target);
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
                        owner.target = ch;  Debug.Log("Find" + owner.target);
                        return true;
                    }
                }
                else{   // value 이하
                    if (ch.curHp / ch.maxHp * 100f <= value)
                    {
                        owner.target = ch;  Debug.Log("Find" + owner.target);
                        return true;
                    }
                }
            }
        }
        Debug.Log("Find false" + owner.target);
        return false;
    }
}