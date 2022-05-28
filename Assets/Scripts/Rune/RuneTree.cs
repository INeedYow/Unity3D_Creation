using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTree : MonoBehaviour
{
    public static RuneTree instance { get; private set;}
    
    public Rune firstRune;


    private void Awake() {
        instance = this;
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
