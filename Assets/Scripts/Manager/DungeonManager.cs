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

    [Space(10f)] [Header("Dungeon UI")]
    public DungeonUI dungeonUI;

    [Space(10f)] [Header("Monster prfSkill")]
    public Skill prfSporeSkill;

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
        PartyManager.instance.EnterDungeon();

        // UI
        dungeonUI.gameObject.SetActive(true);
        dungeonUI.Init(curDungeon);

        GameManager.instance.EnterDungeon(true);
    }

    public void Exit()
    {
        // TODO
        curDungeon.gameObject.SetActive(false);
        curDungeon = null;
        PartyManager.instance.ExitDungeon();

        dungeonUI.gameObject.SetActive(false);

        GameManager.instance.EnterDungeon(false);
    }

    public void WaveStart()
    {
        onWaveStart?.Invoke();
        foreach (Character hero in PartyManager.instance.heroParty)
        { hero.Resume(); }
        foreach (Character mons in curDungeon.curMonsters)
        { mons.Resume(); }
    }

    public void WaveEnd()
    {
        onWaveEnd?.Invoke();
        foreach (Character hero in PartyManager.instance.heroParty)
        { hero.Pause(); }
        foreach (Character mons in curDungeon.curMonsters)
        { mons.Pause(); }
    }

    public void AddMonster(Monster monster)
    {
        monster.transform.position = curDungeon.GetNextSpawnTransform().position;
        curDungeon.curMonsters.Add(monster);
        curDungeon.curMonsterCount++; //Debug.Log("count++ : " + curDungeon.curMonsterCount);
        monster.onDead += OnMonsterDie;

        //dungeonInfoUI.SetCount(curDungeon.curMonsterCount);
        dungeonUI.SetCount(curDungeon.curMonsterCount);
    }

    public void OnMonsterDie(Character character)
    {   
        curDungeon.MonsterDie();
        character.onDead -= OnMonsterDie;

        //dungeonInfoUI.SetCount(curDungeon.curMonsterCount);
        dungeonUI.SetCount(curDungeon.curMonsterCount);
    }
}