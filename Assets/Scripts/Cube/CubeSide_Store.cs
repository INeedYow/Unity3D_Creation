using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Store : CubeSide
{
    [Header("Guide UI")]
    public GameObject storeGuide;
    public override void Enter()
    {
        storeGuide.SetActive(true);
        onEnterFinish?.Invoke();
        PlayerManager.instance.playerInfoUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        storeGuide.SetActive(false);
        ItemManager.instance.itemInfoUI.gameObject.SetActive(false);
        onExitFinish?.Invoke(this);
    }
}
