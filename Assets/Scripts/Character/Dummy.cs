using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Hero owner;
    public BoardBlock placedBlock;      // 보드에 세팅된 자기 위치 블럭
    public FloatingBlock placedFloat;   // 
    public bool hasClicked;
    public bool isOnBlock;
    private void OnMouseDown() {
        hasClicked = true;
        HeroManager.instance.PickUpDummy(owner);
    }

    private void OnMouseUp() {  Debug.Log("MouseUp()");
        hasClicked = false;
        HeroManager.instance.PutDownDummy();

        if (placedBlock == null){
            transform.position = placedFloat.dummyTF.position;
        }
        else{
            //placedFloat.Remove(); // 이미 있던 경우 체인지
            //placedFloat = null;
            //placedBlock.SetDummy(this);
        }
    }

    public void SetBlock(BoardBlock block){ // 확정되면 setParent해줄것
        if (block == null){     
            placedBlock = null;
            isOnBlock = false;
        }
        else{   
            isOnBlock = true;
            placedBlock = block;
            transform.position = placedBlock.dummyTf.position;
        } 
    }


}
