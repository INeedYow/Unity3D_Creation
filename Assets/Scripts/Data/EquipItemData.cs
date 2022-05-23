using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/EquipItem")]
public class EquipItemData : ItemData
{
    [Header("Cost")]
    public int cost;
    public int sellCost;
    [Header("Spec")]    // TODO 부위별로 나눌 것인가
    public int damage;
    public int magic;
    public int armor;
    public int magicArmor;

    // Q. 특수능력 어떻게

    public override bool Buy()
    {
        if (cost > GameManager.instance.gold) return false;
        GameManager.instance.AddGold(-cost); 
        // TODO : inven item 추가
        return true;
    }
    
    public override bool Sell()
    {
        GameManager.instance.AddGold(sellCost);
        // TODO : inven item 제거
        return true;
    }

    public override int GetCost()
    {
        return cost;
    }
}
