using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroListUnitButton : MonoBehaviour
{
    public HeroListUnit unit;

    public void SelectButton(){
        HeroManager.instance.SelectHero(unit.hero);
    }
}