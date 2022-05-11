using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Self : ConditionMacro
{   // 다른 조건 추가 안 될 경우 EInfo는 지워도 될듯
    public enum EInfo { None, HP };         // None : 항상 자기자신, HP : 자신의 체력
    public enum EType { Least, Most };      // 최소, 최대 %
    public EInfo eInfo;
    public EType eType;
    public float value;
    
    public Condition_Self(string desc, Hero hero, EInfo eInfo, EType eType, float value) : base(desc, hero) 
    {
        this.eInfo = eInfo;
        this.eType = eType;
        this.value = value;
    }

    public override bool IsSatisfy(){
        //Debug.Log("IsSatisfy()");
        if (eInfo == EInfo.None)
        {
            owner.target = owner;
        }
        else if (eInfo == EInfo.HP)
        {
            //Debug.Log("eInfo == EInfo.HP");
            switch (eType)
            {
            case EType.Least:
                return owner.curHp / owner.maxHp * 100f >= value;

            case EType.Most:
                return owner.curHp / owner.maxHp * 100f <= value;
            }
        }
        return false;
    }
}
