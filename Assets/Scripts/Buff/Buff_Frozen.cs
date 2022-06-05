using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Frozen : Buff
{
    private void Awake() { 
        eBuff = EBuff.Frozen; 
        isDebuff = true;
    }

    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        
        target.debuffCount++;
        //
        ratio = buffRatio;
        dura = duration;
        target.buffFrozen += ratio;

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
    {   //Debug.Log("finish");
        if (target != null)
        {   
            target.buffFrozen -= ratio; 
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
            target.buffFrozen -= ratio;
            target.debuffCount--;
            
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}