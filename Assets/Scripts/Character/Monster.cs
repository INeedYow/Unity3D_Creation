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
        eGroup = EGroup.Enemy;
        monsGFX.monster = this;
    }

    public static Monster Create(EMonster eMonster){
        Monster monster = null;
        switch (eMonster)
        {
            case EMonster.RedSlime:
            {
                monster = Instantiate(ObjectPool.instance.prfMonsters[(int)eMonster]);
                // Attack Command
                monster.attackCommand = new NormalAttackCommand(monster);
                break;
            }

            case EMonster.BlueSlime:
            {
                monster = Instantiate(ObjectPool.instance.prfMonsters[(int)eMonster]);
                // Attack Command
                monster.attackCommand = new NormalAttackCommand(monster);
                break;
            }
        }
        // Macro
        monster.SetMacro();
        return monster;
    }

    public void SetMacro(){
        conditionMacros = macrosParent.GetComponentsInChildren<ConditionMacro>();
        foreach (ConditionMacro macro in conditionMacros)
        { macro.owner = this; }

        actionMacros = macrosParent.GetComponentsInChildren<ActionMacro>();
        foreach (ActionMacro macro in actionMacros)
        { macro.owner = this; }
        
        monsGFX.repeat = conditionMacros.Length;
    }
   
    protected override void ShowDamageText(float damage, bool isMagic = false)
    {
        GameManager.instance.ShowBattleInfoText(
            isMagic ? BattleInfoType.Ally_Magic : BattleInfoType.Ally_Damage, 
            transform.position + Vector3.up * 5f, 
            damage
        );
    }

    public override void Death(){
        //
        isDead = true;
        monsGFX.gameObject.SetActive(false);
        onDeath?.Invoke(this);
    }
}