using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [HideInInspector] public EBuff eBuff;
    [HideInInspector] public Character target ;
    [HideInInspector] public float ratio;
    [HideInInspector] public float dura;
    protected bool isDebuff;
    
    private void OnDisable() {
        if (target != null) target.onDead -= Remove;
    }

    public abstract void Add(Character target, float duration, float buffRatio);
    public abstract void Finish();
    public abstract void Remove();
    public bool IsDebuff() { return isDebuff; }
}
