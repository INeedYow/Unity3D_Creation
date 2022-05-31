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

            case EMonster.Pollen:    monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfPollenSkill, monster.transform);
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
                BattleInfoType.Monster_heal, targetTF.position + Vector3.up * 3f, damage );
        }
        else if (isMagic){  
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Hero_magic, targetTF.position + Vector3.up * 3f, damage );
        }
        else{
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Hero_damage, targetTF.position + Vector3.up * 3f, damage );
        }
    }

    public override void Death(){
        //
        isDead = true;
        onDeadGetThis?.Invoke(this);
        onDead?.Invoke();
        DungeonManager.instance.onSomeoneDead?.Invoke();
        monsGFX.gameObject.SetActive(false);
    }

    // public void Reset()
    // {
    //     ResetBuffs();
    // }
    

    private void OnMouseDown() { DungeonManager.instance.ShowMonInfoUI(this); }
}