using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleInfoType { Ally_Damage, Ally_Magic, Ally_Heal, Enemy_Damage, Enemy_Magic, Enemy_Heal, Etc, }
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [Header("Game UI")]
    public GameObject worldCanvas;
    public Text prfText;
    BattleInfoText m_battleInfotext;
    
    [Header("Player Info")]
    public int LV = 1;
    public int maxLV = 21;
    int _curExp;
    public int curExp{
        get { return _curExp; }
        set {
            _curExp += value;
            if (_curExp >= maxExp) { LevelUp(); }
        }
    }
    public int maxExp = 200;
    public int gold;

    public CameraMove cam;
    public CubePlanet cube;

    private void Awake() { 
        instance = this; 
        Application.targetFrameRate = 30;   // 프레임
    }

    void LevelUp(){
        LV++;
        curExp -= maxExp;
        maxExp += 50;
        
        if (LV % 3 == 1) PartyManager.instance.maxCount++;
    }

    public void ShowBattleInfoText(BattleInfoType infoType, Vector3 position, float value){
        // m_text = Instantiate(prfText, worldCanvas.transform, true);
        // m_text.transform.position = position;
        // m_text.text = value.ToString();
        // m_text.color = GetColorByInfoType(infoType);
        // Destroy(m_text, 2f);

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
            //
            default:                            return Color.black;
        }
    }
}