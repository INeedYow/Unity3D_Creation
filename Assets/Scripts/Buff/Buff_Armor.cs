﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Armor : Buff
{
    private void Awake() { eBuff = EBuff.Armor; }
    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        ratio = buffRatio;

        //
        target.buffArmor += ratio;
        DungeonManager.instance.onChangeAnyArmor?.Invoke();

        // Effect effect = ObjectPool.instance.GetEffect((int)EEffect.Buff_ArmorInit);
        // effect.transform.position = target.targetTF.position;
        // effect.SetDuration(1f);

        effect = ObjectPool.instance.GetEffect((int)EEffect.Buff_ArmorInit);
        effect.transform.position = target.targetTF.position;
        effect.SetDuration(1f);

        Invoke("Finish", duration);
    }

    public override void Finish()
    {   //
        target.buffArmor -= ratio;
        effect.Return();
        DungeonManager.instance.onChangeAnyArmor?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
