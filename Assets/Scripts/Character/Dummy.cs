using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Hero owner;
    public BoardBlock placedBlock;      // 보드에 세팅된 자기 위치 블럭
    public FloatingBlock placedFloat;   // 
    public bool hasClicked;

    private void OnMouseDown() {
        hasClicked = true;
        HeroManager.instance.PickUpDummy(owner);
    }

    private void OnMouseUp() {
        hasClicked = false;
        HeroManager.instance.PutDownDummy();
    }
}
