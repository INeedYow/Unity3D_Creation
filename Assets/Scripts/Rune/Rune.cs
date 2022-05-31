using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rune : MonoBehaviour
{
    public RuneData data;
    public RuneStem stem;
    public Button button;
    public int point;

    public Text cur;
    public Text max;
    
    private void Start() {
        if (stem == null) stem = GetComponentInParent<RuneStem>();
        max.text = data.GetMax().ToString();
        cur.text = "0";
    }

    public void AddPoint()
    {
        if (PlayerManager.instance.runeTree.point <= 0) return;
        if (data.IsMax(point)) return;

        point++;
        stem.AddPoint();

        cur.text = point.ToString();
    }

    public void Apply()
    {
        if (point == 0) return;

        data.Apply(point);
    }

    public void Release()
    {
        if (point == 0) return;
        
        data.Release(point);
    }

    public void Reset()
    {
        if (point == 0) return;

        stem.tree.point += point;
        point = 0;

        cur.text = point.ToString();
    }
    
    public void ShowRuneInfoUI()
    {
        stem.tree.ShowRuneInfoUI(this);
    }

    public void HideRuneInfoUI()
    {
        stem.tree.HideRuneInfoUI();
    }
}
