using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RuneSkillUnit : MonoBehaviour
{
    public RuneSkill skill;

    public Image skillIcon;
    public Image cooldown;

    public KeyCode keyCode;

    // public void RenewUI(RuneSkillData data)
    // {

    // }

    private void Update() {
        if (Input.GetKeyDown(keyCode))
        {
            skill.Use();
        }
    }
}
