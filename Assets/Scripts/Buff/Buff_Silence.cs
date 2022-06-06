using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Silence : Buff
{
    private void Awake() { 
        eBuff = EBuff.Silence; 
        isDebuff = true;
    }

    public override void Add(Character target, float duration, float buffRatio)
    {   
        if (target == null || target.isDead) return ;
        
        this.target = target;       
        target.buffs.AddLast(this);
        
        target.debuffCount++;

        dura = duration;
        target.silence++;

        DungeonManager.instance.onChangeAnyBuff?.Invoke();
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
            target.debuffCount--;
            target.silence--;

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
            target.debuffCount--;
            target.silence--;
            
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
