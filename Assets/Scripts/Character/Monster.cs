using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    public UnityAction<Monster> onDeath;

    public override void Death(){
        //
        base.Death();
        onDeath?.Invoke(this);
    }
}
