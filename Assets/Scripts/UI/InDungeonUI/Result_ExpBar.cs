using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_ExpBar : MonoBehaviour
{
    public Text cur;
    public Text max;
    public Text LV;
    public Image bar;
    bool isFinish;

    private void OnEnable() { Init(); }

    private void OnDisable() {
        PlayerManager.instance.onLevelUp -= RenewLevelText;
    }
    
    void Init()
    {
        PlayerManager.instance.onLevelUp += RenewLevelText;
        isFinish = false;

        cur.text = PlayerManager.instance.curExp.ToString();
        max.text = PlayerManager.instance.maxExp.ToString();
        LV.text = PlayerManager.instance.LV.ToString();
    }

    void RenewLevelText(int lv)
    {   //Debug.Log("RenewExpBar LV : " + lv + " / max : " + PlayerManager.instance.maxExp);
        LV.text = lv.ToString();
        max.text = PlayerManager.instance.maxExp.ToString();
    }

    private void LateUpdate() 
    {
        if (isFinish) return;

        bar.fillAmount = PlayerManager.instance.curExp / PlayerManager.instance.maxExp;
        cur.text = PlayerManager.instance.curExp.ToString();
    }

    public void SetFinish()
    {
        isFinish = true;
    }
}
