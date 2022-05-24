using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/WeaponItemData")]
public class WeaponItemData : EquipItemData
{
    [Header("Item Spec------------------------")]
    public float minDamage;
    public float maxDamage;
    public float magicDamage;

    public override void Use()
    {   //Debug.Log("Equip.Use()");
        HeroManager.instance.selectedHero.Equip(this);
        InventoryManager.instance.EquipItem(this);
    }

    protected override void AddItem()
    {
        InventoryManager.instance.AddItem(this);
    }

    public override void UnEquip()
    {   //Debug.Log("UnEquip()");
        HeroManager.instance.selectedHero.UnEquipWeapon();
        InventoryManager.instance.AddItem(this);
    }
}
