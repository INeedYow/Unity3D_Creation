using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSide_Board : CubeSide
{
    [Space(10f)][Header("--------------UI--------------")]
    //public GameObject UI;

    [Space(4f)][Header("------------------------------")]
    public Board board;
    public Transform groundTF;

    float m_boardRot;
    bool m_isExit;

    public override void Enter()
    {
        StartCoroutine("OpenBoard"); 
    }

    public override void Exit()
    {   //Debug.Log("boardside Exit()");
        if (board.isActive){ Debug.Log("if board.isActive");
            m_isExit = true;
            StartCoroutine("CloseBoard"); 
        }
        else { 
            onExitFinish?.Invoke(this); 
        }

        //PartyManager.instance.TurnOffBoard();
    }

    public void TurnOnBoard(){
       
        StartCoroutine("OpenBoard");
    }

    public void TurnOffBoard(){
        
        StartCoroutine("CloseBoard");
    }

    IEnumerator OpenBoard(){    //Debug.Log("co open()");
        float dura = 0f;
        m_boardRot = 180f;
        board.isActive = false;
 
        yield return null;

        while (dura < 1f){
            groundTF.rotation = Quaternion.Slerp(groundTF.rotation, Quaternion.Euler(0f, 0f, m_boardRot), dura);
            dura += Time.deltaTime;
            yield return null;
        }

        groundTF.rotation = Quaternion.Euler(0f, 0f, m_boardRot);
        board.isActive = true;
        
        onEnterFinish?.Invoke();
        yield return null;
    }

    IEnumerator CloseBoard(){   //Debug.Log("co close");
        float dura = 0f;
        m_boardRot = 0f;
        board.isActive = false;

        yield return null;

        while (dura < 1f){
            groundTF.rotation = Quaternion.Slerp(groundTF.rotation, Quaternion.Euler(0f, 0f, m_boardRot), dura);
            dura += Time.deltaTime;
            yield return null;
        }

        groundTF.rotation = Quaternion.Euler(0f, 0f, m_boardRot);
        if (m_isExit) onExitFinish?.Invoke(this);

        yield return null;
    }
}
