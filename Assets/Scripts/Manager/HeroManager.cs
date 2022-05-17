using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroManager : MonoBehaviour
{  
    public UnityAction<Hero> onChangeSelectedHero;
    public static HeroManager instance { get; private set; }

    public List<Hero> heroList = new List<Hero>();
    Hero _selectedHero;
    public Hero selectedHero{
        get { return _selectedHero; }
        set { 
            test = value;
            if (value == null) Debug.Log("SelectHero null");
            if (_selectedHero != value)
            {
                Debug.Log("_SelectedHero = " + _selectedHero + " / " + "value = " + value);
                //onChangeSelectedHero?.Invoke(_selectedHero);
            }
            _selectedHero = value;
        }
    }

    public Hero test;

    [Header("UI")]
    public HeroInfoUI heroInfoUI;
    public HeroListUI heroListUI;
    public HeroSetUI heroSetUI;

    [Header("Hero Prf")]
    public Hero prfKngiht;
    public Hero prfArcher;
    
    public int maxMacroCount { get { return 5; } }

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
        // switch (GameManager.instance.eMenu)
        // {
        // case EMenu.Board:
        // {
        //     if (null != selectedHero)
        //     {   
        //         selectedHero.dummy.gameObject.SetActive(false);
        //     }
        //     else if (hero.dummy.placedBlock != null)    // 이미 배치된 경우
        //     {   
        //         hero.dummy.placedBlock.dummy = null;
        //         hero.dummy.placedBlock = null;
        //     }
            
        //     selectedHero = hero;
        //     hero.dummy.gameObject.SetActive(true);
        //     heroInfoUI.RenewUI(selectedHero);
        //     break;
        // }
        // case EMenu.Setting:
        // {
        //     if (selectedHero == hero) return;
        //     selectedHero = hero;
        //     MacroManager.instance.macroUI.RenewUI(hero);
        //     heroInfoUI.RenewUI(selectedHero);
        //     break;
        // }
        // default:
        // {
        //     selectedHero = hero;
        //     break;
        // }
        // }
        //
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
            hero.dummy.gameObject.SetActive(true);
            heroInfoUI.RenewUI(selectedHero);
        }
        else{
            if (selectedHero == hero) return;
            selectedHero = hero;
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
