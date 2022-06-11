using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : Character
{
    [Header("GFX")]
    public Monster_GFX monsGFX;
    [Header("Macros")]
    public GameObject macrosParent;
    new protected void Awake() {
        base.Awake();
        InitMonster();
    }

    void InitMonster(){
        eGroup = EGroup.Monster;
        monsGFX.monster = this;
    }

    public static Monster Create(EMonster eMonster){
        Monster monster = null;
        monster = Instantiate(ObjectPool.instance.prfMonsters[(int)eMonster]);
        switch (eMonster)
        {
            case EMonster.RedSlime: 
            case EMonster.BlueSlime:
            case EMonster.Plant:       

            { monster.attackCommand = new NormalAttackCommand(monster); break; }


            case EMonster.Spore:            monster.attackCommand = new NormalAttackCommand(monster);
            {   
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfSporeSkill, monster.transform);
                monster.skills[0].Init(monster, 1);     // Init(스킬 owner , 몇 번째 스킬(1 ~ 4))
                break;
            }

            case EMonster.Pollen:           monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfPollenSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }

            case EMonster.OddPlant:        monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfPlantSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }

            case EMonster.PlantChewer:     monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[2];

                for (int i = 0; i < DungeonManager.instance.prfChewerSkills.Length; i++)
                {
                    monster.skills[i] = Instantiate(DungeonManager.instance.prfChewerSkills[i], monster.transform);
                    monster.skills[i].Init(monster, i + 1);
                }
                break;
            }
            
            case EMonster.Skeleton:  
            case EMonster.BraveSkeleton :   monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfSkeletonSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }

            case EMonster.EvilMage :   monster.attackCommand = new ProjectileAttackCommand(monster, EProjectile.EvilMage_0);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfEvilMageSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }

            case EMonster.Golem_Earth : monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfGolemEarthSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }

            case EMonster.Golem_Ice : monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfGolemIceSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }

            case EMonster.Golem_Fire : monster.attackCommand = new ProjectileAttackCommand(monster, EProjectile.Golem_Fire_0);
            {
                monster.skills = new Skill[1];
                monster.skills[0] = Instantiate(DungeonManager.instance.prfGolemFireSkill, monster.transform);
                monster.skills[0].Init(monster, 1);
                break;
            }

            case EMonster.Golem_Blue : monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[2];

                for (int i = 0; i < DungeonManager.instance.prfGolemBlueSkills.Length; i++)
                {
                    monster.skills[i] = Instantiate(DungeonManager.instance.prfGolemBlueSkills[i], monster.transform);
                    monster.skills[i].Init(monster, i + 1);
                }
                break;
            }

            case EMonster.Golem_Red : monster.attackCommand = new NormalAttackCommand(monster);
            {
                monster.skills = new Skill[2];

                for (int i = 0; i < DungeonManager.instance.prfGolemRedSkills.Length; i++)
                {
                    monster.skills[i] = Instantiate(DungeonManager.instance.prfGolemRedSkills[i], monster.transform);
                    monster.skills[i].Init(monster, i + 1);
                }
                break;
            }

        }


        // Macro
        monster.SetMacro();
        return monster;
    }

    public void SetMacro(){ // 몬스터 하위로 넣어준 매크로로 초기화
        conditionMacros = macrosParent.GetComponentsInChildren<ConditionMacro>();
        foreach (ConditionMacro macro in conditionMacros)
        { macro.owner = this; }

        actionMacros = macrosParent.GetComponentsInChildren<ActionMacro>();
        foreach (ActionMacro macro in actionMacros)
        { macro.owner = this; }
        
        monsGFX.repeat = conditionMacros.Length;
    }
   
    protected override void ShowDamageText(float damage, bool isMagic = false, bool isHeal = false)
    {   
        if (isHeal) {
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Monster_heal, HpBarTF.position + Vector3.up * 3f, damage );
        }
        else if (isMagic){  
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Hero_magic, HpBarTF.position + Vector3.up * 3f, damage );
        }
        else{
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Hero_damage, HpBarTF.position + Vector3.up * 3f, damage );
        }
    }

    public override void Death()
    {
        isDead = true;
        onDeadGetThis?.Invoke(this);
        onDead?.Invoke();

        if (provoker)
        {
            CancelInvoke("FinishProvoke");
            provoker = null;
        }

        if (isStasis) 
        { 
            SetStasis(false); 
        }
        
        DungeonManager.instance.onMonsterDead?.Invoke();
        monsGFX.gameObject.SetActive(false);
    }

    public override void Revive(float rateHp)
    {
        isDead = false;
        
        rateHp = Mathf.Clamp(0.01f * rateHp, 0.1f, 1f);

        curHp = rateHp * maxHp;

        //Debug.Log("mon Revive // ratio : " + rateHp);
        
        monsGFX.gameObject.SetActive(true);
        isStop = false; 

        onHpChange?.Invoke();
        DungeonManager.instance.onChangeAnyHP?.Invoke();
        DungeonManager.instance.onSomeoneAdd?.Invoke();
        DungeonManager.instance.ReviveMonster();
    }

    private void OnMouseDown() { 
        if (isDead) return;
        DungeonManager.instance.ShowMonInfoUI(this); 
    }
}