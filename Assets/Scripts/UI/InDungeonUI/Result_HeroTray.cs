using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Result_HeroTray : MonoBehaviour
{
    public UnityAction onFinish;

    public List<Result_HeroUnit> resultHeroUnits;

    float m_interval = 0.5f;

    private void OnEnable() {
        for (int i = 0; i < resultHeroUnits.Count; i++)
        {
            resultHeroUnits[i].gameObject.SetActive(false);
        }
    }

    public void ShowExp()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        for (int i = 0; i < PartyManager.instance.heroParty.Count; i++)
        {
            resultHeroUnits[i].gameObject.SetActive(true);
            resultHeroUnits[i].SetOwner(PartyManager.instance.heroParty[i]);

            yield return new WaitForSeconds(m_interval);
        }

        onFinish?.Invoke();
    }
}
