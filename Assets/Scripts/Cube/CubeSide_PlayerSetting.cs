using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_PlayerSetting : CubeSide
{
    public override void Enter()
    {
        //Debug.Log("PlayerSetting");
        onEnterFinish?.Invoke();
    }

    public override void Exit()
    {
        onExitFinish?.Invoke(this);
    }
}
