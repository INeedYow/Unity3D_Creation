﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneInfoUI : MonoBehaviour
{
    [SerializeField]
    Rune rune;
    public Text desc;
    public Text curValue;
    public Text nextValue;
    public Text curPoint;
    public Text maxPoint;

    int m_value;

    public void SetRune(Rune newRune)
    {
        rune = newRune;
        SetInfo();
        RenewUI();
    }

    void SetInfo()
    {
        desc.text = rune.data.description;
        maxPoint.text = rune.data.GetMax().ToString();
    }

    public void RenewUI()
    {
        curPoint.text = rune.point.ToString();
        curValue.text = rune.data.GetCurValue(rune.point).ToString();
        
    
        m_value = rune.data.GetNextValue(rune.point);

        if (m_value == 0){
            nextValue.text = "Max";
        }
        else {
            nextValue.text = rune.data.GetNextValue(rune.point).ToString();
        }
    }
}
