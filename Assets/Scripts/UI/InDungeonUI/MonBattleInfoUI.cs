using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonBattleInfoUI : MonoBehaviour
{
    public Monster mon;
    public Text monName;
    public MonBattleInfoUnit[] macroInfoUnits;
    public MonBattleInfoUnit[] skillInfoUnits;

    void OnEnable() { DungeonManager.instance.onWaveEnd += Hide; }

    public void RenewUI(Monster monster){
        mon = monster;
        SetText();
    } 

    void SetText()
    {
        monName.text = mon.name;

        for (int i = 0; i < macroInfoUnits.Length; i++)
        {
            if (i < mon.conditionMacros.Length)
            {   // 몬스터가 가진 매크로 만큼만 활성화하고 셋팅
                macroInfoUnits[i].gameObject.SetActive(true);
                macroInfoUnits[i].up.text = mon.conditionMacros[i].data.desc;
                macroInfoUnits[i].down.text = mon.actionMacros[i].data.desc;
            }
            else{ // 나머지 비활성화
                macroInfoUnits[i].gameObject.SetActive(false);
            }
            
        }

        for (int i = 0; i < skillInfoUnits.Length; i++)
        {   // 스킬
            if (i < mon.skills.Length)
            {
                skillInfoUnits[i].gameObject.SetActive(true);
                skillInfoUnits[i].up.text = mon.skills[i].data.skillName;
                skillInfoUnits[i].down.text = mon.skills[i].data.description;
            }
            else{
                skillInfoUnits[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void Hide() { gameObject.SetActive (false); }
}
