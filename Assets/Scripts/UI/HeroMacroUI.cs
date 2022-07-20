using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMacroUI : MonoBehaviour
{
    public HeroMacroUnit[] conditonMacroUnits = new HeroMacroUnit[5];
    public HeroMacroUnit[] actionMacroUnits = new HeroMacroUnit[5];

    List<Dropdown.OptionData> m_actionOptionDatas;
    Dropdown.OptionData[] m_skillOptionDatas;

    private void Awake() {
        InitConditionUnits();
        InitActionUnits();
    }

    private void OnEnable() {
        HeroManager.instance.onChangeSelectedHero += RenewUI;
        //RenewUI(HeroManager.instance.selectedHero); 
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
        //List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        m_actionOptionDatas  = new List<Dropdown.OptionData>();

        m_skillOptionDatas = new Dropdown.OptionData[4];

        foreach (BattleMacro macro in MacroManager.instance.prfActionMacros)
        {  
            //optionDatas.Add(new Dropdown.OptionData(macro.data.desc));
            m_actionOptionDatas.Add(new Dropdown.OptionData(macro.data.desc));
        }

        for (int i = 0; i < MacroManager.instance.prfSkillMacros.Count; i++)
        {
            m_skillOptionDatas[i] = new Dropdown.OptionData(MacroManager.instance.prfSkillMacros[i].data.desc);
        }

        int id = 0;
        foreach (HeroMacroUnit unit in actionMacroUnits)
        {  
            //unit.SetOptions(optionDatas);
            unit.SetOptions(m_actionOptionDatas);
            unit.ID = id++;
            unit.isConditionMacro = false;
        }

    }

    public void RenewUI(Hero hero){ //Debug.Log("RenewUI");
        if (null == hero){ 
            foreach (HeroMacroUnit unit in conditonMacroUnits)  { unit.dropdown.interactable = false; }
            foreach (HeroMacroUnit unit in actionMacroUnits)    { unit.dropdown.interactable = false; }
        }
        else{
            SetSkillMacro(hero);

            for (int i = 0; i < MacroManager.instance.maxMacroCount; i++)
            { 
                if (null != hero.conditionMacros[i])
                {
                    conditonMacroUnits[i].SetValue(hero.conditionMacros[i].data.ID);
                }
                
                if (null != hero.actionMacros[i])
                {
                    actionMacroUnits[i].SetValue(hero.actionMacros[i].data.ID);
                }
            }
        }
    }



    // 스킬 매크로 추가 관리 (해당 영웅 레벨과 스킬 획득 레벨 비교해서 매크로 dropdown 옵션에 추가, 삭제 관리)
    void SetSkillMacro(Hero hero)
    {
        for (int i = 0; i < hero.skills.Length; i++)
        {
            if (hero.level >= hero.skills[i].data.requireLevel)
            {   // 영웅 레벨이 스킬 레벨제한 이상일 때
                if (!m_actionOptionDatas.Contains(m_skillOptionDatas[i]))
                {   // 해당 스킬 사용 매크로가 없는 경우 -> 추가
                    m_actionOptionDatas.Add(m_skillOptionDatas[i]);
                }
            }
            else
            {   // 레벨이 부족한 경우
                if (m_actionOptionDatas.Contains(m_skillOptionDatas[i]))
                {   // 스킬 매크로가 있는 경우 -> 제거
                    m_actionOptionDatas.Remove(m_skillOptionDatas[i]);
                }
            }
        }

        foreach (HeroMacroUnit unit in actionMacroUnits)
        {
            unit.ClearOption();
            unit.SetOptions(m_actionOptionDatas);
        }
    }

}
