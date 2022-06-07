using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPortal : MonoBehaviour
{
    [Range(0, 4)] public int portalIndex;


    private void Awake() {
        Init();
    }

    void Init()
    {
        gameObject.AddComponent<Portal>().index = portalIndex;
    }
}
