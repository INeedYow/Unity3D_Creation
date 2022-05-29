using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    public RuneStem stem;
    public int point;

    private void Start() {
        if (stem == null) stem = GetComponentInParent<RuneStem>();
    }

    private void OnMouseDown() {
        if (!stem.isOpen) return;
        if (data.IsMax(point)) return;

        point++; //Debug.Log(point + " / " + data.GetMax());
        stem.AddPoint(this);
        PlayerManager.instance.AddRunePoint();
    }

    private void OnMouseEnter() {
        //Debug.Log(data.description);
        PlayerManager.instance.ShowRuneInfoUI(this);
    }

    private void OnMouseExit() {
        PlayerManager.instance.HideRuneInfoUI();
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
}
