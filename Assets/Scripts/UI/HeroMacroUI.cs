using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMacroUI : MonoBehaviour
{
    public HeroMacroUnit[] conditonMacroUnits ;
    public HeroMacroUnit[] actionMacroUnits ;

    private void Start() {
        HeroManager.instance.onChangeSelectedHero += RenewUI;
        InitConditionUnits();
        InitActionUnits();
    }

    private void OnEnable() {
        RenewUI(HeroManager.instance.selectedHero);
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
        {   // 생성한 list unit에 전달
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

    public void RenewUI(Hero hero){
        if (null == hero){  
            foreach (HeroMacroUnit unit in conditonMacroUnits)  { unit.SetValue(0); }
            foreach (HeroMacroUnit unit in actionMacroUnits)    { unit.SetValue(0); }
        }
        else{
            for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
            { 
                conditonMacroUnits[i].SetValue(hero.conditionMacros[i].data.ID);
            }
            for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
            { 
                actionMacroUnits[i].SetValue(hero.actionMacros[i].data.ID);
            }
        }
    }

}
