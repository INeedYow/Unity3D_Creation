using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DummyInfoUnit : MonoBehaviour
{
    public Hero hero;
    public Text LV;
    public Image weapon;
    public Image armor;
    public Image accessory;
    public Sprite empty;

    public void SetOwner(Hero hero)
    {
        this.hero = hero;
        transform.position = hero.dummy.transform.position;

        Renew();
    }

    public void Renew()
    {
        LV.text = hero.level.ToString();

        if (hero.weaponData != null)        weapon.sprite = hero.weaponData.icon;
        else                                weapon.sprite = empty;

        if (hero.armorData != null)         armor.sprite = hero.armorData.icon;
        else                                armor.sprite = empty;

        if (hero.accessoryData != null)     accessory.sprite = hero.accessoryData.icon;
        else                                accessory.sprite = empty;
    }
}
