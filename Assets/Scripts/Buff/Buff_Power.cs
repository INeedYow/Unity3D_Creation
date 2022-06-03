using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Power : Buff
{
    private void Awake() { eBuff = EBuff.Power; }
    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        ratio = buffRatio;

        isDebuff = buffRatio < 0f;
        //
        dura = duration;
        target.powerRate += ratio;

        target.onDead += Remove;

        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dura);

        Finish();
    }
    
    public override void Finish()
    {   
        if (target != null)
        {   
            target.powerRate -= ratio; 
            target.onDead -= Remove; 
            target.buffs.Remove(this);
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {   
            target.powerRate -= ratio;
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
