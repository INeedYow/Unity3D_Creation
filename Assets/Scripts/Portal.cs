using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int index;
    private void OnTriggerEnter(Collider other) {
        DungeonManager.instance.Enter(index);
    }
}