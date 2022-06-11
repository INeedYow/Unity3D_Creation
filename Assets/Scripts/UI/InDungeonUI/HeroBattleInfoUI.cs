using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleInfoUI : MonoBehaviour
{
    public List<HeroBattleInfoUnit> battleHeroInfoUnits;

    [Header("Macro UI")]
    public GameObject macroInfoUI;
    public MacroInfoUnit[] macroInfoUnits = new MacroInfoUnit[5]; 

    [Header("Text Color")]
    public Color normalUpColor;
    public Color normalDownColor;
    public Color currentUpColor;
    public Color currentDownColor;


    Character m_owner;
    int m_index;
    // public RectTransform rtf;
    // public float defaultX;


    // private void Awake() {
    //     Init();
    // }

    // void Init(){
    //     defaultX = rtf.position.x;
    // }

    private void OnEnable() 
    {
        for (int i = 0; i < PartyManager.instance.heroParty.Count; i++){
            battleHeroInfoUnits[i].gameObject.SetActive(true);
            battleHeroInfoUnits[i].SetOwner(PartyManager.instance.heroParty[i]);
        }

        m_owner = null;
        ResetColor();
    }

    //// Macro UI

    public void ShowMacroInfoUI(Character owner, int macroIndex)
    {
        macroInfoUI.SetActive(true);

        if (m_owner != owner)
        {
            m_owner = owner;
            SetMacroText();
        }
        
        m_owner.onMacroChangeGetIndex += RenewCurrentMacro;
        m_owner.onDead += HideMacroInfoUI;

        m_index = macroIndex;
        
        RenewCurrentMacro(m_index);
    }

    void SetMacroText()
    {
        for(int i = 0; i < macroInfoUnits.Length; i++)
        {
            macroInfoUnits[i].up.text = m_owner.conditionMacros[i].data.desc;
            macroInfoUnits[i].down.text = m_owner.actionMacros[i].data.desc;
        }
    }

    void ResetColor()
    {
        for(int i = 0; i < macroInfoUnits.Length; i++)
        {
            macroInfoUnits[i].up.color = normalUpColor;
            macroInfoUnits[i].down.color = normalDownColor;
        }
    }

    void RenewCurrentMacro(int index)
    {
        macroInfoUnits[m_index].up.color = normalUpColor;
        macroInfoUnits[m_index].down.color = normalDownColor;

        m_index = index;

        macroInfoUnits[m_index].up.color = currentUpColor;
        macroInfoUnits[m_index].down.color = currentDownColor;
    }

    public void HideMacroInfoUI()
    {
        if (m_owner != null)
        {
            m_owner.onMacroChangeGetIndex -= RenewCurrentMacro;
            m_owner.onDead -= HideMacroInfoUI;
        }

        ResetColor();
        macroInfoUI.SetActive(false);
    }
}
