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
            Debug.Log(hero);
            if (hero != null)
            {
                PartyManager.instance.Join(hero);
            }
        }
    }
}
