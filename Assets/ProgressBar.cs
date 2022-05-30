using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    public UnityAction onFinishFill;
    
    public Text cur;
    public Text max;
    public Image bar;

    float m_initAmount;
    float m_amount;

    public void SetBar(float curValue, float maxValue, float initValue = 0f)
    {
        cur.text = curValue.ToString();
        max.text = maxValue.ToString();
        m_amount = curValue / maxValue;
        
        StartCoroutine("FillAmount");
    }

    public void SetBar(int curValue, int maxValue)
    {
        cur.text = curValue.ToString();
        max.text = maxValue.ToString();
        m_amount = (float)curValue / (float)maxValue;  

        //Debug.Log((float)curValue + " / " + (float)maxValue + " = " + (float)curValue / (float)maxValue);

        StartCoroutine("FillAmount");
    }


    IEnumerator FillAmount()
    {
        float dura = 0f;

        while (dura < 1f)
        {
            bar.fillAmount = Mathf.Lerp(m_initAmount, m_amount, dura);
            dura += Time.deltaTime;
            yield return null;
        }

        onFinishFill?.Invoke();
        yield return null;
    }

}
