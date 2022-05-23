using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Hero : CubeSide
{
    public override void Enter()
    {   //Debug.Log("Board");
        HeroManager.instance.ShowHeroUI(true);
        onEnterFinish?.Invoke();
    }

    public override void Exit()
    {
        HeroManager.instance.ShowHeroUI(false);
        onExitFinish?.Invoke(this);
    }
}
