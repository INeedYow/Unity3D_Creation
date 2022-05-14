using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroManager : MonoBehaviour
{
    public static MacroManager instance { get; private set; }

    private void Awake() {
        instance = this;
    }
}
