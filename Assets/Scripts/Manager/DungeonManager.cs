using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance { get; private set; }

    public Transform spawnTransform;
    public List<Dungeon> listDungeon = new List<Dungeon>();
    public Dungeon curDungeon;

    private void Awake() {
        instance = this;
    }

    public void Enter(int index)
    {   // TODO
        curDungeon = listDungeon[index];
    }

    public void AddMonster(Monster monster)
    {
        curDungeon.curMonsters.Add(monster);
        curDungeon.curMosterCount++;        // 카운트 변수가 필요한가? 리스트.count 써도 될듯
        monster.onDeath += OnMonsterDie;
    }

    public void OnMonsterDie(Monster monster)
    {
        curDungeon.RemoveMonster(monster);
    }
    
    //테스트용
    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.Q))
    //     {
    //         IDamagable target = curDungeon.curMonsters[Random.Range(0, curDungeon.curMonsters.Count)].GetComponent<IDamagable>();
    //         target?.Damaged(100f, 10f);
    //     }
    // }
}
