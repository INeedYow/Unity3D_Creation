using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Self : ConditionMacro
{   
    public enum EInfo { None, HP };         // None : 항상 자기자신, HP : 자신의 체력
    public enum EType { Lower, Higher };      //  %
    public EInfo eInfo;
    public EType eType;
    [Tooltip("HpRate")] [Range(0f, 100f)] public float value;
    bool m_isSatisfy;
    
    private void Start() {
        if (eInfo == EInfo.HP){
            owner.onHpChange += UpdateHp;
        }
    }

    private void OnDestroy() {
        owner.onHpChange -= UpdateHp;
    }

    public override bool IsSatisfy(){
        target = owner;
        if (eInfo == EInfo.None)
        {
            return true;
        }
        else{
           return m_isSatisfy;
        }
    }

    void UpdateHp(){
        switch (eType){
        case EType.Lower:       m_isSatisfy = owner.curHp / owner.maxHp * 100f <= value; break;
        case EType.Higher:      m_isSatisfy = owner.curHp / owner.maxHp * 100f >= value; break;
        }
    }
}
