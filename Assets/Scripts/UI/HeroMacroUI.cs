using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMacroUI : MonoBehaviour
{
    public HeroMacroUnit[] conditonMacroUnits = new HeroMacroUnit[5];
    public HeroMacroUnit[] actionMacroUnits = new HeroMacroUnit[5];

    private void Start() {
        InitConditionUnits();
        InitActionUnits();
    }

    private void OnEnable() {
        HeroManager.instance.onChangeSelectedHero += RenewUI;
        RenewUI(HeroManager.instance.selectedHero);
    }

    private void OnDisable() {
        HeroManager.instance.onChangeSelectedHero -= RenewUI;
    }

    void InitConditionUnits()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

        foreach (BattleMacro macro in MacroManager.instance.prfConditionMacros)
        {   // 매니저에 있는 매크로 프리팹의 정보로 dropdown dataList 생성
            optionDatas.Add(new Dropdown.OptionData(macro.data.desc));
        }

        int id = 0;
        foreach (HeroMacroUnit unit in conditonMacroUnits)
        {   // id : 자신이 몇 번째 매크로인지 알아야 해서 id 부여했음
            unit.SetOptions(optionDatas);
            unit.ID = id++;
            unit.isConditionMacro = true;
        }
    }

    void InitActionUnits()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

        foreach (BattleMacro macro in MacroManager.instance.prfActionMacros)
        {  
            optionDatas.Add(new Dropdown.OptionData(macro.data.desc));
        }

        int id = 0;
        foreach (HeroMacroUnit unit in actionMacroUnits)
        {  
            unit.SetOptions(optionDatas);
            unit.ID = id++;
            unit.isConditionMacro = false;
        }
    }

    public void RenewUI(Hero hero){ Debug.Log("RenewUI");
        if (null == hero){ 
            foreach (HeroMacroUnit unit in conditonMacroUnits)  { unit.dropdown.interactable = false; }
            foreach (HeroMacroUnit unit in actionMacroUnits)    { unit.dropdown.interactable = false; }
        }
        else{
            for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
            { 
                if (null != hero.conditionMacros[i])
                    conditonMacroUnits[i].SetValue(hero.conditionMacros[i].data.ID);
                
                if (null != hero.actionMacros[i])
                    actionMacroUnits[i].SetValue(hero.actionMacros[i].data.ID);
            }
        }
    }

}
