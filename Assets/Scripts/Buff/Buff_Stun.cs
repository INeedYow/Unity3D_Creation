using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stun : Buff
{
    private void Awake() { 
        eBuff = EBuff.Stun; 
        isDebuff = true;
    }
    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        
        //
        dura = duration;
        target.buffStun += dura;
        target.onDead += Remove;

        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dura);

        Finish();
    }


    public override void Finish()
    {   //Debug.Log("finish");
        if (target != null)
        {
            target.buffStun -= dura; 
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
            target.buffStun -= dura;
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
