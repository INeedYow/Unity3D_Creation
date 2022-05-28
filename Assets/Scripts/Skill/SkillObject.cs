﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillObject : MonoBehaviour
{
    public SkillObjectData data;
    public Skill skill;
    protected Buff buff;

    private void Awake() { gameObject.SetActive(false); }
    private void OnEnable() { SkillWorks(); }

    void SkillWorks() { // 범위 공격에 광역 스턴이 이렇게 하면 안 됨
        Works();
        AddBuff();
        FinishWorks();
    }

    public abstract void Works();

    void AddBuff()
    {   // buff None 이거나 잘못된 값이면 null 받아옴
        buff = ObjectPool.instance.GetBuff((int)data.eBuff);
        buff?.Add(skill.owner.target, data.duration, data.buffRatio);
    }

    protected void FinishWorks() { gameObject.SetActive(false); }

}