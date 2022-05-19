using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "SkillData", menuName = "Data/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public Image icon;
    [Space(10f)]
    public int requireLevel;
    public float cooldown;
    [Tooltip("몇 번째 스킬인지")][Range(1, 4)]
    public int ID;              // 자신이 몇 번째 스킬인지
}