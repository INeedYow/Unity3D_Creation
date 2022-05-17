using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<Dummy> dummies = new List<Dummy>();
    public Transform centerTf;
    public bool isActive;

    private void OnDisable() {
        if (PartyManager.instance != null) HideDummy();
    }

    void ShowDummy() { PartyManager.instance.ShowDummy(); }
    void HideDummy() { PartyManager.instance.HideDummy(); }

    public void Init(){
        StartCoroutine("SetPos");
    }

    IEnumerator SetRot(){

        float rot = 0f;
        while (rot > -25f){
            transform.rotation = Quaternion.Euler(0f, 0f, rot);
            rot -= Time.deltaTime * 35f;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, -25f);
        yield return null;

        rot = 0f;
        while (rot < 10f){
            transform.rotation = Quaternion.Euler(rot, 0f, -25f);
            rot += Time.deltaTime * 13;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(10f, 0f, -25f);
        isActive = true;
        yield return null;
    }

    IEnumerator SetPos(){

        //float value = 0f;
        while (gameObject.transform.position.x < 15f){
            transform.Translate(Time.deltaTime * 15f, 0f , 0f);
            //rot += Time.deltaTime * 6f;
            Debug.Log(gameObject.transform.position.x);
            yield return null;
        }
        transform.position = new Vector3(15f, 0f, 0f);
        StartCoroutine("SetRot");
        yield return null;
    }
}
