using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleInfoType { Ally_Damage, Ally_Magic, Ally_Heal, Enemy_Damage, Enemy_Magic, Enemy_Heal, Etc, }
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [Header("Cube Planet")]
    public CubePlanet cubePlanet;
    [Header("Transparent Plane")]
    public GameObject dummyPlane;
    public DummyCursor cursor;
    
    [Header("Game UI")]
    public PlayerInfoUI playerInfoUI;
    public GameObject worldCanvas;

    BattleInfoText m_battleInfotext;
    
    //
    public int maxLV    { get { return 25; } }
    public int LV       { get; private set; }
    public int maxExp   { get; private set; }
    int _curExp;
    public int curExp{
        get { return _curExp; }
        set {
            if (LV == maxLV) return;
            _curExp = value;
            if (_curExp >= maxExp) { LevelUp(); } 
        }
    }
    public int gold         { get; private set; }
    public int skillPoint   { get; private set; }

    public CameraMove cam;
    public CubePlanet cube;

    private void Awake() { 
        instance = this; 
        Application.targetFrameRate = 50;   // 프레임
        LV = 1;
        maxExp = 150;
    }

    private void Start() { InitUI(); }

    void InitUI(){
        playerInfoUI.RenewCurParty(PartyManager.instance.heroParty.Count);
        playerInfoUI.RenewMaxParty(PartyManager.instance.maxCount);
        playerInfoUI.RenewExp(curExp, maxExp);
        playerInfoUI.RenewGold(gold);
        playerInfoUI.RenewLV(LV);
    }
    
    void LevelUp(){ 
        LV++;                       
        curExp -= maxExp;          
        maxExp += 75;
        skillPoint++;   //               
        playerInfoUI.RenewLV(LV);
        playerInfoUI.RenewExp(curExp, maxExp);
        
        if (LV % 5 == 0){
            PartyManager.instance.maxCount++;
            playerInfoUI.RenewMaxParty(PartyManager.instance.maxCount);
        }
        if (LV == maxLV) curExp = maxExp;
    }

    public void ShowBattleInfoText(BattleInfoType infoType, Vector3 position, float value)
    {
        m_battleInfotext = ObjectPool.instance.GetInfoText();
        m_battleInfotext.transform.SetParent(worldCanvas.transform);
        m_battleInfotext.transform.position = position;
        m_battleInfotext.infoText.text = value.ToString();
        m_battleInfotext.myColor = GetColorByInfoType(infoType);
    }

    Color GetColorByInfoType(BattleInfoType infoType){
        switch (infoType)
        {
            case BattleInfoType.Ally_Damage:    return Color.red;
            case BattleInfoType.Ally_Heal:      return Color.green;
            case BattleInfoType.Enemy_Damage:   return Color.magenta;
            //
            default:                            return Color.black;
        }
    }

    public void OnDummyDrag(){
        dummyPlane.SetActive(true);
        cursor.gameObject.SetActive(true);
    }

    public void OnDummyDrop(){
        dummyPlane.SetActive(false);
        cursor.gameObject.SetActive(false);
    }

    //////////////////////////////

    public void AddGold(int amount){    
        gold += amount;
        playerInfoUI.RenewGold(gold);
    }

    public void AddExp(int amount){ 
        curExp += amount;   
        playerInfoUI.RenewExp(curExp, maxExp);
    }

    public void RenewCurParty(int value){
        playerInfoUI.RenewCurParty(value);
    }


}