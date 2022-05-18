using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GFX : MonoBehaviour
{
    protected int repeat;

    private void Start() { repeat = MacroManager.instance.maxMacroCount; }

    protected void OnEnable()     { InvokeRepeating("LookTarget", 0f, 0.2f); }
    protected void OnDisable()    { CancelInvoke("LookTarget"); }

    protected abstract void LookTarget();
}
