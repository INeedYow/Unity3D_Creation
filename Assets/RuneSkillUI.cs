using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillUI : MonoBehaviour
{
    public RuneSkillUnit[] skillUnits = new RuneSkillUnit[4];




    public void SetSkillUnit(int number, bool isOn)
    {
        skillUnits[number].gameObject.SetActive(isOn);
    }
}
