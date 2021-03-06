using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "SkillData", menuName = "SkillData/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;
    [Space(10f)]
    [Tooltip("습득 레벨")]   public int requireLevel;
    [Range(1f, 40f)]        public float cooldown;
    [Range(0f, 20f)]        public float skillRange;     // 스킬 발동 사거리
}