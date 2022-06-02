using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [Header("bar")]
    public ProgressBar waveBar;
    public ProgressBar playerExpBar;
    //[Header("Hero Exp")]
    //public HeroExpUnit
    
    public int beforeExp;

    private void OnEnable() { Init(); }
    private void OnDisable() {
        TurnOnUI(false);
    }
    
    void Init()
    {
        TurnOnUI(false);
        SetWave();
    }

    void TurnOnUI(bool isOn)
    {
        waveBar.gameObject.SetActive(isOn);
        playerExpBar.gameObject.SetActive(isOn);
    }

    void SetWave()
    {
        waveBar.gameObject.SetActive(true);
        waveBar.onFinishFill += SetExp;

        waveBar.SetBar(DungeonManager.instance.curDungeon.curWave - 1, DungeonManager.instance.curDungeon.maxWave);
    }

    void SetExp()
    {   
        waveBar.onFinishFill -= SetExp; 

        playerExpBar.gameObject.SetActive(true);
        playerExpBar.onFinishFill += SetHeroExp;

        playerExpBar.SetBar(
            PlayerManager.instance.curExp, 
            PlayerManager.instance.maxExp, 
            beforeExp / PlayerManager.instance.maxExp);

        // playerExpBar.SetBar(beforeExp, PlayerManager.instance.curExp)

        // playerExpBar.SetBar(PlayerManager.instance.curExp, 
        //     PlayerManager.instance.curExp);// 
    }

    void SetHeroExp()
    {
        playerExpBar.onFinishFill -= SetExp; 
        
    }

    void SetItem()
    {

    }
    
}
