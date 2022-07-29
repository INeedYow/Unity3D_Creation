using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_GFX : GFX
{
    public Hero hero;

    private void Start() { repeat = MacroManager.instance.maxMacroCount; }


    private void OnEnable() {
        hero.isStop = true;
    }

    
    void Update()
    {
        if (hero.isStop) return;

        for (int i = 0; i < repeat; i++)
        {   
            if (hero.actionMacros[i] == null) continue;
            
            if (hero.actionMacros[i].IsReady())
            {   
                if (hero.conditionMacros[i] == null) continue;
                
                if (hero.conditionMacros[i].IsSatisfy())
                {   
                    hero.onMacroChangeGetIndex?.Invoke(i);
                    hero.actionMacros[i].Execute(hero.conditionMacros[i].GetTarget());
                    break;
                }
                
            }
        }


        // for (int i = 0; i < repeat; i++)
        // {
        //     if (hero.conditionMacros[i] == null) continue;

        //     if (hero.prevMacro != i) {  // 이전 매크로 저장, 비교
        //         hasChange = true;
        //         hero.onMacroChangeGetIndex?.Invoke(i);  
        //         hero.prevMacro = i;
        //     }
        //     else{ hasChange = false; }

        //     if (hero.conditionMacros[i].IsSatisfy(hasChange))
        //     {   
        //         if (hero.actionMacros[i] == null) continue;
                
        //         if (hero.actionMacros[i].Execute()) { break; }
        //         //else { hero.target = null; }
        //     }
        //     //else { hero.target = null; }
        // }
    }

    private void FixedUpdate() {
        CheckAnimation();
    }
    
    void CheckAnimation()
    {
        hero.anim.SetFloat("MoveSpeed", hero.nav.velocity.sqrMagnitude);
    }

    public override void StopSKill(int id)
    {
        OnFinishSkill(id);
    }

    // protected override void LookTarget()
    // {
    //     if (hero.target != null && hero.target != hero)
    //     {
    //         hero.transform.LookAt(hero.target.transform);
    //     }
    // }

    //// Animation Event 함수들 ////
    void OnAttack() { hero.attackCommand.Attack(); }

    void OnEffectSkill(int number) { hero.skills[number - 1].EffectSkill(); }

    void OnFinishSkill(int number)
    {  
        hero.skills[number - 1].FinishSKill();
        hero.anim.SetBool(string.Format("Skill {0}", number), false);
    }
    
}