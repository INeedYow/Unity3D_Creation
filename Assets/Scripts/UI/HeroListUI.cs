using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroListUI : MonoBehaviour
{
    public GameObject iconTrayUI;
    public HeroListUnit prfUnit;

    public void AddHeroUnit(Hero hero){
        HeroListUnit unit = Instantiate(prfUnit);
        unit.SetHero(hero);
        unit.transform.SetParent(iconTrayUI.transform);
    }
}
