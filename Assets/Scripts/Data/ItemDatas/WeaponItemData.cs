using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public override bool SetOptionText(int optionNumber, ItemOptionUnit optionUnit)
    {
        switch (optionNumber)
        {
            case 0 : 
            {
                if (minDamage == 0f) return false;

                optionUnit.option.text = "최소 공격력 : ";
                optionUnit.value.text = minDamage.ToString(); 
                return true;
            }

            case 1 :
            {
                if (maxDamage == 0f) return false;

                optionUnit.option.text = "최대 공격력 : ";
                optionUnit.value.text = maxDamage.ToString(); 
                return true;
            }

            case 2 :
            {
                if (magicDamage == 0f) return false;

                optionUnit.option.text = "마법 공격력 : ";
                optionUnit.value.text = magicDamage.ToString(); 
                return true;
            }

            default : return false;
        }
    }
}
