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
            if (_selectedHero != value)
            {
                Debug.Log("_SelectedHero = " + _selectedHero + " / " + "value = " + value);
                onChangeSelectedHero?.Invoke(_selectedHero);
            }
            _selectedHero = value;
        }
    }
    [Header("UI")]
    public HeroInfoUI heroInfoUI;
    public HeroListUI heroListUI;
    public HeroSetUI heroSetUI;
    
    public int maxMacroCount { get { return 5; } }

    private void Awake() { instance = this; }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetNewHero(Hero.EClass.Knight);
        }
    }

    public void SelectHero(Hero hero){
        // switch (GameManager.instance.eMenu)
        // {
        //     case EMenu.Board:
        //     {

        //         break;
        //     }
        // }
        if (null != selectedHero)
        {
            selectedHero.dummy.gameObject.SetActive(false);
        }
        else if (hero.dummy.placedBlock != null)    // 이미 배치된 경우
        {
            hero.dummy.placedBlock.dummy = null;
            hero.dummy.placedBlock = null;
        }
        selectedHero = hero;
        hero.dummy.gameObject.SetActive(true);
        heroInfoUI.RenewUI(selectedHero);
    }

    public void GetNewHero(Hero.EClass eClass){
        Hero hero = null;
        switch(eClass)
        {
            case Hero.EClass.Knight:
            hero = Instantiate(GameManager.instance.prfKngiht);
            break;

            // 직업 추가

        }

        heroList.Add(hero);
        heroListUI.AddHeroUnit(hero);
    }

    public void ToggleSetUI(){
        heroSetUI.MenuToggle();
    }

}
