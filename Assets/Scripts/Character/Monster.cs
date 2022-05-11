using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    public UnityAction<Monster> onDeath;

    public override void Death(){
        //
        onDeath?.Invoke(this);
        Destroy(gameObject);
        Debug.Log(name + "죽음");
    }
}
