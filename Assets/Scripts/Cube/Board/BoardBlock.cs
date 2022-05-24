using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardBlock : MonoBehaviour
{
    public Dummy dummy;
    public Transform dummyTf;
    public Vector3 beginPos;

    void Start()
    {
        SetBeginPos();
    }

    public void SetBeginPos()
    {
        beginPos = new Vector3(-transform.position.x, 0f, transform.position.z);
    }

    public void MoveDummy(Dummy dm)
    {
        // 빈 블럭인 경우
        if (dummy == null)
        {   //Debug.Log("빈 블럭에 옮김" + dummy);
            dummy = dm;
            dm.placedBlock = this;
            dm.transform.position = dummyTf.position;
        }
        // 이미 있는 경우
        else{   //Debug.Log("이미 있음");
            SwapDummy(dm.placedBlock);
        }
    }

    public void TrySetDummy(Dummy newDummy)
    {
        // 추가 못 하는 경우
        if (PartyManager.instance.IsFull()) {
            newDummy.transform.position = newDummy.placedFloat.dummyTF.position;
            return;
        }

        // 이미 있는 경우
        if (dummy != null)
        {   
            dummy.SwapBoard_Floating(newDummy);
            PartyManager.instance.Leave(dummy.owner);
            PartyManager.instance.Join(newDummy.owner);
        }
        // 빈 블럭인 경우
        else
        {   
            dummy = newDummy;
            newDummy.Float2Board(this);
            PartyManager.instance.Join(newDummy.owner);
        }
    }

    void SwapDummy(BoardBlock block){
        Dummy tempD = dummy;
        dummy = block.dummy;
        block.dummy = tempD;

        dummy.placedBlock = this;
        block.dummy.placedBlock = block;

        dummy.transform.position = dummyTf.position;
        block.dummy.transform.position = block.dummyTf.position;
    }
}
