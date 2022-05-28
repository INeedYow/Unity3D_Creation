using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneData : ScriptableObject
{
    public abstract bool IsMax(int point);
    public abstract void Apply(int point);
    public abstract void Release(int point);
}
