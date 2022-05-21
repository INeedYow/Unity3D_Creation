using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;
    
    //[SerializeField]
    public Character        owner;
    public int              ID;                  // 몇 번째 스킬인지
    public SkillCommand     skillcommand;
    public EffectCommand    effCommand;

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