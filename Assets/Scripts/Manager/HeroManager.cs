using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroManager : MonoBehaviour
{  
    public UnityAction<Hero> onChangeSelectedHero;
    public static HeroManager instance { get; private set; }

    public List<Hero> heroList = new List<Hero>();

    public Hero selectedHero;

    [Header("UI")]
    public HeroInfoUI heroInfoUI;
    public HeroListUI heroListUI;
    public HeroSetUI heroSetUI;

    [Header("Hero Prf")]
    public Hero prfKngiht;
    public Hero prfArcher;

    [Header("Hero prfSkill")]
    public Skill[] prfKnightSkills;
    public Skill[] prfArcherSkills;

    private void Awake() { instance = this; }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetNewHero(Hero.EClass.Knight);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            GetNewHero(Hero.EClass.Archer);
        }
    }

    public void SelectHero(Hero hero){
        if (PartyManager.instance.board.isActive)
        {
            if (null != selectedHero)                   // 이미 선택한 영웅이 있던 경우
            {   
                selectedHero.dummy.gameObject.SetActive(false);
            }
            else if (hero.dummy.placedBlock != null)    // 해당 영웅이 이미 배치된 경우
            {   
                hero.dummy.placedBlock.dummy = null;
                hero.dummy.placedBlock = null;
            }
            
            selectedHero = hero;
            onChangeSelectedHero?.Invoke(selectedHero);
            hero.dummy.gameObject.SetActive(true);
            heroInfoUI.RenewUI(selectedHero);
        }
        else{
            //if (selectedHero == hero) return;
            selectedHero = hero;
            MacroManager.instance.macroUI.RenewUI(hero);
            heroInfoUI.RenewUI(selectedHero);
            onChangeSelectedHero?.Invoke(selectedHero);
        }
    }

    public void GetNewHero(Hero.EClass eClass){
        Hero hero = null;
        switch(eClass)
        {
            case Hero.EClass.Knight:
            {
                hero = Instantiate(prfKngiht);
                // 매크로 생성
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {   
                    hero.conditionMacros[i] = Instantiate(MacroManager.instance.prfConditionMacros[0], hero.transform);
                    hero.conditionMacros[i].owner = hero;
                    //hero.conditionMacros[i].transform.SetParent(hero.transform);
                }
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {
                    hero.actionMacros[i] = Instantiate(MacroManager.instance.prfActionMacros[0], hero.transform);
                    hero.actionMacros[i].owner = hero;
                    //hero.actionMacros[i].transform.SetParent(hero.transform);
                }
                // 스킬 생성
                for (int i = 0; i < prfKnightSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfKnightSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            case Hero.EClass.Archer:
            {
                hero = Instantiate(prfArcher);

                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {
                    hero.conditionMacros[i] = Instantiate(MacroManager.instance.prfConditionMacros[0], hero.transform);
                    hero.conditionMacros[i].owner = hero;
                }
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {
                    hero.actionMacros[i] = Instantiate(MacroManager.instance.prfActionMacros[0], hero.transform);
                    hero.actionMacros[i].owner = hero;
                }

                for (int i = 0; i < prfArcherSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfArcherSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            // 직업 추가

        }

        heroList.Add(hero);
        heroListUI.AddHeroUnit(hero);
    }

    public void ToggleSetUI(){
        heroSetUI.MenuToggle();
    }

}
