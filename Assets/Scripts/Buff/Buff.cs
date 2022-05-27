using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public EBuff eBuff;
    public float ratio;
    protected Character target;

    public abstract void Add(Character target, float duration, float buffRatio);
    public abstract void Finish();
    public void Remove()
    {
        CancelInvoke("Finish");
        Finish();
    }
}
