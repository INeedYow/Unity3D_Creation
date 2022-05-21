using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GFX : MonoBehaviour
{
    public int repeat; // hero는 정해진 매크로 개수만큼 // 몬스터는 내가 입력해준 개수만큼

    protected void OnEnable()     { InvokeRepeating("LookTarget", 0f, 0.2f); }
    protected void OnDisable()    { CancelInvoke("LookTarget"); }

    protected abstract void LookTarget();
}
