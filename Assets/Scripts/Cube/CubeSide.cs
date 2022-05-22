using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CubeSide : MonoBehaviour
{
    public UnityAction onEnterFinish;
    public UnityAction<CubeSide> onExitFinish;

    public CubePlanet cube;
    public CubeSide forwardSide;
    public CubeSide backwardSide;
    public CubeSide leftSide;
    public CubeSide rightSide;

    public Vector3 rot;            // 해당 큐브면의 회전
    
    public abstract void Enter();
    public abstract void Exit();
}
