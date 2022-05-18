using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MacroManager : MonoBehaviour
{
    public static MacroManager instance { get; private set; }
    public HeroMacroUI macroUI;

    public List<ConditionMacro> prfConditionMacros;
    public List<ActionMacro> prfActionMacros;
    public int maxMacroCount { get { return 5; } }

    private void Awake() { 
        instance = this; 
        SetMacroDataID();
    }
    void SetMacroDataID(){ // 각 macro data가 dropdown의 value랑 같게 쓰려고 
        int id = 0;
        foreach (BattleMacro macro in prfConditionMacros){
            macro.data.ID = id++;
        }

        id = 0;
        foreach (BattleMacro macro in prfActionMacros){
            macro.data.ID = id++;
        }
    }

    public void SetMacro(bool isCondition, int macroUnitID, int value){
        Hero hero = HeroManager.instance.selectedHero;
        if (null == hero) return;
        if (isCondition)
        {   Debug.Log("SetMacro_Con");
            Destroy(hero.conditionMacros[macroUnitID].gameObject);
            hero.conditionMacros[macroUnitID] = Instantiate(prfConditionMacros[value]);
            hero.conditionMacros[macroUnitID].owner = hero;
        }
        else{   Debug.Log("SetMacro_Act");
            Destroy(hero.actionMacros[macroUnitID].gameObject);
            hero.actionMacros[macroUnitID] = Instantiate(prfActionMacros[value]);
            hero.actionMacros[macroUnitID].owner = hero;
        }
    }

    private void Update() { // TODO
        if (Input.GetKeyDown(KeyCode.I))
        {
            HeroManager.instance.heroSetUI.gameObject.SetActive(
                !HeroManager.instance.heroSetUI.gameObject.activeSelf);
        }
    }
}