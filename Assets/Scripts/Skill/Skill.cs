using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;

    [HideInInspector] public Character  owner;
    [HideInInspector] public int ID;                    // 몇 번째 스킬인지
    public SkillCommand     skillcommand;               // 스킬 사용 입력받았을 때 명령(즉시 사용, 타겟에게 이동 후 사용 등)
    public EffectCommand    effCommand;                 // 스킬 발동 타이밍에 처리(애니메이션에서 GFX 통해서 호출)

    private void Awake() { SetCommand(); }
    void SetCommand(){ 
        if (data.skillRange > 0f)   { skillcommand = new SkillCommand_Targeting(this); }
        else                        { skillcommand = new SkillCommand_Instant(this); }
        //
        effCommand = new EffectCommand_SingleAttack(this);
    }
    public bool Use() { return skillcommand.Use(); }

    public void Init(Character character, int id) { 
        owner = character; 
        ID = id; 
    }

    public void FinishSKill(){                         
        skillcommand.lastSkillTime = Time.time;         
        skillcommand.isUsing = false;
    }

    public void EffectSkill(){
        effCommand.Works();
    }

}