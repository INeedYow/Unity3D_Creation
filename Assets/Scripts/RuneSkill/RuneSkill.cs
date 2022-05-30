using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneSkill : MonoBehaviour
{
    public UnityAction onChangeCooldown;

    [Range(1, 6)]   // 1 / wave , 3 / wave, 5 / wave , 1 / DG
    public int cooldown;
    public int lastCooldown;    // wave당

    public RuneSkillCursor cursor;  // 룬에 따라서 커서 다르게 넣어줄 생각

    private void Start() {
        DungeonManager.instance.onWaveEnd += EndWave;
    }

    void EndWave() { 
        if (lastCooldown > 0) lastCooldown--; 
        onChangeCooldown?.Invoke();
    }

    public void Use()
    {
        if (lastCooldown > 0)
        {
            // 쿨타임 표시
        }
        else{
            cursor.gameObject.SetActive(true);
        }
    }
}
