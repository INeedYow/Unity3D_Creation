using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    [Header("UI")]
    public GameObject heroSetUI;        // 인벤토리UI가 먼저 보일 거라서 같이 쓰지만 전체는 여기서 갖도록
    public InventoryUI invenUI;
    public Button invenBtn;             // macro, inven 토글 버튼
    [Header("Empty Unit Sprite")]
    public Sprite emptyUnitSprite;

    [Header("Item Data")]
    public List<WeaponItemData>     weaponDatas;
    public List<ArmorItemData>      armorDatas;
    public List<AccessoryItemData>  accessoryDatas;

    [Header("Interact box")]
    public GameObject interactBox;          // 장착, 판매 버튼 UI
    public Text sellCost;

    InventoryUnit m_selectedUnit;

    public int maxCount { get { return 18; } }


    private void Awake() { instance = this; }

    private void Start() {  // 인벤토리 Unit들 초기화
        heroSetUI.gameObject.SetActive(true);
        invenUI.Init();
        heroSetUI.gameObject.SetActive(false);
    }

    // UI 관련
    public void ShowInvenUI(){
        invenUI.gameObject.SetActive(true);
        invenBtn.interactable = false;
        MacroManager.instance.HideMacroUI();

        invenUI.RenewEquipUnits(HeroManager.instance.selectedHero);
    }

    public void HideInvenUI(){
        invenUI.gameObject.SetActive(false);
        invenBtn.interactable = true;
    }

    public void ShowHeroSetUI()
    {
        heroSetUI.gameObject.SetActive(true);
    }

    public void HideHeroSetUI()
    {
        heroSetUI.gameObject.SetActive(false);
    }

    // UI 갱신
    public void RenewUI(){
        RenewWeaponInven();
        RenewArmorInven();
        RenewAccessoryInven();
    }

    public void RenewWeaponInven()      { invenUI.RenewWeaponInven(maxCount, weaponDatas.Count); }
    public void RenewArmorInven()       { invenUI.RenewArmorInven(maxCount, armorDatas.Count); }
    public void RenewAccessoryInven()   { invenUI.RenewAccessoryInven(maxCount, accessoryDatas.Count); }

    // Item 장착
    public void EquipItem(WeaponItemData itemData){
        RemoveItem(itemData);
        invenUI.SetWeaponData(itemData);
    }

    public void EquipItem(ArmorItemData itemData){
        RemoveItem(itemData);
        invenUI.SetArmorData(itemData);
    }

    public void EquipItem(AccessoryItemData itemData){
        RemoveItem(itemData);
        invenUI.SetAccessoryData(itemData);
    }

    // Item 추가, 삭제
    public void AddItem(WeaponItemData itemData) 
    { 
        if (weaponDatas.Count >= maxCount)
        {   
            itemData.Sell();
            return;
        }

        invenUI.AddItem(itemData);          // weaponDatas의 count 값을 참조하기 때문에 순서 중요함
        weaponDatas.Add(itemData); 
    }

    public void AddItem(ArmorItemData itemData) 
    { 
        if (armorDatas.Count >= maxCount)
        {   //Debug.Log("인벤토리가 가득 차서 아이템 자동판매");
            itemData.Sell();
            return;
        }

        invenUI.AddItem(itemData);          
        armorDatas.Add(itemData); 
    }

    public void AddItem(AccessoryItemData itemData) 
    { 
        if (accessoryDatas.Count >= maxCount)
        {   
            itemData.Sell();
            return;
        }

        invenUI.AddItem(itemData);          
        accessoryDatas.Add(itemData); 
    }

    public void RemoveItem(WeaponItemData itemData)
    {   //Debug.Log("RemoveItem : " + itemData.itemName);
        if(!weaponDatas.Contains(itemData)) return;
       
        weaponDatas.Remove(itemData);
        RenewWeaponInven();
    }

    public void RemoveItem(ArmorItemData itemData)
    {   
        if(!armorDatas.Contains(itemData)) return;
       
        armorDatas.Remove(itemData);
        RenewArmorInven();
    }

    public void RemoveItem(AccessoryItemData itemData)
    {   
        if(!accessoryDatas.Contains(itemData)) return;
       
        accessoryDatas.Remove(itemData);
        RenewAccessoryInven();
    }

    public void ShowInteractBox(InventoryUnit unit)
    {   
        m_selectedUnit = unit;

        interactBox.SetActive(true);
        interactBox.transform.position = Input.mousePosition + Vector3.down * 130f;
        sellCost.text = m_selectedUnit.curData.sellCost.ToString();
    }

    public void HideInteractBox()
    {
        m_selectedUnit = null;
        interactBox.SetActive(false);
    }

    public void OnEquipButton()
    {
        m_selectedUnit.EquipItem();
        HideInteractBox();
    }

    public void OnSellButton()
    {
        m_selectedUnit.SellItem();
        HideInteractBox();
    }
}
