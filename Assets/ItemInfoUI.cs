using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemInfoUI : MonoBehaviour
{
    public ItemData data;
    public Text itemName;
    public Text desc;
    public Text cost;
    public ItemOptionUnit[] optionUnits = new ItemOptionUnit[4];

    public void RenewUI(ItemData newData){
        data = newData;
        itemName.text = data.itemName;
        desc.text = data.desc;
        cost.text = data.GetCost().ToString();
        
        SetOptions();
    }

    // itemData에 text를 보내서 줄 정보가 있으면 텍스트 설정해주고 true 반환하게 했음
        // true or false 반환 값에 따라 해당 옵션 텍스트 SetActive()하도록
    void SetOptions()
    {
        for (int i = 0; i < 4; i++)
        {
            optionUnits[i].gameObject.SetActive(data.SetOptionText(i, optionUnits[i]));
        }
    }

    
}
