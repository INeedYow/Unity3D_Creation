using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    new protected void Start() {
        base.Start();
        isStop = true;
    }
    public override void Death(){
        //
        base.Death();
        onDeath?.Invoke();
    }
}
