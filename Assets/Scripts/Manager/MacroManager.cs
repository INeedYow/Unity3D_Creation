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

    private void Awake() { instance = this; }

    public void SetMacro(int value){
        Debug.Log("value = " + value);
    }

}