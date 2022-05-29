using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoUnit : MonoBehaviour
{
    string m_tempStr;
    string m_myStr;
    Text m_myText;

    private void Awake() { 
        m_myText = GetComponentInChildren<Text>(); Debug.Log(m_myText.text);
        m_myStr = m_myText.text;    
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
