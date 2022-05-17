using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Self : ConditionMacro
{   
    public enum EInfo { None, HP };         // None : 항상 자기자신, HP : 자신의 체력
    public enum EType { Least, Most };      // 최소, 최대 %
    public EInfo eInfo;
    public EType eType;
    [Tooltip("value (0f ~ 100f)")] public float value;
    
    private void Start() {
        if (eInfo == EInfo.HP)
        {
            owner.onHpChange += UpdateHp;
        }
    }

    private void OnDestroy() {
        owner.onHpChange -= UpdateHp;
    }

    public override bool IsSatisfy(){
        if (eInfo == EInfo.None)
        {
            owner.target = owner;
            return true;
        }
        else// if (eInfo == EInfo.HP)   // 더 추가되면 else if
        {
           return isSatisfy;
        }
    }

    void UpdateHp(){
        switch (eType){
        case EType.Least:   isSatisfy = owner.curHp / owner.maxHp * 100f >= value; break;
        case EType.Most:    isSatisfy = owner.curHp / owner.maxHp * 100f <= value; break;
        }
    }

}
