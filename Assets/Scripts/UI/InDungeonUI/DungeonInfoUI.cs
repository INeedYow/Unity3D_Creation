using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonInfoUI : MonoBehaviour
{
    [SerializeField] Text m_DGName;
    [SerializeField] Text m_count;
    [SerializeField] Text m_curWave;
    [SerializeField] Text m_maxWave;

    public void Init(Dungeon dg){
        m_DGName.text = dg.DGName;
        m_maxWave.text = dg.maxWave.ToString();

    }

    public void SetCurWave(int value) { m_curWave.text = value.ToString(); } 
    public void SetCount(int value) { m_count.text = value.ToString(); }
}
