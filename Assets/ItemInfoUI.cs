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

    public void RenewUI(ItemData newData){
        data = newData;
        itemName.text = data.itemName;
        desc.text = data.desc;
        cost.text = data.GetCost().ToString();
    }
}
