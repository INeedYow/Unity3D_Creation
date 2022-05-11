using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{   // TODO UI에 배치한 거 받아서 시작 포지션 정해주기
    // UI unit 위에 아이콘?만 생성하고 해당 UI의 좌표로 시작 pos 잡아주면 될듯
    public static PartyManager instance { get; private set; }
    public int maxCount = 2;    // 특정 레벨? 시 증가

    public List<Hero> heroParty = new List<Hero>();
    private void Awake() {
        instance = this;
    }

    public void ResetHeroPos(){
        foreach (Hero hero in heroParty)
        {
            hero.ResetPos();
        }
    }

    public void Join(Hero hero)
    {
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
