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
    public UnityAction onWaveStart;
    public UnityAction onWaveEnd;

    public static DungeonManager instance { get; private set; }
    public List<Dungeon> listDungeon = new List<Dungeon>();
    public Dungeon curDungeon;

    private void Awake() {
        instance = this;
        Init();
    }

    void Init(){
        for (int i = 0; i < transform.childCount; i++)
        {
            listDungeon.Add(transform.GetChild(i).GetComponent<Dungeon>());
        }
    }

    public void Enter(int index)
    {
        curDungeon = listDungeon[index];
        curDungeon.gameObject.SetActive(true);
        PartyManager.instance.SwapDummy2GFX();
        PartyManager.instance.ResetHeroPos();
    }

    public void WaveStart()
    {
        onWaveStart?.Invoke();
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
    {   // count, isdead 확인필요
        curDungeon.MonsterDie();
    }
}
