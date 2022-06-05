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

        isDebuff = buffRatio < 0f;
        if (isDebuff) { target.debuffCount++; }
        else { target.buffCount++; }
        //
        dura = duration;
        target.buffSpeed += ratio;

        DungeonManager.instance.onChangeAnySpeed?.Invoke();
        target.onDead += Remove;

        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dura);

        Finish();
    }

    public override void Finish()
    {   //
        if (target != null)
        {
            target.buffSpeed -= ratio;
            if (isDebuff) { target.debuffCount--; }
            else { target.buffCount--; }

            target.onDead -= Remove; 
            target.buffs.Remove(this);
        }
        
        //effect.Return();
        DungeonManager.instance.onChangeAnySpeed?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {   
            target.buffSpeed -= ratio;
            if (isDebuff) { target.debuffCount--; }
            else { target.buffCount--; }
            
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;
        }
        
        DungeonManager.instance.onChangeAnySpeed?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
