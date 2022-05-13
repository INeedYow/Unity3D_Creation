using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GFX : MonoBehaviour
{
    public Monster monster;


     void Awake() 
    {
        monster = GetComponentInParent<Monster>();
    }

    private void OnEnable() {
        monster.isStop = true;
    }

}
