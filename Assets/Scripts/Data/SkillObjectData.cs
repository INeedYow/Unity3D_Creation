using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillObjectData", menuName = "SkillData/SkillObject")]
public class SkillObjectData : ScriptableObject
{
    public bool isMagic;
    [Tooltip("계수")]
    [Range(0f, 10f)]        public float powerRatio;    // 계수
    [Range(0f, 30f)]        public float area = 0f;
    [Tooltip("개수")]       public int count = 1;
    
    [Header("Buff (+) / Nerf (-) ")]
    public EBuff eBuff;
    [Range(0f, 20f)]        public float duration;
    [Range(-1f, 1f)]        public float buffRatio;

    [Space(5f)] [Header("Effect--------")]
    [Tooltip ("스킬 시전시 생성")]      public EEffect eStartEffect;
    [Tooltip ("사용자 위치에 생성")]    public EEffect eUserEffect;
    [Tooltip ("대상 위치에 생성")]      public EEffect eTargetEffect;
}
