﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Text LV;
    public Image Exp;
    public Text gold;
    public Text curParty;
    public Text maxParty;
    public Text curHero;
    public Text maxHero;

    // UI 갱신
    public void RenewLV(int lv)                 { LV.text = lv.ToString(); }
    public void RenewExp(float exp, float max)  { Exp.fillAmount = exp / max * 0.75f; }
    public void RenewGold(int value)            { gold.text = value.ToString(); }
    public void RenewCurParty(int value)        { curParty.text = value.ToString(); }
    public void RenewMaxParty(int value)        { maxParty.text = value.ToString(); }
    public void RenewCurHero(int value)         { curHero.text = value.ToString(); }
    public void RenewMaxHero(int value)         { maxHero.text = value.ToString(); }
}
