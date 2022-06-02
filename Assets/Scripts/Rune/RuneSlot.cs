using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneSlot : MonoBehaviour
{
    public RuneTree tree;
    public int requireLv;
    public SkillRuneData skillData1;
    public GameObject selectedLine1;
    public SkillRuneData skillData2;
    public GameObject selectedLine2;

    public RuneSkillCursor skillCursor1;
    public RuneSkillCursor skillCursor2;

    bool m_isOpen = false;
    int m_select = 0;       // 0 : 선택 안 함 , 1 : 위 , 2 : 아래

    private void Start() {
        PlayerManager.instance.onLevelUp += IsLevelEnough;
        if (tree == null) tree = GetComponentInParent<RuneTree>();
    }

    private void OnEnable() {
        if (!m_isOpen)
        {
            IsLevelEnough(PlayerManager.instance.LV);
        }
    }

    void IsLevelEnough(int lv)
    {   //Debug.Log("Lv : " + lv + " / reqLv : " + requireLv);
        if (lv >= requireLv)
        {
            m_isOpen = true;
            PlayerManager.instance.onLevelUp -= IsLevelEnough;
        }
    }

    public void Selected(int selectNumber)
    {   
        if (!m_isOpen) return;      Debug.Log("selected : " + selectNumber);
        
        m_select = selectNumber;
        
        if (m_select == 1)
        {
            selectedLine1.SetActive(true);
            selectedLine2.SetActive(false);
        }
        else
        {
            selectedLine1.SetActive(false);
            selectedLine2.SetActive(true);
        }
    }

    public void Apply()
    {   
        if (!m_isOpen) return;
        
        if (m_select == 1)
        {
            skillData1?.Apply(1);
            PlayerManager.instance.runeSkillUI.SetCursor(skillData1.ID, skillCursor1);
        }
        else if (m_select == 2)
        {
            skillData2?.Apply(1);
            PlayerManager.instance.runeSkillUI.SetCursor(skillData2.ID, skillCursor2);
        }
    }

    public void Release()
    {
        if (!m_isOpen) return;

        if (m_select == 1)
        {
            skillData1?.Release(1);
        }
        else if (m_select == 2)
        {
            skillData2?.Release(1);
        }
    }

}
