using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 전투, 비전투 구분하여 관리하는 법.?
public class Hero : Character
{
    [Header("Macro")]
    [SerializeField] ConditionMacro[]   conditionMacros;
    [SerializeField] ActionMacro[]      actionMacros;
    int maxMacroCount = 5;          // 매니저에 둘 변수
    [Header("Additional")]
    public Vector3 beginPos;

    private void Start() {
        conditionMacros = new ConditionMacro[maxMacroCount];
        actionMacros = new ActionMacro[maxMacroCount];
        
        beginPos = gameObject.transform.position;

        conditionMacros[0] = new Condition_FindTarget_Most("HP가 가장 낮은 적", this, Condition_FindTarget_Most.EGroup.Enemy, Condition_FindTarget_Most.EValue.HP, Condition_FindTarget_Most.EMost.Least);
        actionMacros[0] = new Action_NormalAttack("일반 공격", this);

        conditionMacros[1] = new Condition_Self("체력 35% 이하일 때", this, Condition_Self.EInfo.HP, Condition_Self.EType.Most, 35f);
        actionMacros[1] = new Action_FallBack("후퇴", this);
    }
    private void Update() {
        
        for (int i = 0; i < maxMacroCount; i++)
        {
            if (conditionMacros[i] == null) continue;

            if (conditionMacros[i].IsSatisfy())
            {
                if (actionMacros[i] == null) continue;

                actionMacros[i].Execute();
                break;
            }
        }

        //test
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.Damaged(1f, 0f);
        }
    }

    public override void Death()
    {
        base.Death();
        //
    }

    public bool IsTargetInRange()
    {
        return (target.transform.position - gameObject.transform.position).sqrMagnitude <= attackRange * attackRange;
    }

    public void ResetPos()
    {
        Debug.Log("ResetPos()");
        transform.position = beginPos;
        target = null;
    }
}
