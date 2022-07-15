using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEffetor : MonoBehaviour
{
    public Vector3 focusedScale;
    Vector3 m_defaultScale;

    private void Awake() { 
        Init();
    }

    void Init() { m_defaultScale = GetComponent<Transform>().localScale; }

    private void OnMouseEnter() 
    {   
        if (GameManager.instance.isLockFocus) return;
        
        transform.localScale = focusedScale; 
    }
    private void OnMouseExit()  
    { 
        transform.localScale = m_defaultScale; 
    }
}