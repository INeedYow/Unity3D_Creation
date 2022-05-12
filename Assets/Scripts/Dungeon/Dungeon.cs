using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int dungeonLevel;
    public int curWave;
    public int maxWave;

    public List<Wave> waves;
    public List<Monster> curMonsters;   // isDead ture, false 모두 갖고 있음
    public int curMosterCount;          // isDead false인 몬스터의 수

    public Transform spawnAreaParent;
    public Transform[] spawnTransforms;
    public int spawnTransformIndex;
    public Transform homeTransform; // 모든 던전이 같은 방향이라면 월드 스페이스 Vector로 정해줘도 될듯

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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Enter();
        }
    }

    void Enter() {
        curWave = 1;
        SpawnWave();
    }

    public Transform GetNextSpawnTransform()
    {
        int curIndex = spawnTransformIndex++;
        spawnTransformIndex %= spawnTransforms.Length;
        return spawnTransforms[curIndex];
    }

    public void MonsterDie()
    {   
        curMosterCount--;
        if (curMosterCount <= 0)
        {   // 웨이브 클리어
            curMonsters.Clear();
            curWave++;
            DungeonManager.instance.onWaveEnd?.Invoke();
            PartyManager.instance.ResetHeroPos();
            
            if (curWave > maxWave) {
                ClearDungeon();
                return;
            }
            SpawnWave();
        }
    }

    public Monster GetRandMonster()
    {
        if (curMonsters.Count == 0) return null;
        return curMonsters[Random.Range(0, curMonsters.Count)];
    }

    void ClearDungeon()
    {
        Debug.Log("Dungeon Clear");
    }
    
    void SpawnWave()
    {
        Debug.Log(curWave + " 웨이브 시작");
        spawnTransformIndex = 0;
        waves[curWave - 1].SpawnWave();
        Invoke("WaveStart", 1f);
    }

    void WaveStart()
    {
        DungeonManager.instance.WaveStart();
    }

    public Monster GetAliveMonster()
    {
        foreach (Monster aliveMons in curMonsters)
        {
            if (aliveMons.isDead == false)
            {
                return aliveMons;
            }
        }
        return null;
    }

}
