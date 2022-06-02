using System.Collections;
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
        dura = duration;
        target.buffArmor += ratio;

        DungeonManager.instance.onChangeAnyArmor?.Invoke();
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
            target.buffArmor -= ratio;
            target.onDead -= Remove; 
            target.buffs.Remove(this);
        }
        
        DungeonManager.instance.onChangeAnyArmor?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
    
    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {   
            target.buffArmor -= ratio;
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;
        }
        
        DungeonManager.instance.onChangeAnyArmor?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }

}
