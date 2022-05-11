using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int dungeonLevel;
    public int curWave;
    public int maxWave;

    public List<Wave> waves;
    public List<Monster> curMonsters;
    public int curMosterCount;

    public Transform spawnAreaParent;
    public Transform[] spawnTransforms;
    public int spawnTransformIndex;
    public Transform homeTransform; // 모든 던전이 같은 방향이라면 월드 스페이스 Vector로 정해줘도 될듯

    private void Awake() {
        Init();
    }

    private void OnEnable() {
        Enter();
    }

    void Init(){
        spawnTransforms = new Transform[spawnAreaParent.childCount];

        for (int i = 0; i < spawnTransforms.Length; i++)
        {
            spawnTransforms[i] = spawnAreaParent.GetChild(i);
        }
        maxWave = waves.Count;
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

    public void RemoveMonster(Monster monster)
    {
        curMonsters.Remove(monster);
        curMosterCount--;
        if (curMosterCount <= 0)
        {   // 웨이브 클리어
            curWave++;
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
    }

}
