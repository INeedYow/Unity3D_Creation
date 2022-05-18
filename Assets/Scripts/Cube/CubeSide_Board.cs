using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Board : CubeSide
{
    public override void Enter()
    {   Debug.Log("Board");
        HeroManager.instance.heroInfoUI.gameObject.SetActive(true);
        HeroManager.instance.heroListUI.gameObject.SetActive(true);
        PartyManager.instance.board.gameObject.SetActive(true);
        PartyManager.instance.board.Init();
    }

    public override void Exit()
    {
        HeroManager.instance.heroInfoUI.gameObject.SetActive(false);
        HeroManager.instance.heroListUI.gameObject.SetActive(false);
        PartyManager.instance.TurnOffBoard();
    }
}
