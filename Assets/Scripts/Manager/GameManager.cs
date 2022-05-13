using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int gold;
    public int level = 1;
    public List<Hero> heroList = new List<Hero>();

    public Board board;
    public Hero selectedHero;
    public HeroListUI heroListUI;

    [Header("Hero Prf")]
    public Hero prfKngiht;


    private void Awake() {
        instance = this;
    }

    public void TempDGEnter(int index)
    {
        board.gameObject.SetActive(false);
        DungeonManager.instance.Enter(index);
    }

    public void GetNewKnight(){
        Hero hero = Instantiate(prfKngiht);
        heroList.Add(hero);
        heroListUI.AddHeroUnit(hero);
    }

    public void SelectHero(Hero hero){
        if (null != selectedHero)
        {
            selectedHero.dummy.gameObject.SetActive(false);
        }
        selectedHero = hero;
        hero.dummy.gameObject.SetActive(true);
    }

    //
    private void Update() {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetNewKnight();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (Monster mons in DungeonManager.instance.curDungeon.curMonsters)
            {
                mons.curHp *= 0.45f;
            }
        }
    }
}
