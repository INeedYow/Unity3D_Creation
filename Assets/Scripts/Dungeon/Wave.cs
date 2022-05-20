using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wave : MonoBehaviour
{
    //public List<Monster> prfMonsters;
    public List<EMonster> eMonsters;
    public List<int> counts;

    public void SpawnWave()
    {
        //Monster monster;
        for (int j = 0; j < eMonsters.Count; j++)
        {
            for (int i = 0; i < counts[j]; i++)
            {
                // monster = ObjectPool.instance.GetMonster((int)eMonsters[j]);
                // DungeonManager.instance.AddMonster(monster);

                DungeonManager.instance.AddMonster(
                    ObjectPool.instance.GetMonster((int)eMonsters[j])
                );
            }
        }
    }
}