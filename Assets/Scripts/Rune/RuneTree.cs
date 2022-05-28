using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTree : MonoBehaviour
{
    public Rune firstRune;

    private void OnEnable() {
        Init();
    }
    public void Init()
    { 
        firstRune.OpenRuneSlot();
    }
    public void Apply()
    {
        firstRune.Apply();
    }

    public void Release()
    {
        firstRune.Release();
    }
}

// TODO 던전 진입 시 Tree.Apply(); / 나갈 때 Tree.Release();