using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int index;
    
    private void OnMouseDown() {
        DungeonManager.instance.Enter(index);
    }
}