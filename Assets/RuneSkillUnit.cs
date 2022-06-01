using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RuneSkillUnit : MonoBehaviour
{
    public int maxCooldown;
    public int curCooldown;
    public RuneSkillCursor cursor;
    
    [Header("UI")]
    public Image skillIconImg;
    public Image cooldownImg;

    [Header("KeyCode")]
    public KeyCode keyCode;

    private void Start() {
        DungeonManager.instance.onWaveEnd += EndWave;
    }

    public void SetInfo(SkillRuneData data)
    {   
        if (data == null)
        {
            gameObject.SetActive(false);
            return;
        }

        curCooldown = 0;
        maxCooldown = data.cooldown;

        cooldownImg.sprite = data.icon;
        RenewCooldown();
    }

    public void SetCursor(RuneSkillCursor skillCursor)
    {
        if (cursor != skillCursor)
        {
            if (cursor != null)
            {   // 이전 등록된 거 있으면 제거
                cursor.onWorks -= SetCooldown;
            }
            cursor = skillCursor;
            cursor.onWorks += SetCooldown;
        }

        cursor.skillObj.gameObject.SetActive(false);
        cursor.gameObject.SetActive(false);
    }

    void EndWave() { 
        if (curCooldown > 0) 
        {
            curCooldown--; 
            RenewCooldown();
        }
    }

    void RenewCooldown()
    { cooldownImg.fillAmount = 1 - (float)curCooldown / (float)maxCooldown; }

    void SetCooldown()
    {
        curCooldown = maxCooldown;
        RenewCooldown();
    }


    private void Update() 
    {  
        if (Input.GetKeyDown(keyCode))
        {   
            if (curCooldown > 0)
            {   // 쿨타임
                Debug.Log("cursor cooldown : " + curCooldown);
            }
            else{   Debug.Log("cursor active");
                cursor.gameObject.SetActive(true);
            }
        }
    }
}
