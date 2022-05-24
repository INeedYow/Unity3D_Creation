using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/ArmorItemData")]
public class ArmorItemData : EquipItemData
{
    [Header("Item Spec------------------------")]
    public float armor;
    public float magicArmor;
    public float Hp;

    public override void Use()
    {
        //HeroManager.instance.selectedHero.Equip(this);
    }

    protected override void AddItem()
    {
        //InventoryManager.instance.AddItem(this);
    }

    public override void UnEquip(){
        //
    }
}
