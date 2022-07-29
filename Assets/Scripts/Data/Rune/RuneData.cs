using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneData : ScriptableObject
{
    public string description;
    
    public abstract int GetMax();
    public abstract bool IsMax(int point);
    public abstract void Apply(int point);
    public abstract void Release(int point);

    public abstract int GetCurValue(int point);
    public abstract int GetNextValue(int point);
}
