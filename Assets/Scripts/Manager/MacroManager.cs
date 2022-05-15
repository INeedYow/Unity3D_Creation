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
    public int maxMacroCount = 5;

    private void Awake() { 
        instance = this; 
        SetMacroDataID();
    }
    void SetMacroDataID(){ 
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
    }

    public void SetMacro(bool isCondition, int macroUnitID, int value){
        if (HeroManager.instance.selectedHero == null)
            Debug.Log("null");
            
        if (isCondition)
        {
            HeroManager.instance.selectedHero.conditionMacros[macroUnitID] 
                = Instantiate(prfConditionMacros[value]);
        }
        else{
            HeroManager.instance.selectedHero.actionMacros[macroUnitID] 
                = Instantiate(prfActionMacros[value]);
        }
    }

}