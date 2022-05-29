using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStem : MonoBehaviour
{
    public RuneTree tree;
    [HideInInspector] public Rune[] runes;
    public RuneStem nextStem;
    public int reqPoint;

    private void Start() {
        if (tree == null) tree = GetComponentInParent<RuneTree>();

        runes = new Rune[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            runes[i] = transform.GetChild(i).GetComponent<Rune>();
        }
    }
    public bool isOpen { get; private set; }

    public void OpenStem()
    {   Debug.Log(isOpen);
        if (isOpen) return;

        isOpen = true;
        tree.onDecreaseUsePoint += CheckPoint;

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

    public void CloseStem()
    {   Debug.Log("Close");
        if (!isOpen) return;

        isOpen = false;
        tree.onDecreaseUsePoint -= CheckPoint;

        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].gameObject.SetActive(false);
        }
    }

    public virtual void AddPoint(Rune rune)
    {
        tree.usePoints++;

        if (nextStem == null) return;

        if (tree.usePoints >= nextStem.reqPoint)
        {
            nextStem.OpenStem();
        }
    }

    public void Apply()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].Apply();
        }
    }

    public void Release()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            runes[i].Release();
        }
    }
}
