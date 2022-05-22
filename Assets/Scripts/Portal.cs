using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int index;
    
    private void OnMouseDown() {
        if (GameManager.instance.cube.curSide.GetComponent<CubeSide_Board>() == null) return;
        DungeonManager.instance.Enter(index);
    }
}