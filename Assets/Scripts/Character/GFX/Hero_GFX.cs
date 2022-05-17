using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_GFX : MonoBehaviour
{
    public Hero hero;
    int m_repeat;

    private void Start() { m_repeat = MacroManager.instance.maxMacroCount; }
    private void OnEnable() { 
        hero.isStop = true; 
        InvokeRepeating("LookTarget", 0f, 0.2f);
    }

    private void OnDisable() {
        CancelInvoke("LookTarget");
    }

    void Update()
    {
        if (hero.isDead || hero.isStop) return;

        for (int i = 0; i < m_repeat; i++)
        {
            if (hero.conditionMacros[i] == null) continue;

            if (hero.conditionMacros[i].IsSatisfy())
            {
                if (hero.actionMacros[i] == null) continue;

                if (hero.actionMacros[i].Execute()) break;
            }
        }
    }

    void LookTarget()
    {
        if (hero.target != null && hero.target != hero)
        {
            hero.transform.LookAt(hero.target.transform);
        }
    }
}
