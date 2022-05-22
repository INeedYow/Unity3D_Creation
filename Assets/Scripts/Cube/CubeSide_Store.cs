using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Store : CubeSide
{
    public override void Enter()
    {
        //Debug.Log("Store");
        onEnterFinish?.Invoke();
    }

    public override void Exit()
    {
        onExitFinish?.Invoke(this);
    }
}
