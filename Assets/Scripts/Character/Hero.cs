using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Hero : Character
{
    public UnityAction<int> onLevelUp;

    public enum EClass { Knight, Archer, Angel, Necromancer, Bard, Templar,  }  // magician / wizard / sorcerer(sorceress)
    [Header("GFX")]
    public Hero_GFX heroGFX;
    public Dummy dummy;
    [Header("Class")]
    public EClass eClass;
    [HideInInspector] public bool isJoin;
    
    [Header("Level")]
    public int level = 1;
    [HideInInspector] public float maxExp = 100;
    float _curExp;  
    public float curExp {
        get { return _curExp; } 
        set { 
            _curExp = value ;
            if (_curExp >= maxExp)
            {
                level++;
                _curExp -= maxExp;
                maxExp += 50f; 
                StatUp();
                onLevelUp?.Invoke(level);
            }
        }
    }

    [Header("Equipment")]
    public WeaponItemData       weaponData;
    public ArmorItemData        armorData;
    public AccessoryItemData    accessoryData;

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

    void StatUp(){
        switch (eClass)
        {
            case EClass.Knight      : 
            case EClass.Templar     : maxHp += 15;  break;
            
            case EClass.Angel       :
            case EClass.Bard        : maxHp += 12;  break;

            case EClass.Archer      : 
            case EClass.Necromancer : maxHp += 10;  break;

        }
        minDamage += 1;
        maxDamage += 1;
        magicDamage += 1;
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
        ResetBuffs();
        onDeadGetThis?.Invoke(this);
        onDead?.Invoke();
        heroGFX.gameObject.SetActive(false);
    }

    public void ResetPos()
    {   
        transform.position = dummy.placedBlock.beginPos + DungeonManager.instance.curDungeon.beginTf.position; 
    }

    // 파티 관련

    public void Join()
    {
        if (isJoin) return;
        PartyManager.instance.Join(this);
        isJoin = true;
    }

    public void Leave()
    {
        if (!isJoin) return;
        PartyManager.instance.Leave(this);
        isJoin = false;
        dummy.gameObject.SetActive(false);
    }

    // 장비 관련
    
    public void Equip(WeaponItemData itemData)
    {
        if (weaponData != null) { weaponData.UnEquip(); }
        weaponData = itemData;

        minDamage += itemData.minDamage;
        maxDamage += itemData.maxDamage;
        magicDamage += itemData.magicDamage;

        HeroManager.instance.heroInfoUI.RenewUI(this);
    }

    public void UnEquipWeapon()
    {
        if (weaponData == null) return;

        minDamage -= weaponData.minDamage;
        maxDamage -= weaponData.maxDamage;
        magicDamage -= weaponData.magicDamage;

        weaponData = null;
        HeroManager.instance.heroInfoUI.RenewUI(this);
    }

    public void Equip(ArmorItemData itemData)
    {
        if (armorData != null) { armorData.UnEquip(); }
        armorData = itemData;

        armorRate += itemData.armor;
        magicArmorRate += itemData.magicArmor;
        maxHp += itemData.Hp;
        curHp = maxHp;

        HeroManager.instance.heroInfoUI.RenewUI(this);
    }

    public void UnEquipArmor()
    {
        if (armorData == null) return;

        armorRate -= armorData.armor;
        magicArmorRate -= armorData.magicArmor;
        maxHp -= armorData.Hp;
        curHp = maxHp;

        armorData = null;
        HeroManager.instance.heroInfoUI.RenewUI(this);
    }

    public void Equip(AccessoryItemData itemData)
    {
        if (accessoryData != null) { accessoryData.UnEquip(); }
        accessoryData = itemData;

        accessoryData.ability.OnEquip();
        onAttackGetDamage       += accessoryData.ability.OnAttack;
        onDamagedGetAttacker    += accessoryData.ability.OnDamagedGetAttacker;
        onKill                  += accessoryData.ability.OnKill;

        HeroManager.instance.heroInfoUI.RenewUI(this);
    }

    public void UnEquipAccessory()
    {
        if (accessoryData == null) return;

        onAttackGetDamage       -= accessoryData.ability.OnAttack;
        onDamagedGetAttacker    -= accessoryData.ability.OnDamagedGetAttacker;
        onKill                  -= accessoryData.ability.OnKill;
        accessoryData.ability.OnUnEquip();

        accessoryData = null;
        HeroManager.instance.heroInfoUI.RenewUI(this);
    }
}
