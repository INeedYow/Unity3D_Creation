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
    [Tooltip("습득 레벨")]  public int requireLevel;
    [Range(1f, 30f)]       public float cooldown;
    [Range(0f, 20f)]       public float skillRange;

    [Header("Spec")]
    public bool isMagic;
    [Range(0.1f, 4f)]      public float powerRatio;    // 계수
    [Range(0f, 30f)]       public float area;
    [Tooltip("개수")]      public int count;
    [Range(0f, 20f)]       public float duration;
}