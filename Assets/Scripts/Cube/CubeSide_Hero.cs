using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Hero : CubeSide
{
    [Header("Guide UI")]
    public GameObject heroGuide;
    public override void Enter()
    {   
        heroGuide.SetActive(true);
        HeroManager.instance.selectedHero = null;
        HeroManager.instance.ShowHeroUI(true);
        onEnterFinish?.Invoke();
    }

    public override void Exit()
    {
        heroGuide.SetActive(false);
        HeroManager.instance.selectedHero = null;
        HeroManager.instance.ShowHeroUI(false);
        onExitFinish?.Invoke(this);
    }
}
