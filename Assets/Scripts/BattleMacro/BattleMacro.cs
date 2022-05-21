using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BattleMacro : MonoBehaviour
{
    public BattleMacroData data;
    [HideInInspector] public Character owner;
}