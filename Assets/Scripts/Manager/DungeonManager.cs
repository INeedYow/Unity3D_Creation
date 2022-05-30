﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : MonoBehaviour
{   //
    public UnityAction onChangeAnyHP;
    public UnityAction onChangeAnyPower;
    public UnityAction onChangeAnyArmor;
    public UnityAction onChangeAnyMagicPower;
    public UnityAction onChangeAnyMagicArmor;
    public UnityAction onChangeAnySpeed;
    public UnityAction onChangeAnyAttackSpeed;
    public UnityAction onWaveStart;
    public UnityAction onWaveEnd;
    public UnityAction onDungeonExit;

    public static DungeonManager instance { get; private set; }
    public List<Dungeon> listDungeon = new List<Dungeon>();
    public Dungeon curDungeon;

    [Space(10f)] [Header("Dungeon UI")]
    public DungeonUI dungeonUI;
    public MonBattleInfoUI monInfoUI;
    public ResultUI resultUI;

    [Space(10f)] [Header("Monster prfSkill")]
    public Skill prfSporeSkill;


    HpBar bar;

    private void Awake() {
        instance = this;
        Init();
    }

    void Init(){
        for (int i = 0; i < transform.childCount; i++)
        {
            listDungeon.Add(transform.GetChild(i).GetComponent<Dungeon>());
        }
    }

    public void Enter(int index)
    {
        curDungeon = listDungeon[index];
        curDungeon.gameObject.SetActive(true);
        PartyManager.instance.EnterDungeon();
        PlayerManager.instance.runeTree.Apply();

        // UI
        dungeonUI.gameObject.SetActive(true);
        dungeonUI.Init(curDungeon);

        GameManager.instance.EnterDungeon(true);
    }

    public void BattleEnd()
    {
        ShowResult();
    }

    void ShowResult()
    {
        resultUI.gameObject.SetActive(true);
    }

    public void Exit()  // 버튼 이벤트
    {
        onDungeonExit?.Invoke();
        
        curDungeon.gameObject.SetActive(false);
        curDungeon = null;
        PartyManager.instance.ExitDungeon();
        PlayerManager.instance.runeTree.Release();

        resultUI.gameObject.SetActive(false);
        dungeonUI.gameObject.SetActive(false);

        GameManager.instance.EnterDungeon(false);
    }

    public void WaveStart()
    {
        onWaveStart?.Invoke();
        foreach (Character hero in PartyManager.instance.heroParty)
        { hero.Resume(); }
        foreach (Character mons in curDungeon.curMonsters)
        { mons.Resume(); }
    }

    public void WaveEnd()
    {
        onWaveEnd?.Invoke();
        foreach (Character hero in PartyManager.instance.heroParty)
        { hero.Pause(); }
        foreach (Character mons in curDungeon.curMonsters)
        { mons.Pause(); }
    }

    public void AddMonster(Monster monster)
    {
        monster.transform.position = curDungeon.GetNextSpawnTransform().position;
        curDungeon.curMonsters.Add(monster);
        curDungeon.curMonsterCount++;
        monster.onDeadGetThis += OnMonsterDie;

        monster.Reset();
        bar = ObjectPool.instance.GetHpBar(false);
        bar.SetOwner(monster);

        dungeonUI.SetCount(curDungeon.curMonsterCount);
    }

    public void OnMonsterDie(Character character)
    {   
        curDungeon.MonsterDie();
        character.onDeadGetThis -= OnMonsterDie;

        dungeonUI.SetCount(curDungeon.curMonsterCount);
    }

    public void ShowMonInfoUI(Monster monster) {    
        monInfoUI.gameObject.SetActive(true);
        monInfoUI.RenewUI(monster);
    }

    //public void HideMonInfoUI() { monInfoUI.gameObject.SetActive(false); } // 현재 클릭하면 사라지게 해뒀음
}