using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMacroUI : MonoBehaviour
{
    public HeroMacroUnit[] conditonMacroUnits ;
    public HeroMacroUnit[] actionMacroUnits ;

    private void Start() {
        //HeroManager.instance.onChangeSelectedHero += RenewUI; // TODO
        InitConditionUnits();
        InitActionUnits();
    }

    void InitConditionUnits()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

        foreach (BattleMacro macro in MacroManager.instance.prfConditionMacros)
        {   // 매니저에 있는 매크로 프리팹의 정보로 dropdown dataList 생성
            optionDatas.Add(new Dropdown.OptionData(macro.data.desc));
        }

        foreach (HeroMacroUnit unit in conditonMacroUnits)
        {   // 생성한 list unit에 전달
            unit.SetText(optionDatas);
        }
    }

    void InitActionUnits()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

        foreach (BattleMacro macro in MacroManager.instance.prfActionMacros)
        {  
            optionDatas.Add(new Dropdown.OptionData(macro.data.desc));
        }

        foreach (HeroMacroUnit unit in actionMacroUnits)
        {  
            unit.SetText(optionDatas);
        }
    }

    public void RenewUI(Hero hero){
        
    }

}
