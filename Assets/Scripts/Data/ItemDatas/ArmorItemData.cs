using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "ItemData/ArmorItemData")]
public class ArmorItemData : EquipItemData
{
    [Header("Item Spec------------------------")]
    [Range(0f, 1f)] public float armor;
    [Range(0f, 1f)] public float magicArmor;
    public float Hp;

    public override void Use()
    {
        HeroManager.instance.selectedHero.Equip(this);
        InventoryManager.instance.EquipItem(this);
    }
    
    public override void UnEquip(){
        HeroManager.instance.selectedHero.UnEquipArmor();
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
    {
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
                if (armor == 0f) return false;

                optionUnit.option.text = "물리 방어율 : ";
                optionUnit.value.text = Mathf.RoundToInt(armor * 100f).ToString(); 
                return true;
            }

            case 2 :
            {
                if (magicArmor == 0f) return false;

                optionUnit.option.text = "마법 방어율 : ";
                optionUnit.value.text = Mathf.RoundToInt(magicArmor * 100f).ToString(); 
                return true;
            }

            case 3 :
            {
                if (Hp == 0f) return false;

                optionUnit.option.text = "최대 체력 : ";
                optionUnit.value.text = Hp.ToString(); 
                return true;
            }

            default : return false;
        }
    }
}
