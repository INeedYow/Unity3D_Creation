using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [Range(1, 3)] public int maxLV;

    public abstract void Apply(int level);
    public abstract void Release(int level);
}
