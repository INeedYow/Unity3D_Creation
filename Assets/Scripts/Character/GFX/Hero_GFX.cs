﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_GFX : MonoBehaviour
{
    public Hero hero;

    private void OnEnable() {
        hero.isStop = true;
    }

    void Update()
    {
        if (hero.isDead || hero.isStop) return;

        for (int i = 0; i < hero.maxMacroCount; i++)
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
