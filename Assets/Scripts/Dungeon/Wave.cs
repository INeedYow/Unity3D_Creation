using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wave : MonoBehaviour
{
    public List<Monster> prfMonsters;
    public List<int> counts;

    public void SpawnWave()
    {
        for (int j = 0; j < prfMonsters.Count; j++)
        {   // 0번째 몬스터가 0번째 int 만큼 생성
            for (int i = 0; i < counts[j]; i++)
            {
                DungeonManager.instance.AddMonster(
                    Instantiate(
                        prfMonsters[j],
                        DungeonManager.instance.curDungeon.GetNextSpawnTransform().position,
                        Quaternion.identity));
            }
        }
    }
}