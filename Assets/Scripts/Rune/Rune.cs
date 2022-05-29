using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;
    public RuneStem stem;
    int _point;
    public int point{
        get { return _point; }
        set {
            _point = value;
            if (value >= 1) StartCoroutine("Rotate");
            else {
                StopCoroutine("Rotate");
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    private void Start() {
        if (stem == null) stem = GetComponentInParent<RuneStem>();
    }

    private void OnMouseDown() {
        if (!stem.isOpen) return;
        if (data.IsMax(point)) return;

        point++; //Debug.Log(point + " / " + data.GetMax());
        stem.AddPoint(this);
        PlayerManager.instance.UseRunePoint();
    }

    private void OnMouseEnter() {
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

    public void Reset()
    {
        if (point == 0) return;

        PlayerManager.instance.runePoint += point;
        point = 0;
    }


    IEnumerator Rotate()
    {
        float rot = 0f;

        while (true)
        {
            transform.rotation = Quaternion.Euler(rot, 2 * rot, 3 * rot);
            rot += Time.deltaTime * 10;
            rot %= 360;
            yield return null;
        }
    }
}
