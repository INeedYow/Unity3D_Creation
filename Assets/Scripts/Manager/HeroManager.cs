using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroManager : MonoBehaviour
{  
    public UnityAction<Hero> onChangeSelectedHero;
    public static HeroManager instance { get; private set; }

    public List<Hero> heroList = new List<Hero>();

    public Hero selectedHero;
    int heroCost = 1000;
    int costMultiple = 4;

    [Header("UI")]
    public HeroInfoUI heroInfoUI;
    public HeroListUI heroListUI;
    public HeroSetUI heroSetUI;     // macro, inven 같이 있는 UI

    [Header("Hero Prf")]
    public Hero prfKngiht;
    public Hero prfArcher;
    public Hero prfAngel;

    [Header("Hero prfSkill")]
    public Skill[] prfKnightSkills;
    public Skill[] prfArcherSkills;
    public Skill[] prfAngelSkills;

    private void Awake() { instance = this; }

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
    }

    public void GetNewHero(Hero.EClass eClass){
        Hero hero = null;
        switch(eClass)
        {
            case Hero.EClass.Knight:
            {
                hero = Instantiate(prfKngiht);
                // 매크로 생성
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {   
                    hero.conditionMacros[i] = Instantiate(MacroManager.instance.prfConditionMacros[0], hero.transform);
                    hero.conditionMacros[i].owner = hero;
                    //hero.conditionMacros[i].transform.SetParent(hero.transform);
                }
                for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
                {
                    hero.actionMacros[i] = Instantiate(MacroManager.instance.prfActionMacros[0], hero.transform);
                    hero.actionMacros[i].owner = hero;
                    //hero.actionMacros[i].transform.SetParent(hero.transform);
                }
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

                for (int i = 0; i < prfAngelSkills.Length; i++)
                {
                    hero.skills[i] = Instantiate(prfAngelSkills[i], hero.transform);
                    hero.skills[i].Init(hero, i + 1);
                }
                break;
            }

            // 직업 추가

        }

        AddHero(hero);
    }

    public void AddHero(Hero hero){
        heroList.Add(hero);
        heroListUI.AddHeroUnit(hero);
        GameManager.instance.cubePlanet.AddFloatingBlock(hero);
    }

    public void RemoveHero(Hero hero){
        heroList.Remove(hero);
        heroListUI.RemoveHeroUnit(hero);
        GameManager.instance.cubePlanet.RemoveFloatingBlock(hero);
    }

    // 매크로, 인벤토리 토글 // 버튼 이벤트 함수
    public void ToggleSetUI(){ 
        heroSetUI.MenuToggle();
    }

    public int GetHeroCost(){
        if (heroList.Count < 3) { return 0; }
        return heroCost * (int)Mathf.Pow(costMultiple, heroList.Count - 3);
    }

    public void ShowHeroUI(bool isOn){
        heroListUI.gameObject.SetActive(isOn);
        heroInfoUI.gameObject.SetActive(isOn);
        heroSetUI.gameObject.SetActive(isOn);
    }
}
