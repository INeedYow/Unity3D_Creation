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
    [Tooltip("습득 레벨")] public int requireLevel;
    public float cooldown;
    [Range(0, 20)]
    public float skillRange;

    [Header("Spec")]
    public float damage;
    public float area;
    public int count;
    public float ratio;
    public float duration;
    public bool isMagic;
}