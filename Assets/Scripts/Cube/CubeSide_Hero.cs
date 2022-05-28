using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Hero : CubeSide
{
    public override void Enter()
    {   
        HeroManager.instance.selectedHero = null;
        HeroManager.instance.ShowHeroUI(true);
        onEnterFinish?.Invoke();
    }

    public override void Exit()
    {
        HeroManager.instance.selectedHero = null;
        HeroManager.instance.ShowHeroUI(false);
        onExitFinish?.Invoke(this);
    }
}
