using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "RuneSkillObjectData", menuName = "RuneSkill / RuneSkillObjectData")]
public class RuneSkillObjectData : ScriptableObject
{
    [Header("Info--------")]
    public float power;
    public float area;
    [Range(1, 40)]  public int repeat = 1;
    [Range(0f, 2f)] public float repeatInterval;


    [Space(5f)] [Header("Buff--------")]
    public EBuff eBuff;
    [Range(-1f, 1f)] public float buffRatio;
    [Range(0f, 10f)] public float duration;


    [Space(5f)] [Header("Effect--------")]
    public EEffect eWorldEffect;
    public EEffect eOnWorksEffect;
}
