using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "ItemData/AccessoryItemData")]
public class AccessoryItemData : EquipItemData
{
    [Header("Item Spec------------------------")]
    public float value;
    
    public GameObject prfSpecialAbility;    // Q. 특수능력 // 이벤트랑 섞어서 써보려고

    public override void Use()
    {
        HeroManager.instance.selectedHero.Equip(this);
        InventoryManager.instance.EquipItem(this);
    }

    protected override void AddItem()
    {
        InventoryManager.instance.AddItem(this);
    }

    public override void UnEquip(){
        HeroManager.instance.selectedHero.UnEquipArmor();
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

            case 1 : 
            {
                if (value == 0f) return false;
                
                // 특수능력 수치 혹은 일반 능력치
                return true;
            }

            case 2 :
            {
                if (prfSpecialAbility == null) return false;

                // 특수능력 이름, 설명
                return true;
            }

            default : return false;
        }
    }
}
