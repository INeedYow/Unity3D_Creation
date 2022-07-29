using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Result_ItemTray : MonoBehaviour
{
    public UnityAction onFinish;

    public List<Result_ItemUnit> units;

    List<EquipItemData> m_listData;

    private void OnEnable() {
        Init();
    }

    void Init()
    {
        foreach (Result_ItemUnit unit in units)
        {
            unit.gameObject.SetActive(false);
        }
    }

    public void ShowItem(List<EquipItemData> listData)
    {
        m_listData = listData;
        StartCoroutine("ShowInterval");
    }

    IEnumerator ShowInterval()
    {
        for (int i = 0; i < m_listData.Count; i++)
        {
            units[i].gameObject.SetActive(true);
            units[i].SetData(m_listData[i]);

            yield return new WaitForSeconds(0.6f);
        }

        onFinish?.Invoke();
        yield return null;
    }
}
