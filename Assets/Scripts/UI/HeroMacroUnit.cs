using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMacroUnit : MonoBehaviour
{
    public Dropdown dropdown; 
    [Header("ID")]
    public int ID;      // 몇 번째 MacroUnit인지
    public bool isConditionMacro;

    public void SetOptions(List<Dropdown.OptionData> listOptionData){
        dropdown.AddOptions(listOptionData);
    }

    public void ChangeMacro(){ Debug.Log("ID[" + ID + "] Macro Change " + " / " + "isConditionMacro? : " + isConditionMacro);
        MacroManager.instance.SetMacro(isConditionMacro, ID, dropdown.value);
    }

    public void SetValue(int value){
        if (dropdown.interactable == false) 
        {
            dropdown.interactable = true;
        }
        dropdown.value = value;
    }
}
