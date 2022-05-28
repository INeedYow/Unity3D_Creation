using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSlot : MonoBehaviour
{
    public Rune rune;

    private void OnMouseDown()
    {
        if (rune.IsMax()) return;
        rune.AddPoint();
    }

    private void OnMouseEnter() {
        Debug.Log("Rune 효과 : " + rune.data.description);
    }
}
