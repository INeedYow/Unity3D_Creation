using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_Target : RuneSkillCursor
{   
    [Header("RuneCursor_Target")]
    public Character target;

    protected override void CursorPosition() {}
    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0) && target != null)
        {   //
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // TODO LayerMask
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("RunePlane")))
            {   
                skillObj.gameObject.SetActive(true);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {   
            // cancel
        }
    }
}
