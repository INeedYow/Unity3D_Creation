using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMouseTracker : MonoBehaviour
{
    public Vector3 offset;
    
    public float lockH;
    public float lockV;
    
    public RectTransform rtf;
    Vector3 pos;

    private void Awake() {
        if (rtf == null) rtf = GetComponent<RectTransform>();
    }

    private void FixedUpdate() {
        pos = Input.mousePosition + offset;

        if (pos.x < lockH)                        { pos.x = lockH; }
        else if (pos.x > Screen.width - lockH)    { pos.x = Screen.width - lockH; }

        if (pos.y < lockV)                        { pos.y = lockV; }
        else if (pos.y > Screen.height - lockV)   { pos.y = Screen.height - lockV; }

        rtf.position = pos; 
    }
}
