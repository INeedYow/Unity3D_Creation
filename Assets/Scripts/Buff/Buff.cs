using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public EBuff eBuff;
    public Character target ;
    public float ratio;
    public float dura;
    
    private void OnDisable() {
        if (target != null) target.onDead -= Remove;
    }

    public abstract void Add(Character target, float duration, float buffRatio);
    public abstract void Finish();
    public abstract void Remove();
}
