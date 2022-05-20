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

        for (int i = 0; i < repeat; i++){
            if (monster.conditionMacros[i] == null) break;

            if (monster.conditionMacros[i].IsSatisfy())
            {
                if (monster.actionMacros[i] == null) break;

                if (monster.actionMacros[i].Execute()) break;
            }
        }  
    }

    //private void FixedUpdate() {
    //    CheckAnimation();
    //}
    
    void CheckAnimation()
    {
        //monster.anim.SetFloat("MoveSpeed", monster.nav.velocity.sqrMagnitude);
    }

    protected override void LookTarget()
    {
        if (monster.target != null && monster.target != monster)
        {
            monster.transform.LookAt(monster.target.transform);
        }
    }

    //void OnAttack() { monster.attackCommand.Attack(); }

    void OnFinishSkill(int number){   
        //monster.skills[number - 1].FinishSKill();
        //monster.anim.SetBool(string.Format("Skill {0}", number), false);
    }

    void OnEffectSkill(int number){
        //monster.skills[number - 1].EffectSkill();
    }
}
