using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    [Header("UI")]
    public InventoryUI invenUI;
    public Button invenBtn;
    [Header("Empty Unit Sprite")]
    public Sprite emptyUnitSprite;

    [Header("Item Data")]
    public List<WeaponItemData>     weaponDatas;
    public List<ArmorItemData>      armorDatas;
    public List<AccessoryItemData>  accessoryDatas;

    public int maxCount { get { return 18; } }


    private void Awake() { instance = this; }

    // UI 관련
    public void ShowInvenUI(){
        invenUI.gameObject.SetActive(true);
        invenBtn.interactable = false;
        MacroManager.instance.HideMacroUI();
    }

    public void HideInvenUI(){
        invenUI.gameObject.SetActive(false);
        invenBtn.interactable = true;
    }

    // UI 갱신
    public void RenewUI(){
        RenewWeaponInven();
    }

    public void RenewWeaponInven(){
        invenUI.RenewWeaponInven(maxCount, weaponDatas.Count);
    }

    // Item 장착
    public void EquipItem(WeaponItemData itemData){
        RemoveItem(itemData);
        invenUI.SetWeaponData(itemData);
    }

    // Item 추가, 삭제
    public void AddItem(WeaponItemData itemData) { 
        invenUI.AddItem(itemData);          // weaponDatas의 count 값을 참조하기 때문에 순서 중요함
        weaponDatas.Add(itemData); 
    }

    public void RemoveItem(WeaponItemData itemData)
    {   //Debug.Log("RemoveItem : " + itemData.itemName);
        if(!weaponDatas.Contains(itemData)) return;
       
        weaponDatas.Remove(itemData);
        RenewWeaponInven();
    }
}
