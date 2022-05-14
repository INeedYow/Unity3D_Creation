using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MacroManager : MonoBehaviour
{
    public static MacroManager instance { get; private set; }

    public Dropdown[] conditions;
    public Dropdown[] actions;

    private void Awake() { 
        instance = this; 
        Init();
    }

    void Init(){
        //conditions = new Dropdown[HeroManager.instance.maxMacroCount];
        //actions = new Dropdown[HeroManager.instance.maxMacroCount];

        Dropdown.OptionData option = new Dropdown.OptionData();
        for (int i = 0; i < 5; i++)
        {
            option.text = "dropdown test";
            conditions[0].options.Add(option);
        }
    }
}
