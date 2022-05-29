using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Speed : Buff
{
    private void Awake() { eBuff = EBuff.Speed; }

    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        ratio = buffRatio;

        //
        target.buffSpeed += ratio;
        DungeonManager.instance.onChangeAnySpeed?.Invoke();

        // Effect effect = ObjectPool.instance.GetEffect((int)EEffect.Buff_ArmorInit);
        // effect.transform.position = target.targetTF.position;
        // effect.SetDuration(1f);

        // effect = ObjectPool.instance.GetEffect((int)EEffect.Buff_ArmorInit);
        // effect.transform.position = target.targetTF.position;
        // effect.SetDuration(1f);

        Invoke("Finish", duration);
    }

    public override void Finish()
    {   //
        target.buffSpeed -= ratio;
        //effect.Return();
        DungeonManager.instance.onChangeAnySpeed?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
