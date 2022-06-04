using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_RoundArea : RuneSkillCursor
{   
    //public ;//스킬 범위 보여줄 오브젝트 
    public GameObject skillIndicator;
    Vector3 m_cursorPos;
    bool m_isReady;

    protected override void OnEnable() {
        base.OnEnable();
        m_isReady = false;
    }

    protected override void OnDisable() {
        base.OnDisable();
        skillIndicator.SetActive(false);
    }

    protected override void CursorPosition() 
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("RunePlane")))
        {
            m_cursorPos = hit.point;

            if (!m_isReady)
            {   //Debug.Log("RoundAreaCursor.isReady true");
                m_isReady = true;
                skillIndicator.SetActive(true);
            }
            else{
                skillIndicator.transform.position = m_cursorPos;
            }
        }
        else
        {
            if (m_isReady)
            {   //Debug.Log("RoundAreaCursor.isReady false");
                m_isReady = false;
                skillIndicator.SetActive(false);
            }
        }
    }
    
    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_isReady)
            {   //Debug.Log("RoundAreaCursor 스킬 발동");
                skillObj.gameObject.SetActive(true);
                skillObj.transform.position = m_cursorPos;
                onWorks?.Invoke();
                gameObject.SetActive(false);
            }
            
        }
        else if (Input.GetMouseButtonDown(1))
        {   //Debug.Log("RoundAreaCursor 스킬 취소");
            Cancel();
        }
    }
}
