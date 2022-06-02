using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_ItemUnit : MonoBehaviour
{
    public Image icon;
    
    
    public void SetData(EquipItemData data)
    {
        icon.sprite = data.icon;
    }
}
