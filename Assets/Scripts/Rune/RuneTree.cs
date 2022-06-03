using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneTree : MonoBehaviour
{
    [Header("Stat")]
    public RuneStem firstStem;
    public Text leftPoint;


    [Header("Skill")]
    public RuneSlot[] skillSlots = new RuneSlot[4];
    public Text level;

    [Header("UI")]
    public RuneInfoUI infoUI;

    int _point;
    public int point{
        get { return _point; }
        set { 
            _point = value;
            leftPoint.text = _point.ToString();
        }
    }

    private void Start() {
        Open();
        leftPoint.text = _point.ToString();
        level.text = "1";
    }

    void Open() { firstStem.Open(); }

    public void Apply()
    {
        firstStem.Apply();

        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].Apply();
        }

        PlayerManager.instance.runePlane.gameObject.SetActive(true);
    }

    public void Release()
    {
        firstStem.Release();
        
        for (int i = 0; i < skillSlots.Length; i++)
        {   
            skillSlots[i].Release();
        }

        PlayerManager.instance.runePlane.gameObject.SetActive(false);
    }

    public void Reset()
    {
        firstStem.Reset();
        Open();
    }

    //// UI

    public void ShowRuneInfoUI(Rune rune)
    {
        infoUI.gameObject.SetActive(true);
        infoUI.SetRune(rune);
    }

    public void HideRuneInfoUI() { infoUI.gameObject.SetActive(false); }
}

