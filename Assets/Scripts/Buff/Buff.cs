using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public float dura;
    public float value;
    protected bool isOn;

    public abstract void Add(float dura, float value);
    public abstract void Sub();
}
