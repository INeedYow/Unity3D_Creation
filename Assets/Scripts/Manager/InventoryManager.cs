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

    [Header("Item Data")]
    public List<WeaponItemData>     weaponDatas;
    public List<ArmorItemData>      armorDatas;
    public List<AccessoryItemData>  accessoryDatas;

    public int maxCount { get { return 18; } }


    private void Awake() { instance = this; }

    public void ShowInvenUI(){
        invenUI.gameObject.SetActive(true);
        invenBtn.interactable = false;
        MacroManager.instance.HideMacroUI();
    }

    public void HideInvenUI(){
        invenUI.gameObject.SetActive(false);
        invenBtn.interactable = true;
    }

    public void AddItem(WeaponItemData itemData) { 
        invenUI.AddItem(itemData);          // weaponDatas의 count 값을 참조하기 때문에 순서 중요함
        weaponDatas.Add(itemData); 
    }

    public void RenewUI(){

    }

    // void RenewWeaponInven(){
    //     foreach(WeaponItemData weapon in weaponDatas){
    //         weapon
    //     }
    // }

}
