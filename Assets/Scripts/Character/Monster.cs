using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    // [Header("Pool ID")][Range(0, 99)]
    // public int pool_ID;

    [Header("GFX")]
    public Monster_GFX monsGFX;
   
    public override void Death(){
        //
        base.Death();
        monsGFX.gameObject.SetActive(false);
        onDeath?.Invoke();
    }
}
