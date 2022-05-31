using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneSkillCursor : MonoBehaviour
{
    public Texture2D texture;

    Ray ray;
    RaycastHit hit;

    private void OnEnable() {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        // MGR.Plane.SetActive(true);
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {  
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // TODO LayerMask
            if (Physics.Raycast(ray, out hit, 300f, LayerMask.GetMask("RunePlane")))
            {   
                Execute();
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {   
            // 취소
        }
    }

    protected abstract void Execute();
}
