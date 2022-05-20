using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    static PartyManager _instance;
    public static PartyManager instance { 
        get{ 
            if (_instance == null)
            {
                _instance = new PartyManager();
            }
            return _instance;
        }
    }

    public List<Hero> heroParty = new List<Hero>();
    public int maxCount = 2;
    [Header("Board")]
    public Board board;

    private void Awake() { _instance = this; }

    public void EnterDungeon(){
        SwapDummy2GFX();
        ResetHeroPos();
    }

    public void ExitDungeon(){
        // TODO
    }

    public void TurnOffBoard()
    {
        HideDummy();
        board.transform.localPosition = Vector3.zero;
        board.isActive = false;
    }

    void SwapDummy2GFX(){
        foreach(Hero hero in heroParty)
        {
            hero.dummy.gameObject.SetActive(false);
            hero.heroGFX.gameObject.SetActive(true);
        }
    }

    public void ShowDummy(){
        if (heroParty.Count == 0) return;
        foreach(Hero hero in heroParty)
        {
            hero.dummy.gameObject.SetActive(true);
        }
    }

    public void HideDummy(){
        if (heroParty.Count == 0) return;
        foreach(Hero hero in heroParty)
        {
            hero.dummy.gameObject.SetActive(false);
        }
    }

    public void ResetHeroPos(){
        foreach (Hero hero in heroParty)
        {
            hero.ResetPos();
        }
    }

    public void Join(Hero hero)
    {
        if (heroParty.Count >= maxCount) return;
        heroParty.Add(hero);
    }

    public void Leave(Hero hero)
    {
        heroParty.Remove(hero);
    }

    public Hero GetAliveHero()
    {
        foreach (Hero aliveHero in heroParty)
        {
            if (aliveHero.isDead == false)
            {
                return aliveHero;
            }
        }
        return null;
    }
}
