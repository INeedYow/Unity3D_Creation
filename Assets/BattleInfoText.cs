using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoText : MonoBehaviour
{
    public Text infoText;
    public float duration;
    public float alphaSpeed;
    public float ySpeed;
    public Color myColor;

    private void OnEnable()     { StartCoroutine("FadeMove"); }
    private void OnDisable()    { StopCoroutine("FadeMove"); }

    IEnumerator FadeMove(){
        Invoke("Return", duration);
        myColor = infoText.color;
        yield return null;

        while (true)
        {
            transform.Translate(0f, ySpeed, 0f);
            myColor.a -= Time.deltaTime * alphaSpeed;
            infoText.color = myColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Return() { ObjectPool.instance.ReturnObj(gameObject); }
}
