using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroListUnit : MonoBehaviour
{
    public Hero hero;
    public Image icon;
    public Text nameText;
    public Button button;

    public void SetHero(Hero hero){
        this.hero = hero;
        icon.sprite = hero.icon;
        nameText.text = hero.name;
    }
}
