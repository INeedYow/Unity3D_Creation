﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public string DGName;
    public int dungeonLevel;
    int m_curWave;
    public int maxWave;

    public List<Wave> waves;            
    public List<Monster> curMonsters;       // isDead ture, false 모두 갖고 있음
    public int curMonsterCount;             // isDead false인 몬스터의 수

    public Transform spawnAreaParent;
    public Transform[] spawnTransforms;
    public int spawnTransformIndex;
    public Transform beginTf;

    [Header("Rewards--------------------------")]
    public RewardData rewardData;
    int m_gold;
    [SerializeField]
    int m_exp;
    

    private void Awake() {
        Init();
    }

    void Init(){
        spawnTransforms = new Transform[spawnAreaParent.childCount];
        for (int i = 0; i < spawnTransforms.Length; i++)
        {
            spawnTransforms[i] = spawnAreaParent.GetChild(i);
        }
        maxWave = waves.Count;
    }

    void OnEnable() {
        m_curWave = 1;
        Invoke("SpawnWave", 1f);
    }

    public void MonsterDie()
    {   
        curMonsterCount--;
        m_gold += Random.Range(0, dungeonLevel + 3);
        m_exp += Random.Range(0, dungeonLevel + 1);
        //Debug.Log("count-- : " + curMonsterCount);
        if (curMonsterCount <= 0)
        {   // 웨이브 클리어
            WaveEnd();
            Invoke("WaveClear", 1f);
            m_gold += rewardData.gold_perWave;
            m_exp += rewardData.exp_perWave;
        }
    }

    void WaveClear()
    {   // 비활성화 했던 몬스터들 제거
        foreach(Monster mons in curMonsters){
            ObjectPool.instance.ReturnObj(mons.gameObject);
        }
        curMonsters.Clear();

        m_curWave++;

        if (m_curWave > maxWave) { 
            ClearDungeon(); 
        }
        else{ 
            PartyManager.instance.ResetHeroPos();
            SpawnWave(); 
        }
    }

    void EndDungeon(){     
        // 중간 종료
        GetReward();
        //
        DungeonManager.instance.Exit();
    }

    void ClearDungeon(){   
        // TODO 
        m_gold += rewardData.gold_clear;
        m_exp += rewardData.exp_clear;
        GetReward();
        //
        DungeonManager.instance.Exit();
    }

    void GetReward(){
        PlayerManager.instance.AddGold(m_gold);   //Debug.Log(m_gold);
        PlayerManager.instance.AddExp(m_exp);     //Debug.Log(m_exp);
        PartyManager.instance.AddExp(m_exp);
        m_gold = 0;
        m_exp = 0;
    }

    void SpawnWave()
    {   Debug.Log(m_curWave + " 웨이브 시작");
        spawnTransformIndex = 0;
        waves[m_curWave - 1].SpawnWave();
        Invoke("WaveStart", 1f);
        
        //DungeonManager.instance.dungeonInfoUI.SetCurWave(m_curWave);
        DungeonManager.instance.dungeonUI.SetCurWave(m_curWave);
    }

    void WaveStart()    { DungeonManager.instance.WaveStart(); }

    void WaveEnd()      { DungeonManager.instance.WaveEnd(); } 

    public Monster GetAliveMonster()
    {
        foreach (Monster aliveMons in curMonsters)
        {
            if (aliveMons.isDead == false) return aliveMons;
        }
        return null;
    }

    public Monster GetRandMonster()
    {
        if (curMonsterCount == 0) return null;
        
        int rand = Random.Range(0, curMonsters.Count);

        for (int i = 0; i < curMonsters.Count; i++){
            if (!curMonsters[rand].isDead) return curMonsters[rand];
            rand++;
            rand %= curMonsters.Count;
        }
        return null;
    }

    public Transform GetNextSpawnTransform()
    {
        int curIndex = spawnTransformIndex++;
        spawnTransformIndex %= spawnTransforms.Length;
        return spawnTransforms[curIndex];
    }
}