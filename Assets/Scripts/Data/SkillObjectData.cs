using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillObjectData", menuName = "Data/SkillObject")]
public class SkillObjectData : ScriptableObject
{
    public bool isMagic;
    [Range(0.1f, 10f)]      public float powerRatio;    // 계수
    [Range(0f, 30f)]        public float area = 0f;
    [Tooltip("개수")]       public int count = 1;
    [Range(0f, 20f)]        public float duration;
    [Range(0.1f, 1f)]       public float buffRatio;
    
}
