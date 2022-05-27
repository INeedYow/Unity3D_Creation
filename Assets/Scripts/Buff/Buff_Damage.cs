using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Damage : Buff
{
    private void Awake() { eBuff = EBuff.Damage; }
    public override void Add(Character target, float duration, float buffRatio)
    {
        this.target = target;
        target.buffs.AddLast(this);
        ratio = buffRatio;
        target.buffDamage += ratio;
        
        DungeonManager.instance.onChangeAnyPower?.Invoke();

        Invoke("Finish", duration);
    }

    public override void Finish()
    {
        target.buffDamage -= ratio;
        DungeonManager.instance.onChangeAnyPower?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}