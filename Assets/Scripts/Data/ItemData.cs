using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string desc;

    public abstract bool Buy();
    public abstract bool Sell();

    public abstract int GetCost();
}