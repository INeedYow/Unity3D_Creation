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
        //SetBeginPos(PartyManager.instance.board.centerTf);
        SetBeginPos();
    }

    public void SetBeginPos()
    {
        // beginPos = new Vector3(
        //     tf.position.y-transform.position.y,
        //     0f,
        //     -tf.position.z + transform.position.z);
        
        beginPos = new Vector3(-transform.position.x, 0f, transform.position.z);
    }

    // private void OnMouseEnter() {
    //     if (HeroManager.instance.selectedHero != null && PartyManager.instance.board.isActive)
    //     {   Debug.Log("d");
    //         HeroManager.instance.selectedHero.dummy.SetBlock(this);
    //     }
    // }

    // private void OnMouseExit() {
    //     if (HeroManager.instance.selectedHero != null && PartyManager.instance.board.isActive)
    //     {   Debug.Log("x");
    //         HeroManager.instance.selectedHero.dummy.SetBlock(null);
    //     }
    // }
    // }
    // private void OnMouseDown() {
    //     //Debug.Log("OnMouseDown, selectedhero null");
    //     if (HeroManager.instance.selectedHero == null) return;
    //     if (!PartyManager.instance.board.isActive) return;
    //     //Debug.Log("OnMouseDown");
        
    //     if (null == dummy)
    //     {   
    //         SetDummy();
    //     }
    //     else{   // 이미 다른 영웅이 있던 경우
    //         Hero hero = dummy.GetComponentInParent<Hero>();
    //         if (hero == null) return;
            
    //         hero.Leave();

    //         SetDummy();
    //     }
    // }

    // void SetDummy()
    // {
    //     Hero hero = HeroManager.instance.selectedHero.GetComponent<Hero>();
            
    //     if (hero == null) return;

    //     hero.Join();
    //     dummy = HeroManager.instance.selectedHero.dummy;
    //     dummy.placedBlock = this;
    //     HeroManager.instance.selectedHero = null;
    // }

    ///////////////////////////////////

//

    public void SetDummy(Dummy dm){
        if (dummy == null){
            dummy = dm;
        }
        else{
            //dummy.
        }
    }
}
