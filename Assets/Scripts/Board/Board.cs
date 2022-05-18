using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<Dummy> dummies = new List<Dummy>();
    public Transform centerTf;      // 보드의 블럭들이 자신의 BeginPos 계산할 중심점
    public bool isActive;

    void ShowDummy() { PartyManager.instance.ShowDummy(); }
    //void HideDummy() { PartyManager.instance.HideDummy(); }

    public void Init(){
        StartCoroutine("SetPos");
        //StartCoroutine("SetRot");
    }

    // IEnumerator SetRot(){

    //     float dura = 0f;
    //     while (dura < 1f){
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(20f, 30f, 80f), dura);
    //         dura += Time.deltaTime * 1f;
    //         yield return null;
    //     }
    //     //transform.rotation = Quaternion.Euler(0f, -10f, 10f);
    //     yield return null;
    // }

    IEnumerator SetPos()
    {
        float x = 2f;
        float xValue = 6f;
        while (transform.localPosition.x < xValue){
            transform.Translate(xValue * Time.deltaTime * x, 0f , 0f);
            yield return null;
        }
        transform.localPosition = new Vector3(xValue, 0f, 0f);

        x = 2f;
        float yValue = -6f;
        while (transform.localPosition.y > yValue){
            x += Time.deltaTime;
            transform.Translate(0f, yValue * Time.deltaTime * x, 0f);
            yield return null;
        }
        transform.localPosition = new Vector3(xValue, yValue, 0f);

        
        isActive = true;
        ShowDummy();
        yield return null;
    }
}
