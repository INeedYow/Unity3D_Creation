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
    

    private void OnEnable() { SetWave(); }
    private void OnDisable() {
        // TODO ui들 active false;
    }
    void SetWave()
    {
        waveBar.gameObject.SetActive(true);
        waveBar.onFinishFill += SetExp;
        waveBar.SetBar(DungeonManager.instance.curDungeon.curWave - 1, DungeonManager.instance.curDungeon.maxWave);
    }

    void SetExp()
    {   // 값을 미리 받아놓고 해야 하나
        waveBar.onFinishFill -= SetExp; Debug.Log("SetEXP()");
        // playerExpBar.onFinishFill += SetExp;
        // playerExpBar.SetBar(PlayerManager.instance.curExp, 
        //     PlayerManager.instance.curExp);// 
    }

    void SetHeroExp()
    {

    }

    void SetItem()
    {

    }
    
}
