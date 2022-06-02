using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_Normal : RuneSkillCursor
{   // 어느 곳이든 클릭하면 발동하게
    protected override void CursorPosition() {}

    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            skillObj.gameObject.SetActive(true);
            onWorks?.Invoke();
            gameObject.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1))
        {   
            Cancel();
        }
    }
}
