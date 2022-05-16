using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GFX : MonoBehaviour
{
    public Monster monster;

    private void OnEnable() {
        monster.isStop = true;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(monster.transform.position, 1.5f);
    }

}
