using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance { get; private set; }
    public ItemInfoUI itemInfoUI;

    [Header("Equip Items _ Weapon & Armor")]
    public List<EquipItemData> items_equip;
    //public List<int> equipTierIndexLine;    // 0 ~ list의 0번 인덱스까지 1티어 장비로 간주, 
    [Header("Equip Items _ Accessory")]
    public List<AccessoryItemData> items_acce;
    //public List<int> acceTierIndexLine;

    private void Awake() { instance = this; }
    private void Start() { HideInfoUI(); }


    public void ShowInfoUI(ItemData data)
    {
        itemInfoUI.gameObject.SetActive(true);
        RenewItemInfo(data);
    }

    public void HideInfoUI(){ itemInfoUI.gameObject.SetActive(false); }
    void RenewItemInfo(ItemData data) { itemInfoUI.RenewUI(data); }

    public EquipItemData GetRandEquipItem()
    {
        return items_equip[Random.Range(0, items_equip.Count)];
    }

    public EquipItemData GetRandAccessory()
    {
        return items_acce[Random.Range(0, items_acce.Count)];
    }

}
