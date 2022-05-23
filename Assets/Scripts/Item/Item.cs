using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
    float lastClickTime;
    Transform m_tf;
    public static Vector3 s_scale;

    private void Awake() { 
        m_tf = GetComponent<Transform>();  
        //s_scale = new Vector3 (0.3f, 0.3f, 0.3f);
    }

    public bool Buy()       { return data.Buy(); }
    public bool Sell()      { return data.Sell(); }

    private void OnMouseDown() { //Debug.Log("Click");
        if (Time.time < lastClickTime + 1f)
        {   //Debug.Log("DB Click");
            Buy(); return;
        }
        lastClickTime = Time.time;
    }

    // TODO 설명
    private void OnMouseEnter() {   //Debug.Log("enter");
        m_tf.localScale += s_scale;
        ItemManager.instance.ShowInfoUI(data);
    }

    private void OnMouseExit() {    //Debug.Log("exit");
        m_tf.localScale -= s_scale;
        ItemManager.instance.HideInfoUI();
    }
}
