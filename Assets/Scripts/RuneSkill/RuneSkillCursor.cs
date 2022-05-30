using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillCursor : MonoBehaviour
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
                // 스킬 사용 후 쿨타임 생성(이벤트 or 캐싱)
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {   
            // 취소
        }
    }
}
