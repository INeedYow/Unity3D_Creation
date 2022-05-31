﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_SingleTarget : RuneCursor
{
    [Header("Skill Object")]
    public SkillObject skillObj;

    [HideInInspector] public EGroup eTargetGroup;
    [HideInInspector] public float value;

    [SerializeField] // 디버그용
    Character m_target;

    public void SetCursor(EGroup eTargetGroup, float value)
    {
        this.eTargetGroup = eTargetGroup;
        this.value = value;
    }

    private void Update() 
    {
        //CursorPosition();
        CheckInput();
    }

    void CursorPosition()
    {
        //transform.position = ;
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {   // 좌클릭
            if (m_target == null) return;

            skillObj.gameObject.SetActive(true);
        }

        else
        {   // 우클릭
            
        }
         

        
    }

    private void OnTriggerEnter(Collider other) 
    {   // TODO 타겟 포커싱 효과 주기
        if (eTargetGroup == EGroup.Monster)
        {
        }
    }
}
