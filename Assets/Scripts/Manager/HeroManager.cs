using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroManager : MonoBehaviour
{  
    public UnityAction<Hero> onChangeSelectedHero;
    public static HeroManager instance { get; private set; }

    public List<Hero> heroList = new List<Hero>();
    public int maxCount { get { return 12; } }

    public Hero selectedHero;
    int heroCost = 1500;
    int costMultiple = 2;

    [Header("UI")]
    public HeroInfoUI heroInfoUI;
    public HeroListUI heroListUI;
    public GameObject heroSetUI;     // macro, inven 같이 있는 UI(active켜주기만 하면 됨)
    public DummyInfoUnit dummyInfoUnit;

    [Header("Hero Prf")]
    public Hero prfKngiht;
    public Hero prfArcher;
    public Hero prfAngel;
    public Hero prfNecromancer;
    public Hero prfBard;
    public Hero prfTemplar;

    [Header("Hero prfSkill")]
    public Skill[] prfKnightSkills;
    public Skill[] prfArcherSkills;
    public Skill[] prfAngelSkills;
    public Skill[] prfNecromancerSkills;
    public Skill[] prfBardSkills;
    public Skill[] prfTemplarSkills;



    private void Awake() { 
        instance = this; 
    }

    // private void Start() {
    //     heroSetUI.gameObject.SetActive(true);
    //     InventoryManager.instance.invenUI.Init();
    //     heroSetUI.gameObject.SetActive(false);
    // }

    public void PickUpDummy(Hero hero){
        selectedHero = hero;
        GameManager.instance.OnDummyDrag();
    }

    public void PutDownDummy(){
        selectedHero = null;
        GameManager.instance.OnDummyDrop();
    }

    public void SelectHero(Hero hero){
        selectedHero = hero;
        onChangeSelectedHero?.Invoke(selectedHero);
        heroInfoUI.RenewUI(hero);
    }

    public void ShowDummyInfo(Dummy dummy)
    {
        dummyInfoUnit.gameObject.SetActive(true);
        dummyInfoUnit.transform.position = dummy.transform.position + Vector3.right * 3f;
        dummyInfoUnit.SetOwner(dummy.owner);
    }

    public void HideDummyInfo() { dummyInfoUnit.gameObject.SetActive(false); }

    public void GetNewHero(Hero.EClass eClass){
        Hero hero = null;
        switch(eClass)
        {
            case Hero.EClass.Knight:
            {
                hero = Instantiate(prfKngiht);
                // 스킬 생성
                for (int i = 0; i < prfKnightSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfKnightSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }

                break;
            }

            case Hero.EClass.Archer:
            {
                hero = Instantiate(prfArcher);

                for (int i = 0; i < prfArcherSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfArcherSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            case Hero.EClass.Angel:
            {
                hero = Instantiate(prfAngel);

                for (int i = 0; i < prfAngelSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfAngelSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            case Hero.EClass.Necromancer:
            {
                hero = Instantiate(prfNecromancer);

                for (int i = 0; i < prfNecromancerSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfNecromancerSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            case Hero.EClass.Bard:
            {
                hero = Instantiate(prfBard);

                for (int i = 0; i < prfBardSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfBardSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            case Hero.EClass.Templar:
            {
                hero = Instantiate(prfTemplar);

                for (int i = 0; i < prfTemplarSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfTemplarSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            // 직업 추가

        }

        // Macro 생성
        for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
        {
            hero.conditionMacros[i] = Instantiate(MacroManager.instance.prfConditionMacros[0], hero.transform);
            hero.conditionMacros[i].owner = hero;
        }

        for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
        {
            hero.actionMacros[i] = Instantiate(MacroManager.instance.prfActionMacros[0], hero.transform);
            hero.actionMacros[i].owner = hero;
        }
        
        AddHero(hero);
    }

    public void AddHero(Hero hero){
        heroList.Add(hero);
        heroListUI.AddHeroUnit(hero);
        PlayerManager.instance.playerInfoUI.RenewCurHero(heroList.Count);
        GameManager.instance.cubePlanet.AddFloatingBlock(hero);
    }

    public void RemoveHero(Hero hero){
        heroList.Remove(hero);
        heroListUI.RemoveHeroUnit(hero);
        PlayerManager.instance.playerInfoUI.RenewCurHero(heroList.Count);
        GameManager.instance.cubePlanet.RemoveFloatingBlock(hero);
    }

    public int GetHeroCost(){
        if (heroList.Count < 3) { return 0; }
        return heroCost * (int)Mathf.Pow(costMultiple, heroList.Count - 3);
    }

    public void ShowHeroUI(bool isOn)
    {
        heroListUI.gameObject.SetActive(isOn);
        heroInfoUI.gameObject.SetActive(isOn);
        //heroSetUI.gameObject.SetActive(isOn);
        if (isOn)
        {
            InventoryManager.instance.ShowHeroSetUI();
        }
        else{
            InventoryManager.instance.HideHeroSetUI();
        }
    }

    public bool IsFull()
    { return heroList.Count >= maxCount; }
}
