using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RuneSkillUnit : MonoBehaviour
{
    [Header("Cooldown")]
    public int maxCooldown;
    public int curCooldown;
    
    [Header("UI")]
    public Image skillIconImg;
    public Image cooldownImg;

    public RuneSkillCursor cursor;
    public KeyCode keyCode;

    private void Start() {
        DungeonManager.instance.onWaveEnd += EndWave;
    }

    void EndWave() { 
        if (curCooldown > 0) 
        {
            curCooldown--; 
            RenewCooldown();
        }
    }

    void RenewCooldown()
    { cooldownImg.fillAmount = (float)curCooldown / (float)maxCooldown; }


    private void Update() {
        if (Input.GetKeyDown(keyCode))
        {
            if (curCooldown > 0)
            {   // 쿨타임
                
            }
            else{
                cursor.gameObject.SetActive(true);
                curCooldown = maxCooldown;
                //RenewCooldown(); // 취소한 경우는 X 이벤트로 받거나 호출
            }
        }
    }
}
