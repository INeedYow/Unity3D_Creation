using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroManager : MonoBehaviour
{  
    public static HeroManager instance { get; private set; }

    public List<Hero> heroList = new List<Hero>();
    public Hero selectedHero = null;
    [Header("UI")]
    public HeroInfoUI heroInfoUI;
    public HeroListUI heroListUI;
    public HeroSetUI heroSetUI;
    
    private void Awake() { instance = this; }

    

    public void SelectHero(Hero hero){
        if (null != selectedHero)
        {
            selectedHero.dummy.gameObject.SetActive(false);
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

            //

        }

        heroList.Add(hero);
        heroListUI.AddHeroUnit(hero);
    }
}
