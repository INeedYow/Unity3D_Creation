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

    private void OnMouseEnter() {
        if (HeroManager.instance.selectedHero != null)
        {
            HeroManager.instance.selectedHero.dummy.transform.position = dummyTf.position;
        }
    }
    private void OnMouseDown() {
        if (HeroManager.instance.selectedHero == null) return;
        
        if (null == dummy)
        {   // TODO 최대 인원제한 필요
            SetDummy();
        }
        else{   // 이미 다른 영웅이 있던 경우
            Hero hero = dummy.GetComponentInParent<Hero>();
            if (hero == null) return;
            hero.Leave();

            SetDummy();
        }
    }

    void SetDummy()
    {
        Hero hero = HeroManager.instance.selectedHero.GetComponent<Hero>();
            
        if (hero == null) return;

        hero.Join();
        dummy = HeroManager.instance.selectedHero.dummy;
        dummy.placedBlock = this;
        HeroManager.instance.selectedHero = null;
    }
}
