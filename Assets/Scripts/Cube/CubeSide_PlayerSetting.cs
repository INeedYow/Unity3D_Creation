using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_PlayerSetting : CubeSide
{
    public override void Enter()
    {
        //Debug.Log("PlayerSetting");
    }

    public override void Exit()
    {
        onExitFinish?.Invoke(this);
    }
}
