using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_PlayerSetting : CubeSide
{   
    [Header("Guide UI")]
    public GameObject runeGuide;

    public override void Enter()
    {   //Debug.Log("Enter");
        runeGuide.SetActive(true);
        GameManager.instance.EnterTree(true);
        PlayerManager.instance.EnterRuneTree(true);
        onEnterFinish?.Invoke();
    }

    public override void Exit()
    {   //Debug.Log("Exit");
        runeGuide.SetActive(false);
        PlayerManager.instance.EnterRuneTree(false);
        GameManager.instance.EnterTree(false);
        onExitFinish?.Invoke(this);
    }
}
