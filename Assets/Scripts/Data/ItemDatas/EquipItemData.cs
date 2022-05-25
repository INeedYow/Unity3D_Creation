﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EquipItemData : ItemData
{
    [Header("Require Lv")]
    public int requireLevel;

    [Header("Cost")]
    public int cost;
    public int sellCost;

    public override bool Buy()
    {
        if (cost > GameManager.instance.gold) return false;
        AddItem();

        GameManager.instance.AddGold(-cost); 
        return true;
    }
    protected abstract void AddItem();
    
    public override bool Sell()
    {
        GameManager.instance.AddGold(sellCost);
        RemoveItem();
        return true;
    }

    protected abstract void RemoveItem();

    public override int GetCost()
    {
        return cost;
    }

    public abstract void Use();
    public abstract void UnEquip();
}
