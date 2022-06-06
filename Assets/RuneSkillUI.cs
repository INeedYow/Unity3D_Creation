using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneSkillUI : MonoBehaviour
{
    public RuneSkillUnit[] skillUnits = new RuneSkillUnit[4];

    private void OnEnable() {
        for (int i = 0; i < skillUnits.Length; i++)
        {
            skillUnits[i].gameObject.SetActive(false);
        }
    }

    public void SetSkillUnit(int IdNumber, bool isOn)
    {
        if (skillUnits[IdNumber - 1].gameObject.activeSelf == isOn) return;

        skillUnits[IdNumber - 1].gameObject.SetActive(isOn);
    }

    public void SetInfo(int IdNumber, SkillRuneData skillRuneData)
    {   //Debug.Log(IdNumber - 1);
        SetSkillUnit(IdNumber, true);
        skillUnits[IdNumber - 1].SetInfo(skillRuneData);
    }

    public void SetCursor(int IdNumber, RuneSkillCursor skillCursor)
    {
        SetSkillUnit(IdNumber, true);
        skillUnits[IdNumber - 1].SetCursor(skillCursor);
    }

    public void ResetCooldown()
    {
        foreach (RuneSkillUnit unit in skillUnits)
        {
            unit.curCooldown = 0;
            unit.RenewCooldown();
        }
    }
}
 