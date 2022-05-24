using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public InventoryUnit[] invenUnits;


    private void Awake() {
        Init();
    }

    void Init(){
        // 배열 초기화
        invenUnits = new InventoryUnit[transform.childCount];
        invenUnits = GetComponentsInChildren<InventoryUnit>();
    }

    private void OnEnable() {
        RenewUI();
    }

    public void RenewUI(){
        foreach(inven)
    }
}
