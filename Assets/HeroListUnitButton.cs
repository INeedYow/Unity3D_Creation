using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroListUnitButton : MonoBehaviour
{
    public HeroListUnit unit;

    public void SelectButton(){
        GameManager.instance.SelectHero(unit.hero);
    }
}
