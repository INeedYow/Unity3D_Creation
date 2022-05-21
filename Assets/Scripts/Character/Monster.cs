using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    [Header("GFX")]
    public Monster_GFX monsGFX;

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
                // 매크로
                monster.monsGFX.SetMacroSize(1);

                monster.conditionMacros[0] = Instantiate(MacroManager.instance.prfMonConditionMacros[0], monster.transform);
                monster.conditionMacros[0].owner = monster;

                monster.actionMacros[0] = Instantiate(MacroManager.instance.prfActionMacros[0], monster.transform);
                monster.actionMacros[0].owner = monster;

                // Attack Command
                monster.attackCommand = new NormalAttackCommand(monster);
                break;
            }
        }
        return monster;
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