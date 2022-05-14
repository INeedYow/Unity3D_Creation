using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUI : MonoBehaviour
{
    Hero m_curHero;

    public Image icon;
    public Text nameText;

    // TODO spec part

    public void RenewUI(Hero hero){
        //icon.sprite = m_curHero.icon; // 왜 에러
        nameText.text = hero.name;
    }
}
