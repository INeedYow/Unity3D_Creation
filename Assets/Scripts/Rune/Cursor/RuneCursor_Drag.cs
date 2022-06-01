using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_Drag : RuneSkillCursor
{   // 
    protected override void CursorPosition() {}
    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //
            //skillObj.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(1))
        {   
            // cancel
        }
    }
}
