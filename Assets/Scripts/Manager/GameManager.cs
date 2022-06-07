using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleInfoType { Hero_damage, Hero_magic, Hero_heal, Monster_damage, Monster_magic, Monster_heal, Etc, }
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int clearLevel;  // TODO

    [Header("Cube Planet")]
    public CubePlanet cubePlanet;

    [Header("Dummy tools")]
    public GameObject dummyPlane;
    public DummyCursor cursor;
    
    
    public GameObject worldCanvas;
    public GameObject heroLeaveBox;
    public GameObject monsterPlanet;
    public GameObject storeObjects;
    public GameObject floatingBlocks;

    [Header("prfBuffs")]
    public Buff[] prfBUffs = new Buff[(int)EBuff.Size];   // 공, 마공, 방, 마방, 공속

    [Header("Etc")]
    public Vector3 focusedScale = new Vector3 (0.5f, 0.5f, 0.5f);
    public bool isLockFocus;
    BattleInfoText m_battleInfotext;
    
    //

    // public int maxLV    { get { return 25; } }
    // public int LV       { get; private set; }
    // public int maxExp   { get; private set; }
    // int _curExp;
    // public int curExp{
    //     get { return _curExp; }
    //     set {
    //         if (LV == maxLV) return;
    //         _curExp = value;
    //         if (_curExp >= maxExp) { LevelUp(); } 
    //     }
    // }
    // public int gold         { get; private set; }
    public bool isMouseOnLeaveArea;

    private void Awake() { 
        instance = this; 
        Application.targetFrameRate = 60;   // 프레임
    }


    public void ShowBattleInfoText(BattleInfoType infoType, Vector3 position, float value)
    {
        m_battleInfotext = ObjectPool.instance.GetInfoText();
        m_battleInfotext.transform.SetParent(worldCanvas.transform);
        m_battleInfotext.transform.position = position;
        m_battleInfotext.infoText.text = Mathf.RoundToInt(value).ToString(); 
        m_battleInfotext.myColor = GetColorByInfoType(infoType);
    }
    
    public void ShowDodgeText(Vector3 position)
    {
        m_battleInfotext = ObjectPool.instance.GetInfoText();
        m_battleInfotext.transform.SetParent(worldCanvas.transform);
        m_battleInfotext.transform.position = position;
        m_battleInfotext.infoText.text = "dodge";
        m_battleInfotext.myColor = GetColorByInfoType(BattleInfoType.Etc);
    }

    Color GetColorByInfoType(BattleInfoType infoType){
        switch (infoType)
        {
            case BattleInfoType.Hero_damage:        return Color.red;
            case BattleInfoType.Hero_heal:          return Color.green;
            case BattleInfoType.Hero_magic:         return Color.blue;

            case BattleInfoType.Monster_damage:     return Color.magenta;
            case BattleInfoType.Monster_heal:       return Color.yellow;
            case BattleInfoType.Monster_magic:      return Color.cyan;
            //
            default:                                return Color.black;
        }
    }

    public void OnDummyDrag(){
        dummyPlane.SetActive(true);
        cursor.gameObject.SetActive(true);
        heroLeaveBox.gameObject.SetActive(true);
    }

    public void OnDummyDrop(){
        dummyPlane.SetActive(false);
        cursor.gameObject.SetActive(false);
        heroLeaveBox.gameObject.SetActive(false);
        isMouseOnLeaveArea = false;
    }

    public void MouseOnLeaveArea(){
        if (HeroManager.instance.IsFull()) return;
        isMouseOnLeaveArea = true;
    }

    public void MouseOffLeaveArea(){
        isMouseOnLeaveArea = false;
    }

    public void EnterDungeon(bool isEnter){
        cubePlanet.gameObject.SetActive(!isEnter);
        PlayerManager.instance.playerInfoUI.gameObject.SetActive(!isEnter);
        
        if (isEnter)
            CameraManager.instance.SetDungeonView();
        else
            CameraManager.instance.SetPlanetView();
    }

    public void EnterTree(bool isEnter)
    {
        //storeObjects.SetActive(isEnter);
        //monsterPlanet.SetActive(isEnter);
        floatingBlocks.SetActive(!isEnter);
    }

}