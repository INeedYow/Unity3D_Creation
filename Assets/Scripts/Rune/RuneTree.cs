using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneTree : MonoBehaviour
{
    public RuneStem firstStem;
    public Text leftPoint;
    [Header("UI")]
    public RuneInfoUI infoUI;

    int _point = 24;
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
    }

    void Open()
    {
        firstStem.Open();
    }

    public void Apply(){
        firstStem.Apply();
    }

    public void Release(){
        firstStem.Release();
    }

    public void Reset()
    {
        firstStem.Reset();
        Open();
    }

    public void ShowRuneInfoUI(Rune rune)
    {
        infoUI.gameObject.SetActive(true);
        infoUI.SetRune(rune);
    }

    public void HideRuneInfoUI()
    {
        infoUI.gameObject.SetActive(false);
    }
}

