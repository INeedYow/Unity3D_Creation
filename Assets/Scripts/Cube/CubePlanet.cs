﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePlanet : MonoBehaviour
{
    public CubeSide[] cubeSides = new CubeSide[4];
    public CubeSide curSide;
    [Space(6f)][Header("-------------------------------------------------")]
    public GameObject floatingblocksParent;
    public FloatingBlock[] floatingBlocks;
    public GameObject transparentPanel;
    public bool isRolling;

    float m_duration;
    Vector3 m_rot;

    float m_input;

    private void Awake() { Init(); }

    void Init(){
        foreach(CubeSide cube in cubeSides){ cube.onExitFinish += ExitFinish; }
        // floating block 배열 초기화
        floatingBlocks = new FloatingBlock[floatingblocksParent.transform.childCount];
        floatingBlocks = floatingblocksParent.GetComponentsInChildren<FloatingBlock>();
    }

    private void Update() 
    {
        if (isRolling) return;

        m_input = Input.GetAxisRaw("Vertical");
        
        if (m_input > 0f)
        {
            if (null != curSide.forwardSide)
            {
                LockInput(true);
                curSide.Exit();
            }
        }
        else if (m_input < 0f)
        {
            if (null != curSide.backwardSide)
            {
                LockInput(true);
                curSide.Exit();
            }
        }

        m_input = Input.GetAxisRaw("Horizontal");

        if (m_input < 0f)
        {   
            if (null != curSide.leftSide)
            {
                LockInput(true);
                curSide.Exit();
            }
        }
        else if (m_input > 0f)
        {
            if (null != curSide.rightSide)
            {
                LockInput(true);
                curSide.Exit();
            }
        }
    }

    void LockInput(bool isLock){
        isRolling = isLock;
        transparentPanel.gameObject.SetActive(isLock);
    }

    void EnterFinish(){
        LockInput(false);
    }

    void ExitFinish(CubeSide side){ Debug.Log("ExitFinish()");
        curSide = side;
        m_rot = curSide.rot;
        StartCoroutine("Roll"); 
    }

    IEnumerator Roll(){ Debug.Log("Roll()" + m_rot);
        m_duration = 0f;
        while (m_duration < 1f)
        {
            m_duration += Time.deltaTime * 1f;
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.Euler(m_rot), m_duration);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(m_rot);
        curSide.Enter();
        yield return null;
    }

    public void AddFloatingBlock(Hero hero){
        if (floatingBlocks.Length == 0) return;

        int rand = Random.Range(0, floatingBlocks.Length);

        for (int i = rand; i < floatingBlocks.Length;){
            if (floatingBlocks[rand].dummy == null){
                floatingBlocks[rand].SetDummy(hero);
                break;
            }
            else{
                i++;
                i %= floatingBlocks.Length;
            }
        }
    }

    public void RemoveFloatingBlock(Hero hero){
        foreach(FloatingBlock fb in floatingBlocks){
            if (fb.dummy.owner == hero){
                fb.Remove();
            }
        }
    }
}
