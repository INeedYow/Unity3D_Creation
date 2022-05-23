﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Hero owner;
    
    public BoardBlock tempBlock;        // 드래그 중에 저장되는 블럭
    public BoardBlock placedBlock;      // 보드에 세팅된 자기 위치 블럭
    public FloatingBlock placedFloat;   // 파티에 참가하지 않은 영웅들 있는 블럭
    public bool hasClicked;
    public bool isOnBlock;

    private void OnMouseDown() {
        hasClicked = true;
        HeroManager.instance.PickUpDummy(owner);
    }

    private void OnMouseUp() {  
        hasClicked = false;
        HeroManager.instance.PutDownDummy();

        // 이미 보드에 있던 경우
        if (owner.isJoin)
        {  
            if (tempBlock == null)
            {   // 빈 땅 -> 제자리
                transform.position = placedBlock.dummyTf.position;
            }
            else{
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

    public void OnBlock(BoardBlock block){  // 드래그 도중 처리
        if (block == null){     
            tempBlock = null;
            isOnBlock = false;
        }
        else{   
            isOnBlock = true;
            tempBlock = block;
            transform.position = tempBlock.dummyTf.position;
        } 
    }

    public void SwapBoard_Floating(Dummy floatingDummy){
        // 보드블럭 정보교환
        placedFloat = floatingDummy.placedFloat;
        floatingDummy.placedFloat = null;

        // 플로팅블럭 정보교환
        floatingDummy.placedBlock = placedBlock;
        placedBlock = null;

        // 부모정보, 포지션 설정   
        floatingDummy.transform.SetParent(floatingDummy.owner.transform);               
        floatingDummy.transform.position = floatingDummy.placedFloat.dummyTF.position;

        transform.SetParent(placedFloat.transform);
        transform.position = placedFloat.dummyTF.position;
    }

    public void Float2Board(BoardBlock block){
        placedFloat.Remove();
        placedFloat = null;

        placedBlock = block;
        transform.SetParent(owner.transform);
    }
}
