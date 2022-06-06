using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonManager : MonoBehaviour
{   //
    public UnityAction onHeroDead;
    public UnityAction onMonsterDead;
    public UnityAction onSomeoneAdd;
    public UnityAction onChangeAnyHP;
    public UnityAction onChangeAnyDamage;
    public UnityAction onChangeAnyArmor;
    public UnityAction onChangeAnyMagic;
    public UnityAction onChangeAnyMagicArmor;
    public UnityAction onChangeAnySpeed;
    public UnityAction onChangeAnyRange;
    public UnityAction onChangeAnyBuff;
    public UnityAction onWaveStart;
    public UnityAction onWaveEnd;
    public UnityAction onDungeonExit;

    public static DungeonManager instance { get; private set; }
    
    public List<Dungeon> listDungeon = new List<Dungeon>();
    public Dungeon curDungeon;

    [Space(10f)] [Header("Dungeon UI")]
    public DungeonUI dungeonUI;
    public MonBattleInfoUI monInfoUI;
    public ResultUI resultUI;
    public GameObject runePlane;


    [Space(10f)] [Header("Monster prfSkill")]
    public Skill prfSporeSkill;
    public Skill prfPollenSkill;
    public Skill prfPlantSkill;
    public Skill[] prfChewerSkills = new Skill[2];
    public Skill prfSkeletonSkill;
    public Skill prfEvilMageSkill;


    [Space(10f)] [Header("Rewards")]
    [SerializeField] int m_exp;
    [SerializeField] int m_gold;
    [SerializeField] List<EquipItemData> m_item;


    [Space(4f)] [Header("ETC")]
    public Transform worldEffectTF;
    public Transform monBackTF;
    public Transform heroBackTF;


    HpBar bar;

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
        PlayerManager.instance.runeTree.Apply();        // dungeonUI 보다 늦게 적용해야지 UI Active 돼서 적용 됨

        dungeonUI.Init(curDungeon);

        GameManager.instance.EnterDungeon(true);
    }

    public void BattleEnd()
    {
        ShowResultWave();
    }

    public void AddReward(int gold, int exp)
    {
        m_gold += gold;
        m_exp += exp;
    }

    public void AddReward(EquipItemData itemData)
    {
        m_item.Add(itemData);
    }


    ////// ////// //////
    ////// result //////
    ////// ////// //////

    void ShowResultWave()
    {
        resultUI.gameObject.SetActive(true);
        resultUI.waveBar.onFinishFill += ShowResultExp;

        resultUI.waveBar.SetWaveBar(curDungeon.curWave - 1, curDungeon.maxWave);
    }

    void ShowResultExp()
    {   
        resultUI.waveBar.onFinishFill -= ShowResultExp;

        resultUI.playerExpBar.gameObject.SetActive(true);
        resultUI.heroExpTray.gameObject.SetActive(true);
        
        StartCoroutine("GiveExp");
    }

    IEnumerator GiveExp()
    {
        float dura = 0f;
        float gainExp;

        while (dura < 1f)
        {
            gainExp = Time.deltaTime * m_exp;
            PlayerManager.instance.AddExp((int)gainExp);
            PartyManager.instance.AddExp((int)gainExp);
            dura += Time.deltaTime;
            yield return null;
        }

        resultUI.playerExpBar.SetFinish();

        resultUI.heroExpTray.onFinish += ShowResultItem;
        resultUI.heroExpTray.ShowExp();
    }

    void ShowResultItem()
    {
        resultUI.heroExpTray.onFinish -= ShowResultItem;

        resultUI.itemTray.gameObject.SetActive(true);
        resultUI.itemTray.onFinish += ShowExitButton;
        resultUI.itemTray.ShowItem(m_item);
    }

    void ShowExitButton()
    {
        resultUI.exitBtn.gameObject.SetActive(true);
    }

    ////// ////// //////
    ////// ////// //////
    ////// ////// //////





    public void Exit()  // 버튼 이벤트
    {
        onDungeonExit?.Invoke();
        
        curDungeon.ClearMonster();  // 전투 패배 시 남은 몬스터 제거
        curDungeon.gameObject.SetActive(false);
        curDungeon = null;

        PartyManager.instance.ExitDungeon();
        PlayerManager.instance.runeTree.Release();

        resultUI.gameObject.SetActive(false);
        dungeonUI.gameObject.SetActive(false);

        GameManager.instance.EnterDungeon(false);
    }

    public void WaveStart()
    {
        foreach (Character hero in PartyManager.instance.heroParty)
        { hero.Resume(); }
        foreach (Character mons in curDungeon.curMonsters)
        { mons.Resume(); }
        onWaveStart?.Invoke();
    }

    public void WaveEnd()
    {
        foreach (Character hero in PartyManager.instance.heroParty)
        { hero.Pause(); }
        foreach (Character mons in curDungeon.curMonsters)
        { mons.Pause(); }
        onWaveEnd?.Invoke();
    }

    public void AddMonster(Monster monster)
    {
        monster.transform.position = curDungeon.GetNextSpawnTransform().position;
        curDungeon.curMonsters.Add(monster);
        curDungeon.curMonsterCount++;
        monster.onDeadGetThis += OnMonsterDie;

        //monster.Reset();
        bar = ObjectPool.instance.GetHpBar(false);
        bar.SetOwner(monster);

        dungeonUI.SetCount(curDungeon.curMonsterCount);
    }

    public void ReviveMonster()
    {
        curDungeon.curMonsterCount++;
        dungeonUI.SetCount(curDungeon.curMonsterCount);
    }

    public void RemoveMonster(Monster monster)
    {
        curDungeon.curMonsters.Remove(monster);
    }

    public void OnMonsterDie(Character character)
    {   
        curDungeon.MonsterDie();
        character.onDeadGetThis -= OnMonsterDie;

        dungeonUI.SetCount(curDungeon.curMonsterCount);
    }

    public void ShowMonInfoUI(Monster monster) {    
        monInfoUI.gameObject.SetActive(true);
        monInfoUI.RenewUI(monster);
    }

    //public void HideMonInfoUI() { monInfoUI.gameObject.SetActive(false); } // 클릭하면 사라지게 해뒀음

    public Monster GetAliveMonster()
    {
        int random = Random.Range(0, curDungeon.curMonsters.Count);

        for (int i = 0; i < curDungeon.curMonsters.Count; i++)
        {
            if (curDungeon.curMonsters[random].isDead || curDungeon.curMonsters[random].isStop)
            {
                random++;
                random %= curDungeon.curMonsters.Count;
                continue;
            }

            return curDungeon.curMonsters[random];
        }


        return null;
    }

}