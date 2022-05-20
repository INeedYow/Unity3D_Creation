﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 전투, 비전투 구분하여 관리하는 법.?
public class Hero : Character
{
    public enum EClass { Knight, Archer, }
    [Header("GFX")]
    public Hero_GFX heroGFX;
    public Dummy dummy;
    [Header("Class")]
    public EClass eClass;   
    [HideInInspector] public bool isJoin;
    
    [Header("Level")]
    public int level = 1;
    public float maxExp;
    float _curExp;  //TODO
    public float curExp {
        get { return _curExp; } 
        set { 
            _curExp = value ;
            if (_curExp >= maxExp)
            {
                level++;
                _curExp -= maxExp;
                maxExp += 100f; // 임시로
            }
        }
    }
    // [Header("Skill")]
    // public Skill[] skills;

    new protected void Awake() {
        base.Awake();
        InitHero();
    }

    void InitHero(){
        heroGFX.hero = this;
        dummy.owner = this;
        heroGFX.gameObject.SetActive(false);
        dummy.gameObject.SetActive(false);
        // Attack Command
        switch(eClass){
            case EClass.Knight: attackCommand = new NormalAttackCommand(this); break;
            case EClass.Archer: attackCommand = new ProjectileAttackCommand(this, EProjectile.Arrow); break;
        }
    }

    public override void Death()
    {
        base.Death();
        heroGFX.gameObject.SetActive(false);
    }

    // public bool IsTargetInRange()
    // {   //Debug.Log("isTargetInRange()");
    //     return (target.transform.position - transform.position).sqrMagnitude <= attackRange * attackRange;
    // }

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
