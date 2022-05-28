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

    [Header("Equip Units")]
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
        // 
        weaponUnits = new List<InventoryUnit>();
        armorUnits = new List<InventoryUnit>();
        accessoryUnits = new List<InventoryUnit>();

        for (int i = 0; i < InventoryManager.instance.maxCount; i++)
        {   
            weaponUnits.Add(Instantiate(prfUnit, weaponParent));
        }
        
        for (int i = 0; i < InventoryManager.instance.maxCount; i++)
        {
            armorUnits.Add(Instantiate(prfUnit, armorParent));
        }

        for (int i = 0; i < InventoryManager.instance.maxCount; i++)
        {
            accessoryUnits.Add(Instantiate(prfUnit, accessoryParent));
        }

        // 장비칸 구분
        weaponEquipUnit.isEquipUnit = true;     
        armorEquipUnit.isEquipUnit = true;
        accessoryEquipUnit.isEquipUnit = true;
    }

    private void OnEnable() {
        HeroManager.instance.onChangeSelectedHero += RenewEquipUnits;
        RenewEquipUnits(HeroManager.instance.selectedHero);
    }

    private void OnDisable() {
        HeroManager.instance.onChangeSelectedHero -= RenewEquipUnits;
    }

    public void RenewEquipUnits(Hero hero)
    {   // 선택 영웅 변경시 장착 칸 정보 갱신
        if (hero == null)
        {
            weaponEquipUnit.SetData(null);
            armorEquipUnit.SetData(null);
            accessoryEquipUnit.SetData(null);
        }
        else{
            weaponEquipUnit.SetData(hero.weaponData);
            armorEquipUnit.SetData(hero.armorData);
            accessoryEquipUnit.SetData(hero.accessoryData);
        }
        
    }


    // 바로 쓰는 게 아니라 Manager통해서만 호출되게 할 수 있나 event?
    public void AddItem(WeaponItemData itemData){
        weaponUnits[InventoryManager.instance.weaponDatas.Count].SetData(itemData);
    }

    public void AddItem(ArmorItemData itemData){
        armorUnits[InventoryManager.instance.armorDatas.Count].SetData(itemData);
    }

    public void AddItem(AccessoryItemData itemData){
        accessoryUnits[InventoryManager.instance.accessoryDatas.Count].SetData(itemData);
    }


    public void RenewWeaponInven(int maxCount, int curCount)
    {   
        for (int i = 0; i < maxCount; i++)
        {   
            if (i < curCount)
            {   // 아이템이 존재하는 Unit인 경우
                weaponUnits[i].SetData(InventoryManager.instance.weaponDatas[i]);
            }
            else{   
                weaponUnits[i].SetData(null);
            }
        }
    }

    public void RenewArmorInven(int maxCount, int curCount)
    {   
        for (int i = 0; i < maxCount; i++)
        {   
            if (i < curCount)
            {   // 아이템이 존재하는 Unit인 경우
                armorUnits[i].SetData(InventoryManager.instance.armorDatas[i]);
            }
            else{   
                armorUnits[i].SetData(null);
            }
        }
    }

    public void RenewAccessoryInven(int maxCount, int curCount)
    {   
        for (int i = 0; i < maxCount; i++)
        {   
            if (i < curCount)
            {   // 아이템이 존재하는 Unit인 경우
                accessoryUnits[i].SetData(InventoryManager.instance.accessoryDatas[i]);
            }
            else{   
                accessoryUnits[i].SetData(null);
            }
        }
    }

    public void SetWeaponData(WeaponItemData weaponData){
        weaponEquipUnit.SetData(weaponData);
    }

    public void SetArmorData(ArmorItemData armorData){
        armorEquipUnit.SetData(armorData);
    }

    public void SetAccessoryData(AccessoryItemData accessoryData){
        accessoryEquipUnit.SetData(accessoryData);
    }
}
