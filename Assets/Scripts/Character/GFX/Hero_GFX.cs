using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_GFX : GFX
{
    public Hero hero;

    private void Start() { repeat = MacroManager.instance.maxMacroCount; }
    new protected void OnEnable() { 
        base.OnEnable();
        hero.isStop = true; 
    }

    // 조건 매크로 체크해서 아닌 경우 target null 만들어서 다음 조건에서 타겟 찾도록
    // 액션 매크로 역시 아니여서 다음 매크로로 넘어가는 경우 target null 만들어줘서 다음 조건에서 다시 찾도록
    void Update()
    {
        if (hero.isDead || hero.isStop) return;

        for (int i = 0; i < repeat; i++)
        {
            if (hero.conditionMacros[i] == null) continue;

            
            if (hero.conditionMacros[i].IsSatisfy())
            {   
                if (hero.actionMacros[i] == null) continue;
                
                hero.onMacroChangeGetIndex?.Invoke(i);                  
                if (hero.actionMacros[i].Execute()) { break; }
                else { hero.target = null; }
            }
            else { hero.target = null; }
        }
    }

    private void FixedUpdate() {
        CheckAnimation();
    }
    
    void CheckAnimation()
    {
        hero.anim.SetFloat("MoveSpeed", hero.nav.velocity.sqrMagnitude);
    }

    protected override void LookTarget()
    {
        if (hero.target != null && hero.target != hero)
        {
            hero.transform.LookAt(hero.target.transform);
        }
    }

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
