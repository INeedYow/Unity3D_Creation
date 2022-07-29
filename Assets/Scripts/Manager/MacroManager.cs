using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MacroManager : MonoBehaviour
{
    public static MacroManager instance { get; private set; }
    public HeroMacroUI macroUI;
    public Button macroBtn;

    [Header("Hero Macro")]
    public List<ConditionMacro> prfConditionMacros;
    public List<ActionMacro>    prfActionMacros;
    public List<ActionMacro>    prfSkillMacros;
    public int maxMacroCount { get { return 5; } }

    private void Awake() { 
        instance = this; 
        SetMacroDataID();
    }
    void SetMacroDataID(){ // 각 macro data가 dropdown의 value랑 같게 쓰려고 
        int id = 0;
        foreach (BattleMacro macro in prfConditionMacros)
        {
            macro.data.ID = id++;
        }

        id = 0;
        foreach (BattleMacro macro in prfActionMacros)
        {
            macro.data.ID = id++;
        }
        foreach (BattleMacro macro in prfSkillMacros)
        {
            macro.data.ID = id++;
        }
    }

    public void SetMacro(bool isCondition, int macroUnitID, int value){
        Hero hero = HeroManager.instance.selectedHero;
        if (null == hero) return;
        if (isCondition)
        {   //Debug.Log("SetMacro_Con");
            Destroy(hero.conditionMacros[macroUnitID].gameObject);
            hero.conditionMacros[macroUnitID] = Instantiate(prfConditionMacros[value], hero.transform);
            hero.conditionMacros[macroUnitID].owner = hero;
        }
        else{   //Debug.Log("SetMacro_Act");
            Destroy(hero.actionMacros[macroUnitID].gameObject);

            if (value < prfActionMacros.Count)
            {   // 공통 매크로인 경우
                hero.actionMacros[macroUnitID] = Instantiate(prfActionMacros[value], hero.transform);
            }
            else
            {   // 스킬 매크로인 경우
                hero.actionMacros[macroUnitID] = Instantiate(prfSkillMacros[value - prfActionMacros.Count], hero.transform);
            }
            
            hero.actionMacros[macroUnitID].owner = hero;
        }
    }

    public void ShowMacroUI(){
        macroUI.gameObject.SetActive(true);
        macroBtn.interactable = false;
        InventoryManager.instance.HideInvenUI();

        macroUI.RenewUI(HeroManager.instance.selectedHero);
    }

    public void HideMacroUI(){
        macroUI.gameObject.SetActive(false);
        macroBtn.interactable = true;
    }
}