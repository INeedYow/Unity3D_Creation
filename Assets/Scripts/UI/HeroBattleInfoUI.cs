using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBattleInfoUI : MonoBehaviour
{
    public List<HeroBattleInfoUnit> battleHeroInfoUnits;
    public RectTransform rtf;
    public float defaultX;
    //public Color checkColor = new Color(0, 1f, 1f);


    private void Awake() {
        Init();
    }

    void Init(){
        defaultX = rtf.position.x;
    }

    private void OnEnable() {
        for (int i = 0; i < PartyManager.instance.heroParty.Count; i++){
            battleHeroInfoUnits[i].gameObject.SetActive(true);
            battleHeroInfoUnits[i].SetOwner(PartyManager.instance.heroParty[i]);
        }
    }
}
