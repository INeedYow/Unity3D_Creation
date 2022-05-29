using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStem : MonoBehaviour
{
    public RuneTree tree;
    [HideInInspector] public Rune[] runes;
    public RuneStem nextStem;
    public int reqPoint;
    public bool isOpen { get; private set; }

    private void Awake() { 
        if (tree == null) tree = GetComponentInParent<RuneTree>();

        runes = new Rune[transform.childCount];     
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i] = transform.GetChild(i).GetComponent<Rune>();
        }
    }

    private void OnEnable() {
        tree.onChangeUsePoint += CheckPoint;
    }

    private void OnDisable() {
        tree.onChangeUsePoint -= CheckPoint;
    }

    public void OpenStem()
    {   
        if (isOpen) return;

        isOpen = true;
        //tree.onChangeUsePoint += CheckPoint;
        
        for (int i = 0; i < runes.Length; i++)
        {   
            runes[i].gameObject.SetActive(true);
        }
    }

    void CheckPoint()
    {
        if (tree.usePoints < reqPoint)
        {
            CloseStem();
        }
    }

    void CloseStem()
    {   //Debug.Log("Close");
        if (!isOpen) return;

        isOpen = false;
        //tree.onChangeUsePoint -= CheckPoint;

        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public virtual void AddPoint(Rune rune)
    {
        tree.usePoints++;

        if (nextStem == null) return;

        if (tree.usePoints >= nextStem.reqPoint)
        {
            nextStem.gameObject.SetActive(true);
            nextStem.OpenStem();
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
        if (!isOpen) return;

        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].Reset();
        }

        if (nextStem != null)
        {
            if (nextStem.isOpen)
            {
                nextStem.Reset();
            }
        }
        gameObject.SetActive(false);
    }
}
