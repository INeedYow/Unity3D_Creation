using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardBlock : MonoBehaviour
{
    public Dummy dummy;
    public Transform dummyTf;
    public Vector3 beginPos;

    public void Init() {
        beginPos = transform.position;
    }

    // public void SetDummy(Dummy dummy){
    //     if (this.dummy != null)
    //     {
    //         // TODO 기존 더미는 유닛 UI에 추가하고 보드에서는 새 더미로 교체
    //     }
    //     this.dummy = dummy;
    //     dummy.beginPos = transform.position;
    // }

    private void OnMouseEnter() {
        if (GameManager.instance.selectedHero != null)
        {
            GameManager.instance.selectedHero.dummy.transform.position = dummyTf.position;
        }
    }
    private void OnMouseDown() {
        if (null == dummy)
        {
            this.dummy = GameManager.instance.selectedHero.dummy;
            dummy.beginPos = beginPos;
            GameManager.instance.selectedHero = null;

            // temp
            Hero hero = dummy.GetComponentInParent<Hero>();
            hero?.Join();
        }
    }
}
