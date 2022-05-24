using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/WeaponItemData")]
public class WeaponItemData : EquipItemData
{
    [Header("Item Spec------------------------")]
    public float damage;
    public float magicDamage;

    public override void Use()
    {
        HeroManager.instance.selectedHero.Equip(this);
        
    }

    protected override void AddItem()
    {
        InventoryManager.instance.AddItem(this);
    }
}
