using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_PlayerSetting : CubeSide
{
    //public GameObject plane;
    //public RuneCursor cursor;

    public override void Enter()
    {   //Debug.Log("Enter");
        CameraManager.instance.SetRuneTreeView();
        CameraManager.instance.onMoveFinish += EnterFinish;
    }

    public override void Exit()
    {   //Debug.Log("Exit");
        //plane.gameObject.SetActive(false);
        //cursor.gameObject.SetActive(false);
        CameraManager.instance.SetPlanetView();
        CameraManager.instance.onMoveFinish += ExitFinish;
    }




    public void EnterFinish()
    {
        onEnterFinish?.Invoke();
        PlayerManager.instance.EnterRuneTree(true);
        //plane.gameObject.SetActive(true);
        //cursor.gameObject.SetActive(true);
        CameraManager.instance.onMoveFinish -= EnterFinish;
    }

    public void ExitFinish()
    {
        onExitFinish?.Invoke(this);
        PlayerManager.instance.EnterRuneTree(false);
        CameraManager.instance.onMoveFinish -= ExitFinish;
    }
}
