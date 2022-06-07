using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public string DGName;
    public int dungeonLevel;
    public int curWave;
    public int maxWave;

    [Header("Wave")]
    public Transform waveParent;
    public List<Wave> waves;            
    public List<Monster> curMonsters;       // isDead ture, false 모두 갖고 있음
    public int curMonsterCount;             // isDead false인 몬스터의 수

    public Transform spawnAreaParent;
    public Transform[] spawnTransforms;
    public int spawnTransformIndex;
    public Transform beginTf;

    [Header("Rewards--------------------------")]
    public RewardData rewardData;
    

    private void Awake() {
        Init();
    }

    void Init(){
        // 스폰 지역 초기화
        spawnTransforms = new Transform[spawnAreaParent.childCount];
        for (int i = 0; i < spawnTransforms.Length; i++)
        {
            spawnTransforms[i] = spawnAreaParent.GetChild(i);
        }

        // Waves 자식으로 넣어준 wave로 웨이브 배열 초기화
        for (int i = 0; i < waveParent.childCount; i++)
        {
            waves.Add(waveParent.GetChild(i).GetComponent<Wave>());
        }

        maxWave = waves.Count;
    }

    void OnEnable() {
        curWave = 1;
        
        Invoke("SpawnWave", 1f);
    }

    private void OnDisable() {
        //ClearMonster();
        curMonsterCount = 0;
    }

    void AddWaveReward()
    {
        DungeonManager.instance.AddReward(rewardData.GetWaveGold(), rewardData.GetWaveExp());
        
        if (curWave == maxWave)
        {   Debug.Log("Add Accessory Reward");
            DungeonManager.instance.AddReward(ItemManager.instance.GetRandAccessory());
        }
        else if (curWave % 5 == 0)
        {
            if (Random.Range(0, 10) < 9)
            {   Debug.Log("Add Equip Reward");
                DungeonManager.instance.AddReward(ItemManager.instance.GetRandEquipItem());
            }
        }
    }

    void AddClearReward()
    {
        DungeonManager.instance.AddReward(rewardData.gold_clear, rewardData.exp_clear);
    }

    public void MonsterDie()
    {   
        curMonsterCount--;  

        if (curMonsterCount <= 0)
        {   
            WaveEnd();
            AddWaveReward();
            Invoke("WaveClear", 1f);
        }
    }

    void WaveClear()
    {   
        ClearMonster();
        
        curWave++;

        if (curWave > maxWave) { 
            ClearDungeon(); 
        }
        else{ 
            PartyManager.instance.ResetHeroPos();
            SpawnWave(); 
        }
    }

    void EndDungeon()
    {   // 중간에 종료
        //GetReward();
        //
        DungeonManager.instance.BattleEnd();
    }

    void ClearDungeon()
    {   // 클리어
        AddClearReward();
        //GetReward();
        //
        DungeonManager.instance.ClearDungeon(dungeonLevel);
        DungeonManager.instance.BattleEnd();
    }

    void SpawnWave()
    {   
        spawnTransformIndex = 0;
        waves[curWave - 1].SpawnWave();
        Invoke("WaveStart", 1f);
        
        //DungeonManager.instance.dungeonInfoUI.SetCurWave(m_curWave);
        DungeonManager.instance.dungeonUI.SetCurWave(curWave);
    }

    void WaveStart()    { DungeonManager.instance.WaveStart(); }

    void WaveEnd()      { DungeonManager.instance.WaveEnd(); } 


    public void ClearMonster()
    {   

        foreach(Monster mons in curMonsters)
        {
            ObjectPool.instance.ReturnObj(mons.gameObject);
        }

        curMonsters.Clear();
    }

    public Monster GetAliveMonster()
    {
        foreach (Monster aliveMons in curMonsters)
        {
            if (aliveMons.isDead || aliveMons.isStop) continue;
            
            return aliveMons;
        }
        return null;
    }

    public Transform GetNextSpawnTransform()
    {
        int curIndex = spawnTransformIndex++;
        spawnTransformIndex %= spawnTransforms.Length;
        return spawnTransforms[curIndex];
    }

    public void BattleEnd()
    {
        CancelInvoke("WaveStart");
        CancelInvoke("SpawnWave");
        CancelInvoke("WaveClear");
    }
}