using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 전투, 비전투 구분하여 관리하는 법.?
public class Hero : Character
{
    public enum EClass { Knight, Archer }
    [Header("GFX")]
    public Hero_GFX heroGFX;
    // [Macro]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;
    public int maxMacroCount = 5;          // 매니저에 둘 변수?

    [Header("Additional Info")]
    public EClass eClass;   // TODO 직업에 따라 다른 프리팹, 스킬 생성되게

    [Header("Level")]
    public int level = 1;
    public float maxExp;
    float _curExp;  //TODO
    public float curExp {
        get { return _curExp; } 
        set { 
            _curExp = value ;
            if (_curExp >= maxExp)
            {
                level++;
                _curExp -= maxExp;
                maxExp += 100f; // 임시로
            }
        }
    }

    new protected void Awake() {
        base.Awake();
        Init();
        
        //beginPos = gameObject.transform.position;

        // conditionMacros[0] = new Condition_FindDistanceTarget("테스트", this, EMost.Least, EGroup.Enemy, 10f);
        // actionMacros[0] = new Action_NormalAttack("일반 공격", this);

        // conditionMacros[1] = new Condition_FindHpTarget("테스트", this, EMost.Most, EGroup.Enemy, 50f);
        // actionMacros[1] = new Action_NormalAttack("일반 공격", this);
    }

    void Init(){
        heroGFX = GetComponentInChildren<Hero_GFX>(true);
        dummy = GetComponentInChildren<Dummy>(true);
        heroGFX.hero = this;
        dummy.owner = this;
        conditionMacros = new ConditionMacro[maxMacroCount];
        actionMacros = new ActionMacro[maxMacroCount];
    }

    // private void Update() {
    //     if (isDead || isStop) return;

    //     for (int i = 0; i < maxMacroCount; i++)
    //     {
    //         if (conditionMacros[i] == null) continue;

    //         if (conditionMacros[i].IsSatisfy())
    //         {
    //             if (actionMacros[i] == null) continue;

    //             if (actionMacros[i].Execute()) break;
    //             else continue;
    //         }
    //     }
    // }

    public override void Death()
    {
        base.Death();
        // 바로 사라지게 하는 게 아니라 코루틴이나 애니메이션 이벤트 써서 죽는 애니메이션 후 사망 처리 하고 싶은데
        // 사망 모션동안 충돌 처리, 공격 처리 어떻게 막지? .. bool 변수 하나 더 해야하나
        heroGFX.gameObject.SetActive(false);
    }

    public bool IsTargetInRange()
    {
        return (target.transform.position - gameObject.transform.position).sqrMagnitude <= attackRange * attackRange;
    }

    public void ResetPos()
    {
        transform.position = dummy.beginPos;// + DungeonManager.instance.curDungeon.; // TODO
        target = null;
    }
}
