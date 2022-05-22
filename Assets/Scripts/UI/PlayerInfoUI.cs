using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Text LV;
    public Image Exp;
    public Text curParty;
    public Text maxParty;
    public Text gold;

    // UI 갱신
    public void RenewLV(int lv)             { LV.text = lv.ToString(); }
    public void RenewExp(int exp, int max)  { Exp.fillAmount = (float)exp / max * 0.75f; }
    public void RenewCurParty(int value)    { curParty.text = value.ToString(); }
    public void RenewMaxParty(int value)    { maxParty.text = value.ToString(); }
    public void RenewGold(int value)        { gold.text = value.ToString(); }
}
