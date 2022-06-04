using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_MagicArmor : Buff
{
    private void Awake() { eBuff = EBuff.MagicArmor; }
    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        ratio = buffRatio;

        isDebuff = buffRatio < 0f;
        //
        dura = duration;
        target.buffMagicArmor += ratio;

        DungeonManager.instance.onChangeAnyMagicArmor?.Invoke();
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
            target.buffMagicArmor -= ratio;
            target.onDead -= Remove; 
            target.buffs.Remove(this);
        }
        
        DungeonManager.instance.onChangeAnyMagicArmor?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
    
    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {   
            target.buffMagicArmor -= ratio;
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;
        }
        
        DungeonManager.instance.onChangeAnyMagicArmor?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
