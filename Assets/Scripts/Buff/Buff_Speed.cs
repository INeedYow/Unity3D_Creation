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

        //
        dura = duration;
        target.buffSpeed += ratio;
        DungeonManager.instance.onChangeAnySpeed?.Invoke();

        // Effect effect = ObjectPool.instance.GetEffect((int)EEffect.Buff_ArmorInit);
        // effect.transform.position = target.targetTF.position;
        // effect.SetDuration(1f);

        // effect = ObjectPool.instance.GetEffect((int)EEffect.Buff_ArmorInit);
        // effect.transform.position = target.targetTF.position;
        // effect.SetDuration(1f);

        //Invoke("Finish", duration);
        StartCoroutine("Timer");
    }

    public override void Finish()
    {   //
        if (target != null){
            target.buffSpeed -= ratio;
            target.buffs.Remove(this);
        }
        
        //effect.Return();
        DungeonManager.instance.onChangeAnySpeed?.Invoke();
        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    IEnumerator Timer()
    {
        while (dura > 0f)
        {
            dura -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Finish();
    }
}
