using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneAbility : MonoBehaviour
{
    public abstract int GetCurValue();

    public abstract void Apply();
    public abstract void Release();
}
