using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUnit : MonoBehaviour
{
    public bool isEquipUnit;    // 장착 칸, 소유 칸 나눔
    [HideInInspector] public EquipItemData curData;
    public Image icon;
    float lastClickTime;

    public void SetData(EquipItemData data)
    { 
        if (data == null)
        {   
            curData = null;
            icon.sprite = null;
        }
        else{
            curData = data; 
            icon.sprite = data.icon;
        }
    }


    private void OnMouseEnter() {
        if (curData == null) return;
        Debug.Log("OnMouseEnter");
        ItemManager.instance.ShowInfoUI(curData);
    }

    private void OnMouseExit() {
        if (curData == null) return;
        Debug.Log("OnMouseExit");
        ItemManager.instance.HideInfoUI();
    }

    public void OnClicked()
    {
        if (curData == null) return;
        if (Time.time < lastClickTime + 1f) {   //Debug.Log("OnMouseClicked DB");
            
            if (isEquipUnit)
            {   // 장착 해제
                curData.UnEquip();
                SetData(null);
            }
            else if (curData.requireLevel > HeroManager.instance.selectedHero.level) 
            {   // 레벨 제한으로 착용 불가
                return; 
            }
            else{
                curData.Use(); 
            }
                
            lastClickTime = 0f;
        }
        lastClickTime = Time.time ;
    }
    

}
