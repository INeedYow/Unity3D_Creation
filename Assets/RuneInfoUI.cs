using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneInfoUI : MonoBehaviour
{
    [SerializeField]
    Rune rune;
    SkillRuneData skillData;
    public Text title;

    [Header("Stat")]
    public GameObject statPart;
    public Text curValue;
    public Text nextValue;

    [Header("Skill")]
    public GameObject skillPart;
    public Text cooldown;

    UIMouseTracker m_tracker;
    int m_value;


    private void Awake() {
        m_tracker = GetComponent<UIMouseTracker>();    
    }

    public void SetStatRune(Rune statRune)
    {
        statPart.SetActive(true);
        skillPart.SetActive(false);
        rune = statRune;

        title.text = rune.data.description;

        m_tracker.offset.y = -200f;

        RenewStatUI();
    }

    public void SetSkillRune(SkillRuneData data)
    {
        statPart.SetActive(false);
        skillPart.SetActive(true);
        skillData = data;

        title.text = data.description;

        m_tracker.offset.y = +200f;

        RenewSkillUI();
    }

    public void RenewStatUI()
    {
        curValue.text = rune.data.GetCurValue(rune.point).ToString();
        
    
        m_value = rune.data.GetNextValue(rune.point);

        if (m_value == 0){
            nextValue.text = "Max";
        }
        else {
            nextValue.text = rune.data.GetNextValue(rune.point).ToString();
        }
    }

    public void RenewSkillUI()
    {
        cooldown.text = skillData.GetMax().ToString();
    }
}
