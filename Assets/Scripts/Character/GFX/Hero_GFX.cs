using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_GFX : GFX
{
    public Hero hero;

    new protected void OnEnable() { 
        base.OnEnable();
        hero.isStop = true; 
    }

    void Update()
    {
        if (hero.isDead || hero.isStop) return;

        for (int i = 0; i < repeat; i++)
        {
            if (hero.conditionMacros[i] == null) continue;

            if (hero.conditionMacros[i].IsSatisfy())
            {
                if (hero.actionMacros[i] == null) continue;

                if (hero.actionMacros[i].Execute()) break;
            }
        }
    }

    protected override void LookTarget()
    {
        if (hero.target != null && hero.target != hero)
        {
            hero.transform.LookAt(hero.target.transform);
        }
    }

    void Hit(){ //Debug.Log("GFX.Hit()");
        if (null == hero.target) return;
        IDamagable target = hero.target.GetComponent<IDamagable>();
        target?.Damaged(hero.curDamage, hero.powerRate);
    }

    void Launch(){
        hero.LaunchProjectile();
    }

}
