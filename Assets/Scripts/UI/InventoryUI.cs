using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Prefab Unit")]
    public InventoryUnit prfUnit;
    [Header("Inventory Units")]
    public List<InventoryUnit> weaponUnits;
    public List<InventoryUnit> armorUnits;
    public List<InventoryUnit> accessoryUnits;

    [Header("Current Equips")]
    public InventoryUnit weaponEquipUnit; 
    public InventoryUnit armorEquipUnit;
    public InventoryUnit accessoryEquipUnit;

    [Header("Unit parents")]
    public RectTransform weaponParent;
    public RectTransform armorParent;
    public RectTransform accessoryParent;

    // Macro UI랑 Inven UI랑 같은 오브젝트 하위에서 존재하는데
    // invenUI를 초기화 해주고 active 끄고 싶은데 
    // awake는 스크립트 비활성화 때는 동작하는데
    // gameObject 비활성화에 동작하는 건 아님
    // HeroManager.Start()에서 처음에 켜고 초기화 해주고 끄게 했음(켜놓고 있어야 하는 게 불편해서)
    public void Init(){
        weaponUnits = new List<InventoryUnit>();
        armorUnits = new List<InventoryUnit>();
        accessoryUnits = new List<InventoryUnit>();

        for (int i = 0; i < InventoryManager.instance.maxCount; i++)
        {   
            weaponUnits.Add(Instantiate(prfUnit, weaponParent));
        }
        //Debug.Log("InvenUI Init()" + weaponUnits.Count);
        for (int i = 0; i < InventoryManager.instance.maxCount; i++)
        {
            armorUnits.Add(Instantiate(prfUnit, armorParent));
        }

        for (int i = 0; i < InventoryManager.instance.maxCount; i++)
        {
            accessoryUnits.Add(Instantiate(prfUnit, accessoryParent));
        }
    }

    public void AddItem(WeaponItemData itemData){
        weaponUnits[InventoryManager.instance.weaponDatas.Count].SetData(itemData);
    }
}
