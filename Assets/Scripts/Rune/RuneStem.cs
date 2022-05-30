using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStem : MonoBehaviour
{
    public RuneTree tree;
    [HideInInspector] public Rune[] runes;
    public RuneStem nextStem;
    public int maxPoint;
    [HideInInspector] public int usePoints;
    public bool isOpen { get; private set; }

    private void Awake() { 
        if (tree == null) tree = GetComponentInParent<RuneTree>();

        runes = new Rune[transform.childCount];     
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i] = transform.GetChild(i).GetComponent<Rune>();
        }
    }

    public void AddPoint()
    {
        usePoints++;
        tree.point--;
        tree.infoUI.RenewUI();

        if (usePoints >= maxPoint)
        {
            if (nextStem == null) return;

            if (nextStem.isOpen) return;

            nextStem.Open();
            Max();
        }
    }

    public void Open()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].button.interactable = true;
        }
        isOpen = true;
    }

    public void Max()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].button.interactable = false;
        }
    }


    public void Apply()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].Apply();
        }

        if (nextStem != null)
        {
            if (nextStem.isOpen)
            {
                nextStem.Apply();
            }
        }
    }

    public void Release()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].Release();
        }

        if (nextStem != null)
        {
            if (nextStem.isOpen)
            {
                nextStem.Release();
            }
        }
    }

    public void Reset()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].Reset();
            runes[i].button.interactable = false;
        }
        usePoints = 0;
        isOpen = false;

        if (nextStem != null)
        {
            if (nextStem.isOpen)
            {
                nextStem.Reset();
            }
        }
    }
}
