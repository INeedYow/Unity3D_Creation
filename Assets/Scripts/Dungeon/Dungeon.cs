using System.Collections;
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

    public Transform GetNextSpawnTransform()
    {
        int curIndex = spawnTransformIndex++;
        spawnTransformIndex %= spawnTransforms.Length;
        return spawnTransforms[curIndex];
    }

    public void MonsterDie()
    {   
        curMonsterCount--;
        //Debug.Log("count-- : " + curMonsterCount);
        if (curMonsterCount <= 0)
        {   // 웨이브 클리어
            Invoke("WaveClear", 1f);
        }
    }

    void WaveClear()
    {   // 비활성화 했던 몬스터들 제거
        foreach(Monster mons in curMonsters){
            ObjectPool.instance.ReturnObj(mons.gameObject);
        }
        curMonsters.Clear();

        m_curWave++;

        if (m_curWave > maxWave) { ClearDungeon(); }
        else{ 
            PartyManager.instance.ResetHeroPos();
            SpawnWave(); 
        }
    }

    void ClearDungeon()
    {   Debug.Log("Dungeon Clear");
        // TODO 
    }

    void SpawnWave()
    {   Debug.Log(m_curWave + " 웨이브 시작");
        spawnTransformIndex = 0;
        waves[m_curWave - 1].SpawnWave();
        Invoke("WaveStart", 1f);
        
        //DungeonManager.instance.dungeonInfoUI.SetCurWave(m_curWave);
        DungeonManager.instance.dungeonUI.SetCurWave(m_curWave);
    }

    void WaveStart()
    {
        DungeonManager.instance.WaveStart();
    }

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
}