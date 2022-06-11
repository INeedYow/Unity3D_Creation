using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillObject : MonoBehaviour
{
    public SkillObjectData data;
    public Skill skill;
    protected Buff buff;
    protected Effect eff;

    private void Awake() { gameObject.SetActive(false); }
    private void OnEnable() { 
        StartEffect();
        Works(); 
    }

    void StartEffect()
    {
        if (data.eStartEffect == EEffect.None) return;

        eff = ObjectPool.instance.GetEffect((int)data.eStartEffect);
        eff.SetPosition(skill.owner);
    }

    public abstract void Works();

    protected void AddBuff(Character target)
    {   // buff None 이거나 잘못된 값이면 null 받아옴
        if (target.isDead) return;                                  // 데미지 준 후라서 죽었으면 줄 필요없음
        buff = ObjectPool.instance.GetBuff((int)data.eBuff);
        buff?.Add(target, data.duration, data.buffRatio);
    }

    protected void FinishWorks() { gameObject.SetActive(false); }

}