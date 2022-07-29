using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HeroItemData", menuName = "Data/HeroItem")]
public class HeroItemData : ItemData
{
    public Hero.EClass eClass;
    public SkillData[] skillDatas = new SkillData[4];

    public override bool Buy()
    {
        if (HeroManager.instance.GetHeroCost() > PlayerManager.instance.gold) return false;
        PlayerManager.instance.AddGold(-HeroManager.instance.GetHeroCost());  // 골드부터 빼야지 인원 수 가격 제대로 책정됨
        HeroManager.instance.GetNewHero(eClass);
        ItemManager.instance.itemInfoUI.RenewUI(this);
        return true;
    }
    
    public override bool Sell()
    {
        // TODO
        
        return true;
    }
    public override int GetCost()
    {
        return HeroManager.instance.GetHeroCost();
    }

    public override bool SetOptionText(int optionNumber, ItemOptionUnit optionUnit)
    {
        if (skillDatas[optionNumber] == null)
        {   // 나중에 스킬 다 넣어주면 없어질 코드
            optionUnit.option.text = "스킬";
            optionUnit.value.text = "정보 없음";
            return true;
        }
        else{
            optionUnit.option.text = string.Format("{0}(Lv{1})", skillDatas[optionNumber].skillName, skillDatas[optionNumber].requireLevel );
            optionUnit.value.text = string.Format("{0}", skillDatas[optionNumber].description );
            return true;
        }
    }

}
