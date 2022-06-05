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
        target.debuffCount++;
        
        //
        dura = duration;
        target.buffStun += dura;
        target.onDead += Remove;

        DungeonManager.instance.onChangeAnyBuff?.Invoke();
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
            target.debuffCount--;
            target.onDead -= Remove; 
            target.buffs.Remove(this);

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
        }

        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {   
            target.buffStun -= dura;
            target.debuffCount--;
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
