using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Hero : CubeSide
{
    public override void Enter()
    {   //Debug.Log("Board");
        HeroManager.instance.heroInfoUI.gameObject.SetActive(true);
        HeroManager.instance.heroListUI.gameObject.SetActive(true);
        //PartyManager.instance.board.gameObject.SetActive(true);
        //PartyManager.instance.board.Init();
        onEnterFinish?.Invoke();
    }

    public override void Exit()
    {
        HeroManager.instance.heroInfoUI.gameObject.SetActive(false);
        HeroManager.instance.heroListUI.gameObject.SetActive(false);
        //PartyManager.instance.TurnOffBoard();
        onExitFinish?.Invoke(this);
    }
}
