using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance { get; private set; }
    public ItemInfoUI itemInfoUI;


    private void Awake() { instance = this; }
    private void Start() {
        HideInfoUI();
        Item.s_scale = new Vector3(0.5f, 0.5f, 0.5f);
    }


    public void ShowInfoUI(ItemData data){
        itemInfoUI.gameObject.SetActive(true);
        RenewItemInfo(data);
    }

    public void HideInfoUI(){ itemInfoUI.gameObject.SetActive(false); }
    void RenewItemInfo(ItemData data) { itemInfoUI.RenewUI(data); }

}
