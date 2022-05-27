using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    [Header("GFX")]
    public Monster_GFX monsGFX;
    [Header("Macros")]
    public GameObject macrosParent;
    new protected void Awake() {
        base.Awake();
        InitMonster();
    }

    void InitMonster(){
        eGroup = EGroup.Monster;
        monsGFX.monster = this;
    }

    public static Monster Create(EMonster eMonster){
        Monster monster = null;
        monster = Instantiate(ObjectPool.instance.prfMonsters[(int)eMonster]);
        switch (eMonster)
        {
            case EMonster.RedSlime: 
            case EMonster.BlueSlime: monster.attackCommand = new NormalAttackCommand(monster); break;


            case EMonster.Spore:     monster.attackCommand = new NormalAttackCommand(monster);
            {   // 초기화 필요
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfSporeSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }
        }
        // Macro
        monster.SetMacro();
        return monster;
    }

    public void SetMacro(){ // 몬스터 하위로 넣어준 매크로로 초기화
        conditionMacros = macrosParent.GetComponentsInChildren<ConditionMacro>();
        foreach (ConditionMacro macro in conditionMacros)
        { macro.owner = this; }

        actionMacros = macrosParent.GetComponentsInChildren<ActionMacro>();
        foreach (ActionMacro macro in actionMacros)
        { macro.owner = this; }
        
        monsGFX.repeat = conditionMacros.Length;
    }
   
    protected override void ShowDamageText(float damage, bool isMagic = false, bool isHeal = false)
    {   
        if (isHeal) {
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Monster_heal, transform.position + Vector3.up * 5f, damage );
        }
        else if (isMagic){  
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Hero_magic, transform.position + Vector3.up * 5f, damage );
        }
        else{
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Hero_damage, transform.position + Vector3.up * 5f, damage );
        }
    }

    public override void Death(){
        //
        isDead = true;
        monsGFX.gameObject.SetActive(false);
        onDeadGetThis?.Invoke(this);
        onDead?.Invoke();
    }

    private void OnMouseDown() { DungeonManager.instance.ShowMonInfoUI(this); }
}