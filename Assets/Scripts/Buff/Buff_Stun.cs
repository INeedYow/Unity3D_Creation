using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stun : Buff
{
    private void Awake() { eBuff = EBuff.Stun; }
    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        
        //
        dura = duration;
        target.buffStun += duration;

        effect = ObjectPool.instance.GetEffect((int)eEffect);
        effect.transform.SetParent(target.transform);
        effect.transform.position = target.HpBarTF.position;
        target.onDead += Finish;

        //Invoke("Finish", duration);
        StartCoroutine("Timer");
    }

    public override void Finish()
    {   //Debug.Log("finish");
        if (target != null)
        {
            target.buffStun -= ratio; 
            target.buffs.Remove(this);
        }
        effect.Return();
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
