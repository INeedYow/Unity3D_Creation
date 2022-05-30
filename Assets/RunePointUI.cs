using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RunePointUI : MonoBehaviour
{
    public Text lv;
    public Image exp;
    public Text point;

    // private void OnEnable() {
    //     SetLV();
    // } 

    // void SetLV()
    // {
    //     lv.text = PlayerManager.instance.LV.ToString();
    //     exp.fillAmount = PlayerManager.instance.curExp / PlayerManager.instance.maxExp;
    //     point.text = PlayerManager.instance.runePoint.ToString();
    // }

    // public void RenewUI(int newPoint)
    // {   
    //     point.text = newPoint.ToString();
    // }
}
