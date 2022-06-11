using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBlock : MonoBehaviour
{
    public Dummy dummy;
    public Transform dummyTF;
    public float maxFloatSpeed;
    public float minFloatSpeed ;
    public float curFloatSpeed;
    public float accel ;
    public bool isMoving;
    float m_floatHeight = 10;
    float m_defaultHeight;

    private void OnDisable() { StopCoroutine("InitFloating"); }
    private void Awake() {
        m_defaultHeight = GetComponent<Transform>().position.y;
    }
    public void SetDummy(Hero hero){
        if (hero == null) return;
        hero.dummy.gameObject.SetActive(true); // temp
        dummy = hero.dummy;
        dummy.placedFloat = this;
        StopCoroutine("Falldown");
        StartCoroutine("InitFloating");
    }

    public void Remove(){
        dummy = null;
        StartCoroutine("Falldown");
    }

    IEnumerator InitFloating()
    {
        isMoving = true;
        curFloatSpeed = maxFloatSpeed;

        while (m_floatHeight > transform.position.y)
        {
            transform.Translate(0f, curFloatSpeed * Time.deltaTime, 0f);

            if (curFloatSpeed > minFloatSpeed)
            {
                curFloatSpeed -= Time.deltaTime * accel;
            }
            yield return null;
        }

        isMoving = false;
    }

    IEnumerator Falldown()
    {                
        isMoving = true;
        curFloatSpeed = minFloatSpeed;

        while (m_defaultHeight < transform.position.y)
        {   
            transform.Translate(0f, -curFloatSpeed * Time.deltaTime, 0f);

            if (curFloatSpeed < maxFloatSpeed)
            {
                curFloatSpeed += Time.deltaTime * accel;
            }
            yield return null;
        }

        isMoving = false;
    }
}
