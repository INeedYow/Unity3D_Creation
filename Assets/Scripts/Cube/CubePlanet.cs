using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePlanet : MonoBehaviour
{
    public CubeSide curSide;
    public GameObject transparentPanel;
    public bool isRolling;

    float m_duration;
    Vector3 m_rot;

    float m_input;

    private void Update() 
    {
        if (isRolling) return;

        m_input = Input.GetAxisRaw("Vertical");
        
        if (m_input > 0f)
        {
            if (null != curSide.forwardSide)
            {
                curSide.Exit();
                curSide = curSide.forwardSide;

                m_rot = curSide.rot;
                StartCoroutine("Roll");

                curSide.Enter();
            }
        }
        else if (m_input < 0f)
        {
            if (null != curSide.backwardSide)
            {
                curSide.Exit();
                curSide = curSide.backwardSide;
                
                m_rot = curSide.rot;
                StartCoroutine("Roll");

                curSide.Enter();
            }
        }

        m_input = Input.GetAxisRaw("Horizontal");

        if (m_input < 0f)
        {   
            if (null != curSide.leftSide)
            {
                curSide.Exit();
                curSide = curSide.leftSide;
                
                m_rot = curSide.rot;
                StartCoroutine("Roll");

                curSide.Enter();
            }
        }
        else if (m_input > 0f)
        {
            if (null != curSide.rightSide)
            {
                curSide.Exit();
                curSide = curSide.rightSide;
                
                m_rot = curSide.rot;
                StartCoroutine("Roll");

                curSide.Enter();
            }
        }
    }

    void RollStart(){
        isRolling = true;
        transparentPanel.gameObject.SetActive(true);
    }

    void RollEnd(){
        isRolling = false;
        transparentPanel.gameObject.SetActive(false);
    }
    IEnumerator Roll()
    {
        RollStart();
        m_duration = 0f;

        while (m_duration < 1f)
        {
            m_duration += Time.deltaTime * 1f;
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.Euler(m_rot), m_duration);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(m_rot);
        RollEnd();
        yield return null;
    }
}
