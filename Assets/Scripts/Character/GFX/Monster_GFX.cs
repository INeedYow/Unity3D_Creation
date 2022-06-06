using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_GFX : GFX
{
    public Monster monster;


    private void OnEnable() {
        monster.isStop = true;
    }
      
    private void Update() 
    {   
        if (monster.isStop || monster.isDead) return;  

        for (int i = 0; i < repeat; i++)
        {   
            if (monster.actionMacros[i] == null) continue;
            
            if (monster.actionMacros[i].IsReady())
            {   
                if (monster.conditionMacros[i] == null) continue;
                
                if (monster.conditionMacros[i].IsSatisfy())
                {  
                    monster.onMacroChangeGetIndex?.Invoke(i);
                    monster.actionMacros[i].Execute(monster.conditionMacros[i].GetTarget());
                    break;
                }
                
            }
        }

        // for (int i = 0; i < repeat; i++){
        //     if (monster.conditionMacros[i] == null) break;

        //     if (monster.conditionMacros[i].IsSatisfy(false))
        //     {
        //         if (monster.actionMacros[i] == null) break;

        //         if (monster.actionMacros[i].Execute()) break;
        //         else { monster.target = null; }
        //     }
        //     else { monster.target = null; }
        // }  
    }

    // public void SetMacroSize(int size){ 
    //     this.repeat = size;
    //     monster.conditionMacros = new ConditionMacro[size];
    //     monster.actionMacros = new ActionMacro[size];
    // }

    private void FixedUpdate() {
       CheckAnimation();
    }
    
    void CheckAnimation()
    {
        monster.anim.SetFloat("MoveSpeed", monster.nav.velocity.sqrMagnitude);
    }

    // protected override void LookTarget()
    // {
    //     if (monster.target != null && monster.target != monster)
    //     {
    //         monster.transform.LookAt(monster.target.transform);
    //     }
    // }

     //// Animation Event 함수 ////
    void OnAttack() { monster.attackCommand.Attack(); }

    void OnFinishSkill(int number){   
        monster.skills[number - 1].FinishSKill();
        monster.anim.SetBool(string.Format("Skill {0}", number), false);
    }

    void OnEffectSkill(int number){
        monster.skills[number - 1].EffectSkill();
    }
}
