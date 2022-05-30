using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_PlayerSetting : CubeSide
{
    public override void Enter()
    {   //Debug.Log("Enter");
        GameManager.instance.EnterTree(true);
        PlayerManager.instance.EnterRuneTree(true);
        onEnterFinish?.Invoke();

        //CameraManager.instance.SetRuneTreeView();
        //CameraManager.instance.onMoveFinish += EnterFinish;
    }

    public override void Exit()
    {   //Debug.Log("Exit");
        PlayerManager.instance.EnterRuneTree(false);
        GameManager.instance.EnterTree(false);
        onExitFinish?.Invoke(this);

        //CameraManager.instance.SetPlanetView();
        //CameraManager.instance.onMoveFinish += ExitFinish;
    }


    // public void EnterFinish()
    // {
        
    //     //PlayerManager.instance.EnterRuneTree(true);
    //     //plane.gameObject.SetActive(true);
    //     //cursor.gameObject.SetActive(true);
    //     //CameraManager.instance.onMoveFinish -= EnterFinish;
    // }

    // public void ExitFinish()
    // {
        
        
    //     //PlayerManager.instance.EnterRuneTree(false);
    //     //CameraManager.instance.onMoveFinish -= ExitFinish;
    // }
}
