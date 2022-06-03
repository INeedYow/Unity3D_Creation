using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stasis : Buff
{
    private void Awake() {
        eBuff = EBuff.Stasis;
        isDebuff = false;
    }

    public override void Add(Character target, float duration, float buffRatio)
    {
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        
        //
        dura = duration;
        target.isStop = true;           // todo 어그로 풀리나 확인
        
        //target.onDead += Remove;      // 죽으면 안 되는 상태

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
            //target.onDead -= Remove; 
            target.buffs.Remove(this);
        }

        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public override void Remove()
    {   // 제거 불가능
        // StopCoroutine("Timer");

        // if (target != null)
        // {   
        //     target.buffFrozen -= ratio;
        //     //target.onDead -= Remove; 
        //     target.buffs.Remove(this);
        //     target = null;
        // }
        
        // ObjectPool.instance.ReturnObj(this.gameObject);
    }
}