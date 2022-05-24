using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    float lastClickTime;
    Transform m_tf;

    private void Awake() { 
        m_tf = GetComponent<Transform>();  
    }

    public bool Buy()       { return data.Buy(); }
    public bool Sell()      { return data.Sell(); }

    private void OnMouseDown() { 
        if (Time.time < lastClickTime + 1f)
        {   //Debug.Log("DB Click");
            if (Buy()) {
                return;
            }
        
            else{

            }
        }
        lastClickTime = Time.time;
    }

    // TODO 설명
    private void OnMouseEnter() {   //Debug.Log("enter");
        m_tf.localScale += GameManager.instance.focusedScale;
        ItemManager.instance.ShowInfoUI(data);
    }

    private void OnMouseExit() {    //Debug.Log("exit");
        m_tf.localScale -= GameManager.instance.focusedScale;
        ItemManager.instance.HideInfoUI();
    }
}
