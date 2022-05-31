using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_World : RuneSkillCursor
{
    [Header("Skill Object")]
    public SkillObject skillObj;

    //[HideInInspector] public EGroup eTargetGroup; // skillObj에 있음
    [HideInInspector] public float value;

    [SerializeField] // 디버그용
    public void SetCursor(float value)
    {
        this.value = value;
    }

    protected override void Execute()
    {
        skillObj.gameObject.SetActive(true);
    }
}
