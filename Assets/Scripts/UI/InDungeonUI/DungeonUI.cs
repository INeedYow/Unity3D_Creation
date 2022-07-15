using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DungeonUI : MonoBehaviour
{
    public HeroBattleInfoUI heroBattleInfoUI;
    public DungeonInfoUI dungeonInfoUI;
    public GameObject menuButton;
    public GameObject menuUI;
    public Image speedIcon;

    public void Init(Dungeon dungeon){
        dungeonInfoUI.Init(dungeon);
    }

    public void SetCount(int count){
        dungeonInfoUI.SetCount(count);
    }

    public void SetCurWave(int wave){
        dungeonInfoUI.SetCurWave(wave);
    }

    public void MenuOn()
    {
        Time.timeScale = 0f;

        menuUI.SetActive(true);
        PlayerManager.instance.runeSkillUI.LockSkillInput();
    }

    public void MenuOff()
    {
        Time.timeScale = DungeonManager.instance.gameSpeed;

        menuUI.SetActive(false);
        PlayerManager.instance.runeSkillUI.UnLockSkillInput();
    }

    public void Exit()
    {
        menuUI.SetActive(false);
        DungeonManager.instance.BattleEnd();
        Time.timeScale = DungeonManager.instance.gameSpeed;
    }


    public void ToggleGameSpeed()
    {
        DungeonManager.instance.ToggleGameSpeed();

        if (DungeonManager.instance.isSpeed)
        {
            speedIcon.color = Color.yellow;
        }
        else{
            speedIcon.color = Color.white;
        }
    }
}
