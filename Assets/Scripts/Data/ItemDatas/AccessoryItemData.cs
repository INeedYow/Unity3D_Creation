using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/AccessoryItemData")]
public class AccessoryItemData : EquipItemData
{
    [Header("Item Spec------------------------")]
    public float value;
    
    public GameObject prfSpecialAbility;    // Q. 특수능력 // 이벤트랑 섞어서 써보려고

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
