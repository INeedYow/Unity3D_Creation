using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_CharacterCount : ConditionMacro
{
    public EGroup eCountGroup;
    [Range(0, 30)] public int count;
    [Tooltip("> < ==")]
    public EType eType;
    
    bool m_hasSomeoneDead;
    bool m_isSatisfy;
    int m_count;

    private void OnEnable() { DungeonManager.instance.onSomeoneDead += SomeoneDead; }

    void SomeoneDead() { m_hasSomeoneDead = true; }

    public override bool IsSatisfy()
    {
        if (m_hasSomeoneDead) { CheckCount(); }

        return m_isSatisfy;
    }

    void CheckCount()
    {
        m_count = 0;

        if (eCountGroup == EGroup.Hero)
        {
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead) continue;
                m_count++;
            }
        }
        else
        {
            m_count = DungeonManager.instance.curDungeon.curMonsterCount;
        }


        if (eType == EType.Lower)
        {
            m_isSatisfy = count > m_count;
        }
        else if (eType == EType.Higher)
        {
            m_isSatisfy = count < m_count;
        }
        else
        {
            m_isSatisfy = count == m_count;
        }
    }

}
