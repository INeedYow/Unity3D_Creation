using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    [Header("GFX")]
    public Monster_GFX monsGFX;

    // public static Monster CreateNewMonster(EMonster eMonster){
    //     Monster monster = null;
    //     switch (eMonster)
    //     {
    //         case EMonster.RedSlime:
    //         {
    //             monster = ObjectPool.instance.p
    //         }
    //     }
    // }
   
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