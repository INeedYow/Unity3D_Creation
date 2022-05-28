using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillObject : MonoBehaviour
{
    public SkillObjectData data;
    public Skill skill;
    protected Buff buff;

    private void Awake() { gameObject.SetActive(false); }
    private void OnEnable() { SkillWorks(); }

    void SkillWorks() { 
        Works();
        //AddBuff();        // 이렇게 하면 범위 공격에 광역 스턴이 안 됨
        FinishWorks();
    }

    public abstract void Works();

    protected void AddBuff(Character target)
    {   // buff None 이거나 잘못된 값이면 null 받아옴
        buff = ObjectPool.instance.GetBuff((int)data.eBuff);
        buff?.Add(target, data.duration, data.buffRatio);
    }

    protected void FinishWorks() { gameObject.SetActive(false); }

}