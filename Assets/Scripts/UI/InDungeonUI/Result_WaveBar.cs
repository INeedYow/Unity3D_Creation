using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Result_WaveBar : MonoBehaviour
{
    public UnityAction onFinishFill;

    public Image bar;
    public Text cur;
    public Text max;

    float m_amount;


    public void SetWaveBar(int curValue, int maxValue)
    {
        cur.text = curValue.ToString();
        max.text = maxValue.ToString();
        m_amount = (float)curValue / (float)maxValue;  
        
        StartCoroutine("FillAmount");
    }

    IEnumerator FillAmount()
    {
        float dura = 0f;

        while (dura < 1f)
        {

            bar.fillAmount = Mathf.Lerp(0f, m_amount, dura);
            dura += Time.deltaTime;
            yield return null;
        }

        onFinishFill?.Invoke();
        yield return null;
    }
}
