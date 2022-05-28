﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set;}


    [Header("UI")]
    public PlayerInfoUI playerInfoUI;

    //[Header("Player Skill")]
    //public List<PlayerSkill> skills;

    //
    public int maxLV    { get { return 25; } }
    public int LV       { get; private set; }
    public int maxExp   { get; private set; }
    int _curExp;
    public int curExp{
        get { return _curExp; }
        set {
            if (LV == maxLV) return;
            _curExp = value;
            if (_curExp >= maxExp) { LevelUp(); } 
        }
    }
    public int gold         { get; private set; }


    private void Awake() { instance = this; }

    private void Start() {
        SetBeginInfo();
        InitUI();
    }

    void SetBeginInfo(){
        LV = 1;
        maxExp = 150;
        gold = 500000;  
    }
    void InitUI(){
        playerInfoUI.RenewCurParty(PartyManager.instance.heroParty.Count);
        playerInfoUI.RenewMaxParty(PartyManager.instance.maxCount);
        playerInfoUI.RenewExp(curExp, maxExp);
        playerInfoUI.RenewGold(gold);
        playerInfoUI.RenewLV(LV);
    }

    void LevelUp(){ 
        LV++;                       
        curExp -= maxExp;          
        maxExp += 75;           
        playerInfoUI.RenewLV(LV);
        playerInfoUI.RenewExp(curExp, maxExp);
        
        if (LV % 5 == 0){
            PartyManager.instance.maxCount++;
            playerInfoUI.RenewMaxParty(PartyManager.instance.maxCount);
        }
        if (LV == maxLV) curExp = maxExp;
    }

    public void AddGold(int amount){    
        gold += amount;
        playerInfoUI.RenewGold(gold);
    }

    public void AddExp(int amount){ 
        curExp += amount;   
        playerInfoUI.RenewExp(curExp, maxExp);
    }

    public void RenewCurParty(int value){
        playerInfoUI.RenewCurParty(value);
    }
}
