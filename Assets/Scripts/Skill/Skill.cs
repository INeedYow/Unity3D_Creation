using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{
    public Character owner;
    public SkillData data;
    public float lastSkillTime;
    public bool isUsing;
    //public SkillCommand command;

    public bool Use()
    {
        if (isUsing) return true;
        if (Time.time < lastSkillTime + data.cooldown) return false;

        // TODO anim 스킬 종료 시 lastSkillTime = Time.time;, isUsing = false; 해주기
        isUsing = true;
        owner.anim.SetTrigger(string.Format("Skill {0}", data.ID));
        Debug.Log(data.ID);
        return true;
    }

    public void FinishSKill(){
        lastSkillTime = Time.time;
        isUsing = false;
    }

}