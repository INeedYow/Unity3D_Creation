using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Bleed : Buff
{
    Effect m_effect;

    private void Awake() { 
        eBuff = EBuff.Bleed; 
        isDebuff = true;
    }

    public override void Add(Character target, float duration, float buffRatio)
    {   // ratio를 데미지로
        if (target == null) return ;
        
        this.target = target;
        target.buffs.AddLast(this);
        
        target.debuffCount++;
        //
        ratio = buffRatio;
        dura = duration;

        // eff
        m_effect = ObjectPool.instance.GetEffect((int)EEffect.Bleed);
        m_effect.SetPosition(target);

        DungeonManager.instance.onChangeAnyBuff?.Invoke();
        target.onDead += Remove;

        StartCoroutine("Timer");
    }
    
    IEnumerator Timer()
    {
        float timer = 0f;
        while (timer < dura)
        {
            target.Damaged(ratio);
            timer += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        
        Finish();
    }


    public override void Finish()
    {   //Debug.Log("finish");
        if (target != null)
        {   
            target.debuffCount--;
            
            target.onDead -= Remove; 
            target.buffs.Remove(this);

            m_effect?.Return();

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
            
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;

            m_effect?.Return();

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
