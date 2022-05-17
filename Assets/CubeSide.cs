using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeSide : MonoBehaviour
{
    public CubePlanet cube;
    public CubeSide forwardSide;
    public CubeSide backwardSide;
    public CubeSide leftSide;
    public CubeSide rightSide;
    
    public abstract void Enter();
    public abstract void Exit();
}
