using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " SpawnData", menuName = " Data/Spawn")]
public class SpawnData : ScriptableObject
{
    public Monster prfMonster;
    public int count;

    public void Spawn()
    {
        for (int i = 0; i < count; i++)
        {   // 설정된 포지션 넘어서 스폰되는 경우 다시 0위치부터 스폰되게
            DungeonManager.instance.AddMonster(
                Instantiate(
                    prfMonster, 
                    DungeonManager.instance.curDungeon.GetNextSpawnTransform().position,
                    Quaternion.identity));
        }
    }
}
