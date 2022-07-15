using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "ItemData/AccessoryItemData")]
public class AccessoryItemData : EquipItemData
{
    [Header("Item Spec------------------------")]
    public Ability prfAbility;             // 장착 관련 부분 외에는 모두 ability쪽에서

    public override void Use()
    {   
        HeroManager.instance.selectedHero.Equip(this);
        InventoryManager.instance.EquipItem(this);
    }

    public override void UnEquip(){
        HeroManager.instance.selectedHero.UnEquipAccessory();
        InventoryManager.instance.AddItem(this);
    }

    protected override void AddItem()
    {
        InventoryManager.instance.AddItem(this);
    }

    protected override void RemoveItem()
    {
        InventoryManager.instance.RemoveItem(this);
    }

    public override bool SetOptionText(int optionNumber, ItemOptionUnit optionUnit)
    {   // TODO
        switch (optionNumber)
        {
            case 0 : 
            {
                optionUnit.option.text = "요구 레벨 : ";
                optionUnit.value.text = requireLevel.ToString(); 
                return true;
            }

            case 1: 
            case 2: 
            case 3:
            { return prfAbility.SetOptionText1to3(optionNumber, optionUnit); }

            default : return false;
        }
    }
}
