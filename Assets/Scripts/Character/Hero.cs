using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Hero : Character
{
    public UnityAction<int> onLevelUp;

    public enum EClass { Knight, Archer, Angel, Necromancer, Bard, Templar, Magician,  }
    [Header("GFX")]
    public Hero_GFX heroGFX;
    public Dummy dummy;
    [Header("Class")]
    public EClass eClass;
    [HideInInspector] public bool isJoin;
    
    [Header("Level")]
    public int level = 1;
    public float maxExp = 100;
    float _curExp;  //TODO
    public float curExp {
        get { return _curExp; } 
        set { 
            _curExp = value ;
            if (_curExp >= maxExp)
            {
                level++;
                _curExp -= maxExp;
                maxExp += 100f; 
                onLevelUp?.Invoke(level);
            }
        }
    }

    new protected void Awake() {
        base.Awake();
        InitHero();
    }

    void InitHero(){
        eGroup = EGroup.Hero;
        heroGFX.hero = this;
        dummy.owner = this;
        heroGFX.gameObject.SetActive(false);
        dummy.gameObject.SetActive(false);
        skills = new Skill[4];
        // 매크로 배열 초기화
        conditionMacros = new ConditionMacro[MacroManager.instance.maxMacroCount];
        actionMacros = new ActionMacro[MacroManager.instance.maxMacroCount];
        // Attack Command
        switch(eClass){
            case EClass.Knight: attackCommand = new NormalAttackCommand(this); break;
            case EClass.Archer: attackCommand = new ProjectileAttackCommand(this, EProjectile.ArcherArrow); break;
            case EClass.Angel:  attackCommand = new ProjectileAttackCommand(this, EProjectile.YellowMarble); break;
        }
    }

    protected override void ShowDamageText(float damage, bool isMagic = false, bool isHeal = false)
    {   
        if (isHeal) {
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Hero_heal, transform.position + Vector3.up * 5f, damage );
        }
        else if (isMagic){  // 피해 받았을 때 동작하기 때문에 InfoType enemy
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Monster_magic, transform.position + Vector3.up * 5f, damage );
        }
        else{
            GameManager.instance.ShowBattleInfoText( 
                BattleInfoType.Monster_damage, transform.position + Vector3.up * 5f, damage );
        }
    }

    public override void Death()
    {
        isDead = true;
        heroGFX.gameObject.SetActive(false);
        onDeath?.Invoke(this);
    }

    public void ResetPos()
    {   
        transform.position = dummy.placedBlock.beginPos + DungeonManager.instance.curDungeon.beginTf.position; 
        target = null;
    }

    public void Join(){
        if (isJoin) return;
        PartyManager.instance.Join(this);
        isJoin = true;
    }

    public void Leave(){
        if (!isJoin) return;
        PartyManager.instance.Leave(this);
        isJoin = false;
        dummy.gameObject.SetActive(false);
    }
}
