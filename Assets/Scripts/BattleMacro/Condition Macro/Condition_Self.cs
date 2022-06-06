using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EType { Lower, Higher, Equal, };      //  %
public class Condition_Self : ConditionMacro
{   
    public enum EInfo { None, HP };         // None : 항상 자기자신, HP : 자신의 체력
    public EInfo eInfo;
    public EType eType;
    [Tooltip("HpRate")] [Range(0f, 100f)] public float value;
    bool m_isSatisfy;
    bool m_hasChanged;


    private void Start() {
        DungeonManager.instance.onWaveStart += OnBattle;
        DungeonManager.instance.onWaveEnd += OffBattle;
    }

    void OnBattle()     { owner.onHpChange += HpChange; }
    void OffBattle()    { owner.onHpChange -= HpChange; }
    
    void HpChange()     { m_hasChanged = true; }

    public override bool IsSatisfy(){
        target = owner;
        if (eInfo == EInfo.None)
        {
            return true;
        }
        else{

            if (m_hasChanged) UpdateHp();

            return m_isSatisfy;
        }
    }

    void UpdateHp()
    {
        switch (eType){
        case EType.Lower:       m_isSatisfy = owner.curHp / owner.maxHp * 100f <= value; break;
        case EType.Higher:      m_isSatisfy = owner.curHp / owner.maxHp * 100f >= value; break;
        case EType.Equal:       m_isSatisfy = Mathf.Approximately(owner.curHp / owner.maxHp * 100f, value); break;
        }
        m_hasChanged = false;
    }
}
