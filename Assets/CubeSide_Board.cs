using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Board : CubeSide
{
    public override void Enter()
    {
        HeroManager.instance.heroInfoUI.gameObject.SetActive(true);
        HeroManager.instance.heroListUI.gameObject.SetActive(true);
        PartyManager.instance.board.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        HeroManager.instance.heroInfoUI.gameObject.SetActive(false);
        HeroManager.instance.heroListUI.gameObject.SetActive(false);
        PartyManager.instance.board.gameObject.SetActive(false);
    }
}
