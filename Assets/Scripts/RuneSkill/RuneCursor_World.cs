using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_World : RuneCursor
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
            skillObj.gameObject.SetActive(true);
        }

        else
        {   // 우클릭
            
        }
         

        
    }
}
