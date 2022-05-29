using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneTree : MonoBehaviour
{
    public UnityAction onDecreaseUsePoint;
    public RuneStem firstStem;
    public Canvas canvas;

    int _usePoints;
    public int usePoints{
        get { return _usePoints; }
        set {
            _usePoints = value;
            onDecreaseUsePoint?.Invoke();
        }
    }

    private void OnEnable() {
        Open();
    }
    
    void Open()
    {
        firstStem.OpenStem();
    }

    public void Apply()
    {
        firstStem.Apply();
    }
}

// TODO 던전 진입 시 Tree.Apply(); / 나갈 때 Tree.Release();