using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_GFX : MonoBehaviour
{
    public Hero hero;
    int m_repeat;

    private void Awake() { m_repeat = HeroManager.instance.maxMacroCount; }
    private void OnEnable() {
        hero.isStop = true;
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
                else continue;
            }
        }
    }
}
