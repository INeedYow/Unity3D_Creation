using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    [Header("GFX")]
    public Monster_GFX monsGFX;
    
    public override void Death(){
        //
        base.Death();
        onDeath?.Invoke();
    }
}
