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

    public Text abilityName;
    public Text abilityDesc;

    [Header("Skill")]
    public Image[] skill = new Image[4];
    public Text skillName;
    public Text skillDesc;

    [Header("Empty")]
    public Sprite emptyIcon;
    public Sprite emptySkill;

    private void Start() {  
        HeroManager.instance.onChangeSelectedHero += RenewUI;
        //HeroManager.instance.onChangeSelectedHero += RenewAbilityDesc;
    }

    private void OnEnable() {
        RenewUI(HeroManager.instance.selectedHero);
    }

    private void OnDisable() {
        EmptySkillDesc();
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

    public void RenewSkillDesc(int number)
    {   
        if (HeroManager.instance.selectedHero == null) return;  

        if (HeroManager.instance.selectedHero.skills.Length <= number) return;  // 아직 스킬 다 없기 때문에 임시로

        skillName.text = string.Format("[ {0} ]", HeroManager.instance.selectedHero.skills[number].data.skillName);
        skillDesc.text = HeroManager.instance.selectedHero.skills[number].data.description;
    }

    public void EmptySkillDesc()
    {   Debug.Log("djfdfjdkdjk");
        skillName.text = " [ 스킬 이름 ] ";
        skillDesc.text = " ";
    }

    public void RenewAbilityDesc(Hero hero)     // TODO 선택 영웅 변경시 호출해주기
    {
        if (hero == null)
        {
            abilityName.text = " ";
            abilityDesc.text = " ";
        }
        else if (hero.accessoryData == null)
        {
            abilityName.text = "[ 특수 능력 ]";
            abilityDesc.text = " 없음 ";
        }
        else{
            abilityName.text = hero.accessoryData.itemName;
            abilityDesc.text = hero.accessoryData.desc;
        }
        
    }
}
