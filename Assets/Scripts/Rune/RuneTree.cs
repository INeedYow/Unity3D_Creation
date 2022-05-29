using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneTree : MonoBehaviour
{
    public UnityAction onChangeUsePoint;
    public RuneStem firstStem;
    public LineRenderer line;
    public Canvas canvas;

    int _usePoints;
    public int usePoints{
        get { return _usePoints; }
        set {
            _usePoints = value;
            onChangeUsePoint?.Invoke();
        }
    }

    // public void Init(){
    //     firstStem.Init
    // }

    private void OnEnable() {
        Open();
    }
    
    void Open()
    {
        firstStem.gameObject.SetActive(true);
        firstStem.OpenStem();
    }

    public void Apply(){
        firstStem.Apply();
    }

    public void Release(){
        firstStem.Release();
    }

    public void Reset()
    {
        firstStem.Reset();
        usePoints = 0;
        Open();
    }


}

