using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;
    
    //[SerializeField]
    public Character owner;
    public int ID;                  // 몇 번째 스킬인지(0~3)
    public float lastSkillTime;
    public SkillCommand command;
    
    private void Awake() { SetCommand(); }
    void SetCommand(){ 
        if (data.skillRange > 0f)   { command = new SkillCommand_Targeting(this); }
        else                        { command = new SkillCommand_Instant(this); }
    }
    public bool Use() { return command.Use(); }

    public void Init(Character character, int id) { 
        owner = character; 
        ID = id; 
    }

    public void FinishSKill(){          Debug.Log(lastSkillTime + " before ");
        lastSkillTime = Time.time;      Debug.Log(lastSkillTime + " / " + Time.time);
        command.isUsing = false;
        Debug.Log("isUsing : " + command.isUsing);
    }

}