using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUnit : MonoBehaviour
{
    public EquipItemData curData;
    public Image icon;
    float lastClickTime;

    public void SetData(EquipItemData data)
    { 
        if (data == null)
        {
            curData = null;
            icon = null;
        }
        else{
            curData = data; 
            icon.sprite = data.icon;
        }
    }


    private void OnMouseEnter() {
        if (curData == null) return;
        Debug.Log("OnMouseEnter");
        // TODO info
    }

    private void OnMouseExit() {
        if (curData == null) return;
        Debug.Log("OnMouseExit");
        // TODO info
    }

    public void OnClicked()
    {
        if (curData == null) return;
        if (Time.time < lastClickTime + 1f) {Debug.Log("OnMouseClicked DB");
            curData.Use();
        }
        lastClickTime = Time.time ;
    }
    

}
