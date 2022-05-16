using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 전투, 비전투 구분하여 관리하는 법.?
public class Hero : Character
{
    public enum EClass { Knight, Archer, }
    [Header("GFX")]
    public Hero_GFX heroGFX;
    public Dummy dummy;
    // [Macro]
    public ConditionMacro[]   conditionMacros;
    public ActionMacro[]      actionMacros;

    [Header("Additional Info")]
    public EClass eClass;   
    public Projectile_Ally projectile;
    public bool isJoin;
    
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
    [Header("Skill")]   // TODO
    public int maxSkillCount = 4;
    public Skill[] skills = new Skill[4];

    new protected void Awake() {
        base.Awake();
        Init();
    }

    void Init(){
        heroGFX.hero = this;
        dummy.owner = this;
        conditionMacros = new ConditionMacro[HeroManager.instance.maxMacroCount];
        actionMacros = new ActionMacro[HeroManager.instance.maxMacroCount];
        heroGFX.gameObject.SetActive(false);
        dummy.gameObject.SetActive(false);
    }

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
        // Debug.Log(dummy.placedBlock.beginPos);
        // Debug.Log(DungeonManager.instance.curDungeon.beginTf.position);
        transform.position = dummy.placedBlock.beginPos + DungeonManager.instance.curDungeon.beginTf.position; // TODO
        target = null;
    }

    public void Join(){
        if (isJoin) return;
        PartyManager.instance.Join(this);
        isJoin = true;
    }

    public void Leave(){
        if (!isJoin) return;
        PartyManager.instance.Leave(this);
        isJoin = false;
        dummy.gameObject.SetActive(false);
    }
}
