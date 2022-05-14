using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance { get; private set; }

    public List<Hero> heroParty = new List<Hero>();
    public int maxCount = 2;
    [Header("Board")]
    public Board board;

    private void Awake() { instance = this; }

    public void SwapDummy2GFX(){
        foreach(Hero hero in heroParty)
        {
            hero.dummy.gameObject.SetActive(false);
            hero.heroGFX.gameObject.SetActive(true);
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
