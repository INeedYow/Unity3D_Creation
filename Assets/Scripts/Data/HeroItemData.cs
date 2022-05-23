using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroItemData", menuName = "Data/HeroItem")]
public class HeroItemData : ItemData
{
    public Hero.EClass eClass;
    public SkillData skilldata_1;
    public SkillData skilldata_2;
    public SkillData skilldata_3;
    public SkillData skilldata_4;

    public override bool Buy()
    {
        if (HeroManager.instance.GetHeroCost() > GameManager.instance.gold) return false;
        GameManager.instance.AddGold(-HeroManager.instance.GetHeroCost());  // 골드부터 빼야지 인원 수 가격 제대로 책정됨
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
}
