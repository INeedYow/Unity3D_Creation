using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    [Header("GFX")]
    public Monster_GFX monsGFX;
    //[Header("setTarget")]
    //public SetTarget setTarget;         // 어떤 적을 우선 공격대상으로 할지 
    [Header("Macro")]
    public ConditionMacro[] conditionMacros = new ConditionMacro[4];
    public ActionMacro[] actionMacros = new ActionMacro[4];
    
    public override void Death(){
        //
        base.Death();
        monsGFX.gameObject.SetActive(false);
        onDeath?.Invoke();
    }
}
