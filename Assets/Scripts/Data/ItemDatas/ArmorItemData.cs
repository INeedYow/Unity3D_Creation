using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public override bool SetOptionText(int optionNumber, ItemOptionUnit optionUnit)
    {
        switch (optionNumber)
        {
            case 0 : 
            {
                if (armor == 0f) return false;

                optionUnit.option.text = "물리 방어율 : ";
                optionUnit.value.text = armor.ToString(); 
                return true;
            }

            case 1 :
            {
                if (magicArmor == 0f) return false;

                optionUnit.option.text = "마법 방어율 : ";
                optionUnit.value.text = magicArmor.ToString(); 
                return true;
            }

            case 2 :
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
