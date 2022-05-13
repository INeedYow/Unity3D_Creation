using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardBlock : MonoBehaviour
{
    public Dummy dummy;
    public Vector3 dummyPos;
    //public Vector3 beginPos; // 그냥 얘 position이 beginPos임

    public void Init() {
        //beginPos = transform.position;
        dummyPos = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);
    }

    public void SetDummy(Dummy dummy){
        if (this.dummy != null)
        {
            // TODO 기존 더미는 유닛 UI에 추가하고 보드에서는 새 더미로 교체
        }
        this.dummy = dummy;
        dummy.beginPos = transform.position;
        
        // 더미 위치에 맞게 해당 hero 시작 위치 설정해주는 작업인데
            // 던전 진입 전 최종 더미 위치로 설정해주면 최적화? 될 것 같고
                // dummy가 hero 찾아들어가서 beginPos 설정해주는 것보다 hero가 자기 dummy beginPos를 참조하는 게 낫나
        //Hero hero = dummy.owner.GetComponentInParent<Hero>();
        //hero.beginPos = transform.position;
        
    }
}
