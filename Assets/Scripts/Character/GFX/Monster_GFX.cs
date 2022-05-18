using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GFX : GFX
{
    public Monster monster;

    new protected void OnEnable() { 
        base.OnEnable();
        monster.isStop = true; 
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

    protected override void LookTarget()
    {
        if (monster.target != null && monster.target != monster)
        {
            monster.transform.LookAt(monster.target.transform);
        }
    }

    void Hit(){ Debug.Log("Mon_GFX.Hit()");
        IDamagable target = monster.target.GetComponent<IDamagable>();
        target?.Damaged(monster.curDamage, monster.powerRate);
    }
}
