using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "SkillData", menuName = "Data/Skill")]
public abstract class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public Image icon;
    public int requireLevel;
}