﻿using System.Collections;
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
            if (selectedHero == hero) return;
            selectedHero = hero;
            onChangeSelectedHero?.Invoke(selectedHero);
            MacroManager.instance.macroUI.RenewUI(hero);
            heroInfoUI.RenewUI(selectedHero);
        }
    }

    public void GetNewHero(Hero.EClass eClass){
        Hero hero = null;
        switch(eClass)
        {
            case Hero.EClass.Knight:
            {
                hero = Instantiate(prfKngiht);
            
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {   
                    hero.conditionMacros[i] = Instantiate(MacroManager.instance.prfConditionMacros[0]);
                    hero.conditionMacros[i].owner = hero;
                }
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {
                    hero.actionMacros[i] = Instantiate(MacroManager.instance.prfActionMacros[0]);
                    hero.actionMacros[i].owner = hero;
                }
                break;
            }

            case Hero.EClass.Archer:
            {
                hero = Instantiate(prfArcher);
            
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {
                    hero.conditionMacros[i] = Instantiate(MacroManager.instance.prfConditionMacros[0]);
                    hero.conditionMacros[i].owner = hero;
                }
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {
                    hero.actionMacros[i] = Instantiate(MacroManager.instance.prfActionMacros[0]);
                    hero.actionMacros[i].owner = hero;
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
