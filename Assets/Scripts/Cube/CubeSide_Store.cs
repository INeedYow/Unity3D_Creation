using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Store : CubeSide
{
    public override void Enter()
    {
        onEnterFinish?.Invoke();
        PlayerManager.instance.playerInfoUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        ItemManager.instance.itemInfoUI.gameObject.SetActive(false);
        onExitFinish?.Invoke(this);
    }
}
