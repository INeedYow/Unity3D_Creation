using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GFX : MonoBehaviour
{
    public Monster monster;

    private void OnEnable() {
        monster.isStop = true;
    }

}
