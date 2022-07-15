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

        
        GameManager.instance.isLockFocus = true;
    }

    public override void Exit()
    {
        heroGuide.SetActive(false);
        GameManager.instance.isLockFocus = false;
        HeroManager.instance.selectedHero = null;
        HeroManager.instance.ShowHeroUI(false);
        ItemManager.instance.itemInfoUI.gameObject.SetActive(false);
        onExitFinish?.Invoke(this);
    }
}
