using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePlanet : MonoBehaviour
{
    public CubeSide curSide;
    public GameObject transparentPanel;
    public bool isRolling;
    public float rollSpeed;
    
    Vector3 m_rotEuler;
    Vector3 m_dirVec;
    float m_duration;
    float m_rot;
    
    private void Update() {
        if (isRolling) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (null != curSide.forwardSide)
            {
                curSide.Exit();
                curSide = curSide.forwardSide;
                transform.Rotate(0f, 0f, -90f, Space.World);
                curSide.Enter();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (null != curSide.backwardSide)
            {
                curSide.Exit();
                curSide = curSide.backwardSide;
                transform.Rotate(0f, 0f, 90f, Space.World);
                curSide.Enter();
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {   
            if (null != curSide.leftSide)
            {
                curSide.Exit();
                curSide = curSide.leftSide;
                transform.Rotate(90f, 0f, 0f, Space.World);
                curSide.Enter();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (null != curSide.rightSide)
            {
                curSide.Exit();
                curSide = curSide.rightSide;
                transform.Rotate(-90f, 0f, 0f, Space.World);
                curSide.Enter();
            }
        }
        

    //     if (Input.GetKeyDown(KeyCode.W))
    //     {
    //         if (null != curSide.forwardSide)
    //         {   Debug.Log("forward");
    //             curSide.Exit();
    //             curSide = curSide.forwardSide;
                
    //             m_rotEuler = new Vector3(0f, 0f, -90f);

    //             StartCoroutine("Roll");
    //             curSide.Enter();
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         if (null != curSide.backwardSide)
    //         {   Debug.Log("backward");
    //             curSide.Exit();
    //             curSide = curSide.backwardSide;

    //             m_rotEuler = new Vector3(0f, 0f, 90f);

    //             StartCoroutine("Roll");
    //             curSide.Enter();
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.A))
    //     {   
    //         if (null != curSide.leftSide)
    //         {   Debug.Log("left");
    //             curSide.Exit();
    //             curSide = curSide.leftSide;

    //             m_rotEuler = new Vector3(-90f, 0f, 0f);

    //             StartCoroutine("Roll");
    //             curSide.Enter();
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         if (null != curSide.rightSide)
    //         {   Debug.Log("right");
    //             curSide.Exit();
    //             curSide = curSide.rightSide;

    //             m_rotEuler = new Vector3(90f, 0f, 0f);

    //             StartCoroutine("Roll");
    //             curSide.Enter();
    //         }
    //     }
    // }

    // void RollStart(){
    //     isRolling = true;
    //     transparentPanel.gameObject.SetActive(true);
    // }

    // void RollEnd(){
    //     isRolling = false;
    //     transparentPanel.gameObject.SetActive(false);
    // }

    // IEnumerator Roll()
    // {
    //     Debug.Log("Co_Roll() Start");
    //     RollStart();
        
        
    //     while (m_duration < 1f){
    //         m_duration += Time.deltaTime;
    //         transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * 90f);
            
    //         yield return null;
    //     }
    //     //transform.rotation = Quaternion.Euler(m_rotEuler);
    //     m_duration = 0f;

    //     RollEnd();
    //     Debug.Log("Co_Roll() Finish");
    //     yield return null;
    
    // }

    }
}
