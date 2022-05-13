using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMapUI : MonoBehaviour
{
    public int index;
    private void OnMouseDown() { Debug.Log(gameObject.name + " OnMouseDown");
        DungeonManager.instance.Enter(index);
    }    // 왜 안됨?

}
