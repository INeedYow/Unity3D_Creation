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

    [Header("Portals")]
    [HideInInspector] public int clearLevel = 0;
    public GameObject[] monsterPlanets = new GameObject[5];                         // 현재 레벨 포탈
    public GameObject[] bossPortals = new GameObject[5];
    public GameObject[] clearPortals = new GameObject[5];                       // 클리어한 던전 재진입 용도 포탈


    [Header("Dungeons")]
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
    public Skill prfGolemEarthSkill;
    public Skill prfGolemIceSkill;
    public Skill prfGolemFireSkill;
    public Skill[] prfGolemBlueSkills = new Skill[2];
    public Skill[] prfGolemRedSkills = new Skill[2];


    [Space(10f)] [Header("Rewards")]
    [SerializeField] int m_exp;
    [SerializeField] int m_gold;
    [SerializeField] List<EquipItemData> m_items;


    [Space(4f)] [Header("ETC")]
    public Transform worldEffectTF;
    public Transform monBackTF;
    public Transform heroBackTF;
    public bool isLockClick;


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

        for (int i = 0; i < clearPortals.Length; i++)
        {
            clearPortals[i].SetActive(false);
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
        ClearEvent();
        curDungeon.BattleEnd();
        WaveEnd();
        ShowResultWave();
    }

    public void AddReward(int gold, int exp)
    {
        m_gold += gold;
        m_exp += exp;
    }

    public void AddReward(EquipItemData itemData)
    {
        m_items.Add(itemData);
    }

    public void ClearDungeon(int level)
    {
        if (level >= monsterPlanets.Length) return;

        if (clearLevel < level)
        {
            monsterPlanets[clearLevel].SetActive(false);
            clearPortals[clearLevel].SetActive(true);

            for (int i = clearLevel; i < monsterPlanets.Length; i++)
            {
                monsterPlanets[i].transform.Translate(0f, 0f, -8f);
            }
            clearLevel = level;
            
            bossPortals[clearLevel].AddComponent<Portal>().index = clearLevel;
            bossPortals[clearLevel].AddComponent<FocusEffetor>().focusedScale = new Vector3(3f, 3f, 3f);
        }
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
    {   //Debug.Log("reward exp / gold : " + m_exp + " / " + m_gold);
        float dura = 0f;
        float gainExp;

        while (dura < 1f)
        {
            gainExp = Time.deltaTime * m_exp;
            PlayerManager.instance.AddExp(gainExp);
            PartyManager.instance.AddExp(gainExp);
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

        foreach (EquipItemData item in m_items)
        {
            if (item is WeaponItemData)
            {
                InventoryManager.instance.AddItem(item as WeaponItemData);
            }
            else if (item is ArmorItemData)
            {
                InventoryManager.instance.AddItem(item as ArmorItemData);
            }
            else
            {
                InventoryManager.instance.AddItem(item as AccessoryItemData);
            }
        }
        
        resultUI.itemTray.gameObject.SetActive(true);
        resultUI.itemTray.onFinish += ShowExitButton;
        resultUI.itemTray.ShowItem(m_items);
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
        m_items.Clear();

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
        ObjectPool.instance.GetEffect((int)EEffect.Spawn).SetPosition(monster);
        
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

    public void ClearEvent()
    {
        foreach(Monster mons in curDungeon.curMonsters)
        {
            mons.onDeadGetThis -= OnMonsterDie;
        }
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