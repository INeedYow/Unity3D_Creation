using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUI : MonoBehaviour
{
    [Header("Info")]
    public Image icon;
    public Text nameText;

    [Header("Stat")]
    public Text lv;
    public Text hp;
    public Text damage;
    public Text magic;
    public Text armor;
    public Text magicArmor;
    public Text attackSpeed;
    public Text range;
    public Text moveSpeed;
    public Text critical;
    public Text criticalDamage;
    public Text dodge;

    [Header("Skill")]
    public Image[] skill = new Image[4];

    [Header("Empty")]
    public Sprite emptyIcon;
    public Sprite emptySkill;

    private void Start() {  
        HeroManager.instance.onChangeSelectedHero += RenewUI;
    }

    private void OnEnable() {
        RenewUI(HeroManager.instance.selectedHero);
    }

    public void RenewUI(Hero hero){
        if (hero == null){
            icon.sprite         = emptyIcon;
            nameText.text       = " ";

            lv.text             = null;
            hp.text             = null;
            damage.text         = null;
            magic.text          = null;
            armor.text          = null;
            magicArmor.text     = null;
            attackSpeed.text    = null;
            range.text          = null;
            moveSpeed.text      = null;
            critical.text       = null;
            criticalDamage.text = null;
            dodge.text          = null;

            for(int i = 0; i < 4; i++)
            {
                skill[i].sprite = emptySkill;
            }

        }
        else{
            icon.sprite         = hero.icon;
            nameText.text       = hero.name;

            lv.text             = hero.level.ToString();
            hp.text             = hero.maxHp.ToString();
            damage.text         = string.Format("{0} ~ {1}", hero.minDamage, hero.maxDamage);
            magic.text          = hero.magicDamage.ToString();
            armor.text          = Mathf.RoundToInt(hero.armorRate * 100f).ToString();
            magicArmor.text     = Mathf.RoundToInt(hero.magicArmorRate * 100f).ToString();
            attackSpeed.text    = hero.attackDelay.ToString();
            range.text          = (hero.attackRange - 1).ToString();                    // 본인 크기 1 뺌
            moveSpeed.text      = hero.moveSpeed.ToString();
            critical.text       = hero.criticalChance.ToString();
            criticalDamage.text = Mathf.RoundToInt(hero.criticalRate * 100).ToString();
            dodge.text          = hero.dodgeChance.ToString();

            for(int i = 0; i < 4; i++)
            {
                if (hero.skills[i] == null) continue;
                if (hero.skills[i].data == null) continue;
                if (hero.skills[i].data.icon == null) continue;
                skill[i].sprite = hero.skills[i].data.icon;
            }
            
        }
    }
}
