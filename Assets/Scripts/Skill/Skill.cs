using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;

    [HideInInspector] public Character  owner;
    [HideInInspector] public int ID;                    // 몇 번째 스킬인지(1~4)
    public SkillCommand     skillcommand;               // 스킬 사용 입력받았을 때 명령(즉시 사용, 타겟에게 이동 후 사용 등)
    [HideInInspector] public Character target;

    public SkillObject skillObj;

    private void Awake() { SetCommand(); }

    void SetCommand(){ 
        if (data.skillRange > 0f)   { skillcommand = new SkillCommand_Targeting(this); }
        else                        { skillcommand = new SkillCommand_Instant(this); }
    }
    public bool IsReady() { return Time.time >= skillcommand.lastSkillTime + data.cooldown; }

    public void Use(Character target) { 
        this.target = target;
        skillcommand.Use();
    }

    public void Init(Character character, int id) { 
        owner = character; 
        ID = id; 
        owner.onDead += FinishSKill;        // 안 해주면 스킬 도중에 죽고 재활용할 때 고장남
    }

    public void QuitSkill()
    {
        owner.QuitSKill(ID);
        skillObj.gameObject.SetActive(false);

        DungeonManager.instance.onWaveEnd -= QuitSkill;
    }
 
    public void FinishSKill()
    {        
        if (!skillcommand.isUsing) return;        

        skillcommand.lastSkillTime = Time.time;         
        skillcommand.isUsing = false;

        DungeonManager.instance.onWaveEnd -= QuitSkill;
    }

    public void EffectSkill(){  // 실제 스킬 효과부
        skillObj.gameObject.SetActive(true);
    }

}