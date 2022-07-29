using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUnit : MonoBehaviour
{
    public string m_myStr;
    string m_tempStr;
    Text m_myText;

    private void Awake() { 
        m_myText = GetComponentInChildren<Text>(); 
    }

    private void OnEnable() {
        m_myText.text = m_myStr;
    }

    public void ShowString()
    {
        m_tempStr = m_myText.text;
        m_myText.text = m_myStr;
    }

    public void ShowValue()
    {
        m_myText.text = m_tempStr;
    }
}
