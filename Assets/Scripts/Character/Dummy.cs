using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Hero owner;

    BoardBlock _placedBlock;                                // 보드에 세팅된 자기 위치 블럭
    public BoardBlock placedBlock {
        get { return _placedBlock; }
        set {
            _placedBlock = value;
            if (_placedBlock != null) { 
                transform.SetParent(_placedBlock.transform); 
                transform.position = _placedBlock.dummyTf.position;
                placedFloat = null;

                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
    FloatingBlock _placedFloat;                             // 파티에 참가하지 않은 영웅들 있는 블럭
    public FloatingBlock placedFloat {
        get { return _placedFloat; }
        set {
            _placedFloat = value;
            if (_placedFloat != null) { 
                transform.SetParent(_placedFloat.transform); 
                transform.position = _placedFloat.dummyTF.position;
                placedBlock = null;

                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
    public BoardBlock tempBlock;          // 드래그 중에 저장되는 블럭
    public bool isOnBlock;
    Transform m_tf;

    private void Start() {
        m_tf = GetComponent<Transform>();
    }

    public void FocusedScale()
    {
        m_tf.localScale = GameManager.instance.focusedScale;
    }

    public void NormalScale()
    {
        m_tf.localScale = GameManager.instance.normalScale;
    }

    private void OnMouseEnter() {   //Debug.Log("enter");
        FocusedScale();
        //HeroManager.instance.ShowDummyInfo(this);
    }

    private void OnMouseExit() {    //Debug.Log("exit");
        NormalScale();
    }

    private void OnMouseDown() 
    {
        if (GameManager.instance.isLockFocus) return;
        
        m_tf.localScale = GameManager.instance.focusedScale;
        HeroManager.instance.PickUpDummy(owner);
    }

    // private void OnMouseUp()
    // {  
    //     m_tf.localScale = GameManager.instance.normalScale;
    //     if (GameManager.instance.isMouseOnLeaveArea)
    //     {   
    //         if (placedBlock != null)
    //         {   // 파티에서 영웅 해제
    //             placedBlock.dummy = null;
    //             placedBlock = null;
    //             PartyManager.instance.Leave(owner);
    //             HeroManager.instance.PutDownDummy();
    //             GameManager.instance.cubePlanet.AddFloatingBlock(owner);
    //             return;
    //         }
    //     }

    //     HeroManager.instance.PutDownDummy();


    //     // 이미 보드에 있던 경우
    //     if (owner.isJoin)
    //     {  
    //         if (tempBlock == null)
    //         {   // 빈 땅 -> 제자리
    //             Debug.Log("tempBlock");
    //             transform.position = placedBlock.dummyTf.position;
    //         }
    //         else{
    //             tempBlock.MoveDummy(this);
    //         }
    //     }
    //     // floatingBlock에서 드래그한 경우
    //     else{   
    //         if (tempBlock == null)
    //         {   // 빈 땅 -> 제자리
    //             transform.position = placedFloat.dummyTF.position;
    //         }
    //         else{
    //             tempBlock.TrySetDummy(this);
    //         }
    //     }
    // }

    public void OnLeftMouseUp()
    {  
        NormalScale();
        
        if (GameManager.instance.isMouseOnLeaveArea)
        {   
            if (placedBlock != null)
            {   // 파티에서 영웅 해제
                placedBlock.dummy = null;
                placedBlock = null;
                PartyManager.instance.Leave(owner);
                HeroManager.instance.PutDownDummy();
                GameManager.instance.cubePlanet.AddFloatingBlock(owner);
                return;
            }
        }

        HeroManager.instance.PutDownDummy();


        // 이미 보드에 있던 경우
        if (owner.isJoin)
        {  
            if (tempBlock == null)
            {   // 빈 땅 -> 제자리
                Debug.Log("tempBlock null");
                transform.position = placedBlock.dummyTf.position;
            }
            else{   Debug.Log("MoveDummy");
                tempBlock.MoveDummy(this);
            }
        }
        // floatingBlock에서 드래그한 경우
        else{   
            if (tempBlock == null)
            {   // 빈 땅 -> 제자리
                transform.position = placedFloat.dummyTF.position;
            }
            else{
                tempBlock.TrySetDummy(this);
            }
        }
    }

    public void OnBlock(BoardBlock block)
    {  // 드래그 도중 처리
        if (block == null){     
            tempBlock = null;
            isOnBlock = false;
        }
        else{   
            tempBlock = block;
            isOnBlock = true;
            transform.position = tempBlock.dummyTf.position;
        } 
    }

    public void SwapBoard_Floating(Dummy floatingDummy){ 
        BoardBlock tempBlock = placedBlock;

        placedFloat = floatingDummy.placedFloat;
        floatingDummy.placedBlock = tempBlock;
    }

    public void Float2Board(BoardBlock block){
        placedFloat.Remove();
        //placedFloat = null;

        placedBlock = block;
        //transform.SetParent(owner.transform); // 캐릭터에게 돌려줄 필요없음
    }
}
