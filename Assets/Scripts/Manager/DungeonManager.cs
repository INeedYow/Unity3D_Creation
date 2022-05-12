using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : MonoBehaviour
{   //
    public UnityAction onChangeAnyHP;
    public UnityAction onChangeAnyPower;
    public UnityAction onChangeAnyArmor;
    public UnityAction onChangeAnyMagicPower;
    public UnityAction onChangeAnyMagicArmor;
    public UnityAction onChangeAnyAttackRange;
    public UnityAction onChangeAnyAttackSpeed;
    public UnityAction onChangeAnyMoveSpeed;
    public UnityAction onWaveEnd;

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
        curDungeon.gameObject.SetActive(true);
    }

    public void BattleStart()
    {
        foreach (Character hero in PartyManager.instance.heroParty){
            hero.Resume();
        }
        foreach (Character mons in curDungeon.curMonsters){
            mons.Resume();
        }
    }

    public void AddMonster(Monster monster)
    {
        curDungeon.curMonsters.Add(monster);
        curDungeon.curMosterCount++; 
        monster.onDeath += OnMonsterDie;
    }

    public void OnMonsterDie()
    {   // TODO count, isdead
        curDungeon.MonsterDie();
    }
}
