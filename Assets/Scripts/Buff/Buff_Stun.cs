using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stun : Buff
{
    private void Awake() { eBuff = EBuff.Stun; }
    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        
        //
        ratio = duration;
        target.buffStun += duration;

        effect = ObjectPool.instance.GetEffect((int)eEffect);
        effect.transform.SetParent(target.transform);
        effect.transform.position = target.HpBarTF.position;
        target.onDead += Finish;

        Invoke("Finish", duration);
    }

    public override void Finish()
    {   //
        if (target != null)
        {
            target.buffStun -= ratio; 
            target.buffs.Remove(this);
        }
        effect.Return();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
