using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GFX : MonoBehaviour
{
    public Monster monster;
    int m_repeat;

    private void Start() { m_repeat = MacroManager.instance.maxMacroCount; }
    private void OnEnable() { 
        monster.isStop = true; 
        InvokeRepeating("LookTarget", 0f, 0.2f);
    }
    void OnDisable() {
        CancelInvoke("LookTarget");
    }

    private void Update() 
    {   // 몬스터는 내가 매크로 설정해줄거라 null이면 break;했음
        if (monster.isStop || monster.isDead) return;  

        for (int i = 0; i < 4; i++){
            if (monster.conditionMacros[i] == null) break;

            if (monster.conditionMacros[i].IsSatisfy())
            {
                if (monster.actionMacros[i] == null) break;

                if (monster.actionMacros[i].Execute()) break;
            }
        }  
    }

    void LookTarget()
    {
        if (monster.target != null && monster.target != monster)
        {
            monster.transform.LookAt(monster.target.transform);
        }
    }
}
