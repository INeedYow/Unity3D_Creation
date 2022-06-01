using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneSkillObject : MonoBehaviour
{
    public RuneSkillObjectData data;
    // cursor
    protected Buff buff;
    protected Effect eff;

    private void OnEnable() {
        WorldEffect();
        SkillWorks();
    }

    void SkillWorks() { 
        Works();
        FinishWorks();
    }

    void WorldEffect()
    {
        if (data.eWorldEffect == EEffect.None) return;
        
        eff = ObjectPool.instance.GetEffect((int)data.eWorldEffect);
        eff.transform.position = DungeonManager.instance.worldEffectTF.position;
    }

    public abstract void Works();

    protected void AddBuff(Character target)
    {
        buff = ObjectPool.instance.GetBuff((int)data.eBuff);
        buff?.Add(target, data.duration, data.buffRatio);
    }

    protected void FinishWorks()
    {
        gameObject.SetActive(false);
    }

}
