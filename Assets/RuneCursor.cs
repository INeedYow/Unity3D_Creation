using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor : MonoBehaviour
{
    Rune rune;
    RaycastHit hit;
    Ray ray;
    LayerMask mask;

    private void Start() {
        mask = LayerMask.GetMask("RuneBlock");
    }

    private void Update() 
    {

        // if (Input.GetMouseButtonDown(0))
        // {  
        //     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        //     if (Physics.Raycast(ray, out hit, 300f, mask))
        //     {   
        //         rune = hit.transform.GetComponent<Rune>();
        //         rune?.OnLeftClicked();
        //     }
        // }

        // else if (Input.GetMouseButtonDown(1))
        // {   Debug.Log("R mouse btn down");
        //     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     Debug.Log("1");
        //     if (Physics.Raycast(ray, out hit, 300f, mask))
        //     {   Debug.Log("2");
        //         rune = hit.transform.GetComponent<Rune>();
        //         rune?.OnLeftClicked();
        //     }
        // }


    }
    // private void FixedUpdate() {
    //     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
    //     {
    //         transform.position = hit.point;
    //     }
    // }
}
