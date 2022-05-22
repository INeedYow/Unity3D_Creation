using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroListUI : MonoBehaviour
{
    public GameObject iconTrayUI;
    public HeroListUnit prfUnit;
    public List<HeroListUnit> heroListUnits;

    public void AddHeroUnit(Hero hero){
        HeroListUnit unit = Instantiate(prfUnit);
        unit.SetHero(hero);
        unit.transform.SetParent(iconTrayUI.transform);
        heroListUnits.Add(unit);
    }

    public void RemoveHeroUnit(Hero hero){
        foreach(HeroListUnit unit in heroListUnits){
            if (unit.hero == hero){
                Destroy(unit.gameObject);
            }
        }
    }
}
