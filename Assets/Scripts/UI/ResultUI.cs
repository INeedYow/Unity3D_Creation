using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [Header("bar")]
    public Result_WaveBar waveBar;
    public Result_ExpBar playerExpBar;
    //[Header("Hero Exp")]
    //public HeroExpTray
    public GameObject heroExpTray;  // temp
    [Header("Item")]
    public Result_ItemTray itemTray;
    public Button exitBtn;

    private void OnEnable() { Init(); }
    
    void Init()
    {
        waveBar.gameObject.SetActive(true);
        playerExpBar.gameObject.SetActive(false);
        heroExpTray.gameObject.SetActive(false);
        itemTray.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
    }
        

    // public void SetWave()
    // {
    //     waveBar.SetBar(DungeonManager.instance.curDungeon.curWave - 1, DungeonManager.instance.curDungeon.maxWave);
    // }

    // void SetExp()
    // {   
    //     waveBar.onFinishFill -= SetExp; 

    //     playerExpBar.gameObject.SetActive(true);
    //     playerExpBar.onFinishFill += SetHeroExp;

    //     playerExpBar.SetBar(
    //         PlayerManager.instance.curExp, 
    //         PlayerManager.instance.maxExp, 
    //         beforeExp / PlayerManager.instance.maxExp);

    //     // playerExpBar.SetBar(beforeExp, PlayerManager.instance.curExp)

    //     // playerExpBar.SetBar(PlayerManager.instance.curExp, 
    //     //     PlayerManager.instance.curExp);// 
    // }

    // void SetHeroExp()
    // {
    //     playerExpBar.onFinishFill -= SetExp; 
        
    // }

    // void SetItem()
    // {

    // }
    
}
