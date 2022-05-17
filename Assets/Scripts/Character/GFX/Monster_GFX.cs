using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GFX : MonoBehaviour
{
    public Monster monster;

    private void OnEnable() {
        monster.isStop = true;
    }

    private void Update() {
        // TODO
        if (monster.isStop || monster.isDead) return;  

        for (int i = 0; i < 4; i++){
            if (monster.conditionMacros[i] == null) continue;

            if (monster.conditionMacros[i].IsSatisfy())
            {
                if (monster.actionMacros[i] == null) continue;

                if (monster.actionMacros[i].Execute()) break;
                else continue;
            }
        }  
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(monster.transform.position, 2f);
        Gizmos.DrawWireSphere(monster.transform.position, 3f);
        Gizmos.DrawWireSphere(monster.transform.position, 4f);
        Gizmos.DrawWireSphere(monster.transform.position, 5f);
    }
}
