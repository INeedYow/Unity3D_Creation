using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Store : CubeSide
{
    public override void Enter()
    {
        //Debug.Log("Store");
        onEnterFinish?.Invoke();
        GameManager.instance.playerInfoUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        onExitFinish?.Invoke(this);
    }
}
