using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleInfoUnit : MonoBehaviour
{
    public Image icon;
    public Image[] macroCheckBoxes = new Image[5];
    public Image HpBar;
    Character m_owner;
    int m_prevIndex = 0;

    public void SetOwner(Character owner)
    {
        m_owner = owner;
        icon.sprite = m_owner.icon;
        m_owner.onHpChange += RenewHpBar;
        m_owner.onMacroChange += RenewMacroBox;

        RenewHpBar();
    }

    private void OnEnable() {
        ResetMacroBox();
    }

    private void OnDisable() {
        if (m_owner != null)
        {
            m_owner.onHpChange -= RenewHpBar;
            m_owner.onMacroChange -= RenewMacroBox;
        }
    }

    void RenewHpBar(){
        HpBar.fillAmount = m_owner.curHp / m_owner.maxHp;
    }

    void RenewMacroBox(int index){
        if (m_prevIndex != index) 
        {
            macroCheckBoxes[m_prevIndex].color = Color.white;
            m_prevIndex = index;
            macroCheckBoxes[m_prevIndex].color = new Color(0f, 1f, 1f);
        }
    }

    void ResetMacroBox(){
        macroCheckBoxes[0].color = new Color(0f, 1f, 1f);
        for (int i = 1 ; i < macroCheckBoxes.Length; i++)
        {
            macroCheckBoxes[i].color = Color.white;
        }
        m_prevIndex = 0;
    }
}
