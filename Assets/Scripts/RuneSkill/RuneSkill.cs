using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneSkill : MonoBehaviour
{   // 스킬들 누군가 가지고 있어야
    public UnityAction onChangeCooldown;

    [Range(1, 6)]                       // 1 / wave , 3 / wave, 5 / wave , 1 / DG
    public int cooldown;
    public int curCooldown;             // wave당

    public RuneSkillCursor cursor;      // 룬에 따라서 커서 다르게 넣어줄 생각

    private void Start() {
        DungeonManager.instance.onWaveEnd += EndWave;
    }

    void EndWave() { 
        if (curCooldown > 0) curCooldown--; 
        onChangeCooldown?.Invoke();
    }

    public void Use()
    {
        if (curCooldown > 0)
        {
            // 쿨타임
        }
        else{
            cursor.gameObject.SetActive(true);
            curCooldown = cooldown;
        }
    }
}
