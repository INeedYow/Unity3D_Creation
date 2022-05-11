using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wave : MonoBehaviour
{
    public List<SpawnData> listSpawnData;

    public void SpawnWave()
    {
        foreach (SpawnData data in listSpawnData)
        {
            data.Spawn();
        }
    }
}