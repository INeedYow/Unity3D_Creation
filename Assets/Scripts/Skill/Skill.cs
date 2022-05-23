using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillData data;

    [HideInInspector] public Character  owner;
    [HideInInspector] public int ID;                    // 몇 번째 스킬인지
    public SkillCommand     skillcommand;               // 스킬 사용 입력받았을 때 명령(즉시 사용, 타겟에게 이동 후 사용 등)
    //public EffectCommand    effCommand;                 // 스킬 발동 타이밍에 처리(애니메이션에서 GFX 통해서 호출)

    public SkillObject skillObj;

    private void Awake() { SetCommand(); }
    void SetCommand(){ 
        if (data.skillRange > 0f)   { skillcommand = new SkillCommand_Targeting(this); }
        else                        { skillcommand = new SkillCommand_Instant(this); }
        //
        //effCommand = new EffectCommand_SingleAttack(this);
    }
    public bool Use() { return skillcommand.Use(); }

    public void Init(Character character, int id) { 
        owner = character; 
        ID = id; 

        // 스킬 할당할 때 영웅이면 레벨업 이벤트에 함수 등록해서 요구 레벨에 스킬 on되게 하려고
        Hero hero = owner.GetComponent<Hero>();
        if (hero != null)
            hero.onLevelUp += CheckLevel;
    }

    public void CheckLevel(int lv)
    {
        if (lv == data.requireLevel)
        {
            // TODO : 해당 영웅 스킬 매크로 추가
        }
    }
 
    public void FinishSKill(){                         
        skillcommand.lastSkillTime = Time.time;         
        skillcommand.isUsing = false;
    }

    public void EffectSkill(){  // 실제 스킬 효과부
        // m_obj = ObjectPool.instance.GetSkillObject((int)eObj);
        // m_obj.skill = this;
        // m_obj.Works();

        skillObj.gameObject.SetActive(true);
        
    }

}