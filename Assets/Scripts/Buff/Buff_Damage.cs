using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Damage : Buff
{
    private void Awake() { eBuff = EBuff.Damage; }
    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null || target.isDead) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        ratio = buffRatio;

        isDebuff = buffRatio < 0f;
        if (isDebuff) { target.debuffCount++; }
        else { target.buffCount++; }
        //
        dura = duration;
        target.buffDamage += ratio;

        DungeonManager.instance.onChangeAnyDamage?.Invoke();
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
            target.buffDamage -= ratio; 
            if (isDebuff) { target.debuffCount--; }
            else { target.buffCount--; }

            target.onDead -= Remove; 
            target.buffs.Remove(this);

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
            DungeonManager.instance.onChangeAnyDamage?.Invoke();
        }
        
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {   
            target.buffDamage -= ratio;
            if (isDebuff) { target.debuffCount--; }
            else { target.buffCount--; }
            
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
            DungeonManager.instance.onChangeAnyDamage?.Invoke();
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}