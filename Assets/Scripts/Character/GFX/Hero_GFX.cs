using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_GFX : GFX
{
    public Hero hero;
    bool hasChange;

    private void Start() { repeat = MacroManager.instance.maxMacroCount; }
    // new protected void OnEnable() { 
    //     base.OnEnable();
    //     hero.isStop = true; 
    // }

    private void OnEnable() {
        hero.isStop = true;
    }

    
    void Update()
    {
        if (hero.isDead || hero.isStop) return;

        for (int i = 0; i < repeat; i++)
        {   //Debug.Log(i);
            if (hero.actionMacros[i] == null) continue;
            //Debug.Log("AM isReady()");
            if (hero.actionMacros[i].IsReady())
            {   //Debug.Log("AM isReady() true");
                if (hero.conditionMacros[i] == null) continue;
                //Debug.Log("AM isSatisfy()");
                if (hero.conditionMacros[i].IsSatisfy())
                {   //Debug.Log("AM isSatisfy() true");
                    hero.onMacroChangeGetIndex?.Invoke(i);
                    hero.actionMacros[i].Execute(hero.conditionMacros[i].GetTarget());
                    break;
                }
                
            }
        }


        // 타겟이 action 매크로 진행 중간에 바뀌는 현상 발생
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

    // protected override void LookTarget()
    // {
    //     if (hero.target != null && hero.target != hero)
    //     {
    //         hero.transform.LookAt(hero.target.transform);
    //     }
    // }

    //// Animation Event 함수들 ////
    void OnAttack() { hero.attackCommand.Attack(); }

    void OnEffectSkill(int number){ 
        //if (hero.eClass == Hero.EClass.Angel)
            //Debug.Log(string.Format("OnSkillEffect // Target : {0}", hero.target));

        hero.skills[number - 1].EffectSkill();
    }

    void OnFinishSkill(int number){  
        //if (hero.eClass == Hero.EClass.Angel)
            //Debug.Log(string.Format("OnSkillFinish // Target : {0}", hero.target));

        hero.skills[number - 1].FinishSKill();
        hero.anim.SetBool(string.Format("Skill {0}", number), false);
    }
    
}
