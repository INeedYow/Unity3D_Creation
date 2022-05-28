using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneData data;           
    public int point;               // 투자한 rune point
    public RuneSlot slot;           
    public Rune[] nextRunes;
    
    public bool isOpen { get; private set; }

    public void AddPoint()
    {
        point++;
        if (IsMax())
        {
            OpenNextRune();
        }
    }

    public bool IsMax() { return data.IsMax(point); }

    void OpenNextRune()
    {
        if (nextRunes.Length == 0) return;

        for(int i = 0; i < nextRunes.Length; i++)
        {
            if (!nextRunes[i].isOpen) 
            {
                nextRunes[i].OpenRuneSlot();
            }
        }
    }

    public void OpenRuneSlot()
    {
        isOpen = true;
        point = 0;
        slot.gameObject.SetActive(true);
    }

    public void Apply()
    {
        data.Apply(point);
        
        for(int i = 0; i < nextRunes.Length; i++)
        {
            if (nextRunes[i].isOpen)
            {
                nextRunes[i].Apply();
            }
        }
    }

    public void Release()
    {
        data.Release(point);
        
        for(int i = 0; i < nextRunes.Length; i++)
        {
            if (nextRunes[i].isOpen)
            {
                nextRunes[i].Release();
            }
        }
    }
}
