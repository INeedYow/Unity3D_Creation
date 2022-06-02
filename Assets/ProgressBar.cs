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

    // float beforeExp;
    // float addExp;
    // float maxExp;

    public void SetBar(float curValue, float maxValue, float initValue = 0f)
    {
        cur.text = curValue.ToString();
        max.text = maxValue.ToString();
        m_amount = curValue / maxValue;

        m_initAmount = initValue;
        
        StartCoroutine("FillAmount");
    }

    public void SetBar(int curValue, int maxValue, float initValue = 0f)
    {
        cur.text = curValue.ToString();
        max.text = maxValue.ToString();
        m_amount = (float)curValue / (float)maxValue;  

        m_initAmount = initValue;
        
        StartCoroutine("FillAmount");
    }

    // public void SetExpBar(int beforeExp, int addExp, int maxExp)
    // {
    //     this.beforeExp = (float)beforeExp;
    //     this.addExp = (float)addExp;
    //     this.maxExp = (float)maxExp;

    //     cur.text = beforeExp.ToString();
    //     max.text = maxExp.ToString();
        
    //     StartCoroutine("ExpFillAmount");
    // }

    // IEnumerator ExpFillAmount()
    // {
    //     while (true)
    //     {
    //         beforeExp += Time.deltaTime * addExp;

    //         bar.fillAmount = beforeExp / maxExp;

    //         if (beforeExp >= maxExp)
    //         {

    //             maxExp = PlayerManager.instance.maxExp;
    //         }
    //     }

    // }

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
