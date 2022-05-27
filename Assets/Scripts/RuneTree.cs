using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTree : MonoBehaviour
{
    public static AbilityTree instance { get; private set;}
    
    public AbilitySlot firstSlot;


    private void Awake() {
        instance = this;
    }

    // 
    public void InitTree()  { firstSlot.InitNext(); }
    //
    public void Apply()     { firstSlot.Apply(); }
    //
    public void Release()   { firstSlot.Release(); }

}
