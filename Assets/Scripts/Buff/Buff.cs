using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public EBuff eBuff;
    [HideInInspector]
    public float ratio;
    public EEffect eEffect;
    protected Character target;
    protected Effect effect;
    protected float dura;

    public abstract void Add(Character target, float duration, float buffRatio);
    public abstract void Finish();
    public void Remove()
    {
        // CancelInvoke("Finish");
        // Finish();
        StopCoroutine("Timer");
        Finish();
    }
}
