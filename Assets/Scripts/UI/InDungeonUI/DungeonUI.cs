using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonUI : MonoBehaviour
{
    public HeroBattleInfoUI heroBattleInfoUI;
    public DungeonInfoUI dungeonInfoUI;
    public MiniMacroInfoUI miniMacroUI;

    public void Init(Dungeon dungeon){
        dungeonInfoUI.Init(dungeon);
    }

    public void SetCount(int count){
        dungeonInfoUI.SetCount(count);
    }

    public void SetCurWave(int wave){
        dungeonInfoUI.SetCurWave(wave);
    }

    // public void SetMiniMacroUI(bool isOn){
    //     miniMacroUI.gameObject.SetActive(isOn);
    // }
}
