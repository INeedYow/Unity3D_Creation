using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Result_HeroUnit : MonoBehaviour
{
    Hero m_hero;

    public Image icon;

    public Text Lv;
    public Text curExp;
    public Text maxExp;

    public void SetOwner(Character owner)
    {
        m_hero = owner as Hero;
        icon.sprite = m_hero.icon;
        
        Lv.text = m_hero.level.ToString();
        curExp.text = m_hero.curExp.ToString();
        maxExp.text = m_hero.maxExp.ToString();
    }
}
