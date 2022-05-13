using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour
{
    public Transform lookAt_map;
    public Transform lookAt_board;
    public Image upMouseBox;
    public Image downMouseBox;

    Transform m_defaultTf;
    Quaternion m_defaultQuat;


    private void Start() {
        m_defaultTf = transform;
        m_defaultQuat = Quaternion.identity;

        upMouseBox.gameObject.SetActive(false);
    }

    public void ShowBoard(){
        Camera.main.transform.LookAt(lookAt_board);
        upMouseBox.gameObject.SetActive(true);
        downMouseBox.gameObject.SetActive(false);
    }
    
    public void ShowMap(){
        Camera.main.transform.LookAt(lookAt_map);
        upMouseBox.gameObject.SetActive(false);
        downMouseBox.gameObject.SetActive(true);
    }
}
