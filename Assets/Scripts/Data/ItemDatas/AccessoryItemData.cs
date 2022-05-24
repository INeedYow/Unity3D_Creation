using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public override bool SetOptionText(int optionNumber, ItemOptionUnit optionUnit)
    {   // TODO
        switch (optionNumber)
        {
            case 0 : 
            {
                if (value == 0f) return false;
                
                // 특수능력 수치 혹은 일반 능력치
                return true;
            }

            case 1 :
            {
                if (prfSpecialAbility == null) return false;

                // 특수능력 이름, 설명
                return true;
            }

            default : return false;
        }
    }
}
