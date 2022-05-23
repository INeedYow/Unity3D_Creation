using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;

    private void Start() {
        HeroManager.instance.onChangeSelectedHero += RenewUI;
    }

    // TODO spec part
    public void RenewUI(Hero hero){
        if (hero == null){
            icon.sprite = null;
            nameText.text = null;
        }
        else{
            icon.sprite = hero.icon;
            nameText.text = hero.name;
        }
    }
}
