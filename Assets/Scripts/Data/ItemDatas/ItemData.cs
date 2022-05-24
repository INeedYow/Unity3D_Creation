using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string desc;
    public Sprite icon;

    public abstract bool Buy();
    public abstract bool Sell();

    public abstract int GetCost();

    // heroItem은 스킬에 대한 정보, equipItem은 장비 능력치 정보로 UI에 보여주려고
        // ex) SetOptionText(1, text) -> heroItem인 경우 스킬 1정보 텍스르로 반환하게
        // ++) true 반환하면 해당 unit text정보 출력하도록 하려고
    public abstract bool SetOptionText(int optionNumber, ItemOptionUnit optionUnit);
}