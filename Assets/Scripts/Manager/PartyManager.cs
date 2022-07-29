﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    static PartyManager _instance;
    public static PartyManager instance { 
        get{ 
            if (_instance == null)
            {
                _instance = new PartyManager();
            }
            return _instance;
        }
    }

    public int maxCount = 3;
    public List<Hero> heroParty;
    public int aliveHeroCount;
    

    HpBar bar;

    private void Awake() { _instance = this; }

    public void EnterDungeon(){
        SwapDummy2GFX();
        ResetHeroPos();
        SetOnHpBar();

        aliveHeroCount = heroParty.Count;
    }

    public void ExitDungeon(){
        SwapGFX2Dummy();

        foreach(Hero hero in heroParty)
        {
            hero.curHp = hero.maxHp;
            hero.powerRate = 1f;
            
            if (hero.isDead) hero.isDead = false;
        }
    }

    void SwapDummy2GFX(){
        foreach(Hero hero in heroParty)
        {
            //hero.dummy.gameObject.SetActive(false);
            hero.heroGFX.gameObject.SetActive(true);
        }
    }

    void SwapGFX2Dummy(){
        foreach(Hero hero in heroParty)
        {
            //hero.dummy.gameObject.SetActive(true);
            hero.heroGFX.gameObject.SetActive(false);
        }
    }

    public void ResetHeroPos(){
        foreach (Hero hero in heroParty)
        {
            hero.ResetPos();
        }
    }

    public bool IsFull() { return heroParty.Count >= maxCount; }

    public bool Join(Hero hero)
    {
        if (IsFull()) return false;

        heroParty.Add(hero);
        hero.isJoin = true;
        PlayerManager.instance.RenewCurParty(heroParty.Count);
        return true;
    }

    public void Leave(Hero hero)
    {   //Debug.Log("Leave()" + hero.name);
        heroParty.Remove(hero); 
        hero.isJoin = false;
        PlayerManager.instance.RenewCurParty(heroParty.Count);
    }

    public Hero GetAliveHero()
    {
        int random = Random.Range(0, heroParty.Count);

        for (int i = 0; i < heroParty.Count; i++)
        {
            if (heroParty[random].isDead || heroParty[random].isStop)
            {
                random++;
                random %= heroParty.Count;
                continue;
            }

            return heroParty[random];
        }

        return null;
    }

    public void AddExp(float exp)
    {
        foreach(Hero hero in heroParty)
        {
            //if (hero.isDead) continue;
            
            hero.curExp += exp;
        }
    }

    void SetOnHpBar()
    {   // 끄는 건 던전 종료 시 이벤트 받아서 hpbar가 자체적으로
        foreach(Hero hero in heroParty)
        {
            bar = ObjectPool.instance.GetHpBar(true);
            bar.SetOwner(hero);
        }
    }

    public void HeroDead()
    {
        aliveHeroCount--;

        if (aliveHeroCount == 0)
        {
            DungeonManager.instance.BattleEnd();
        }
    }
}
