using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubePlanet : MonoBehaviour
{
    public CubeSide[] cubeSides = new CubeSide[4];
    public CubeSide curSide;
    CubeSide m_nextSide;
    [Space(6f)][Header("-------------------------------------------------")]
    public GameObject floatingblocksParent;
    public FloatingBlock[] floatingBlocks;
    public bool isRolling;

    float m_duration;
    Vector3 m_rot;

    float m_input;

    private void Awake() { Init(); }

    void Init(){
        // 이벤트 함수 등록
        foreach(CubeSide cubeSide in cubeSides){ 
            cubeSide.onExitFinish += ExitFinish; 
            cubeSide.onEnterFinish += EnterFinish;
        }

        // floating block 배열 초기화
        floatingBlocks = new FloatingBlock[floatingblocksParent.transform.childCount];
        floatingBlocks = floatingblocksParent.GetComponentsInChildren<FloatingBlock>();
    }

    private void Start() {
        curSide.Enter();
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
                m_nextSide = curSide.forwardSide;
                curSide.Exit();
            }
        }
        else if (m_input < 0f)
        {   
            if (null != curSide.backwardSide)
            {
                LockInput(true);
                m_nextSide = curSide.backwardSide;
                curSide.Exit();
            }
        }

        m_input = Input.GetAxisRaw("Horizontal");

        if (m_input < 0f)
        {    
            if (null != curSide.leftSide)
            {
                LockInput(true);
                m_nextSide = curSide.leftSide;
                curSide.Exit();
            }
        }
        else if (m_input > 0f)
        {   
            if (null != curSide.rightSide)
            {
                LockInput(true);
                m_nextSide = curSide.rightSide;
                curSide.Exit();
            }
        }
    }

    void LockInput(bool isLock){    
        isRolling = isLock;
        GameManager.instance.LockInput(isLock);
    }

    void EnterFinish(){
        LockInput(false);
    }

    void ExitFinish(CubeSide side){ //Debug.Log("ExitFinish()");
        curSide = m_nextSide;
        m_rot = m_nextSide.rot;
        StartCoroutine("Roll"); 
    }

    IEnumerator Roll(){ //Debug.Log("Roll()" + m_rot);
        m_duration = 0f;
        while (m_duration < 1f)
        {
            m_duration += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.Euler(m_rot), m_duration);

            yield return null;
        }

        transform.rotation = Quaternion.Euler(m_rot);
        curSide.Enter();
        yield return null;
    }

    public void AddFloatingBlock(Hero hero){                        //Debug.Log("AddFloatingBlock()");
        if (floatingBlocks.Length == 0) return;

        int randIndex = Random.Range(0, floatingBlocks.Length);     //Debug.Log("floatingBlocks.Length" + floatingBlocks.Length);

        for (int i = 0; i < floatingBlocks.Length; i++){            //Debug.Log("i / rand : " + i + " / " + rand);
            if (floatingBlocks[randIndex].dummy == null){           //Debug.Log("dummy : " + floatingBlocks[rand].dummy);
                floatingBlocks[randIndex].SetDummy(hero);           //Debug.Log("SetDummy");
                break;
            }
            else{
                randIndex++;                                        //Debug.Log("i++ : " + i);
                randIndex %= floatingBlocks.Length;                 //Debug.Log("i %= length : " + i);
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
