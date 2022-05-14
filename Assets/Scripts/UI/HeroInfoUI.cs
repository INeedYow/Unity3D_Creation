using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;

    // TODO spec part
    public void RenewUI(Hero hero){
        icon.sprite = hero.icon;
        nameText.text = hero.name;
    }
}
