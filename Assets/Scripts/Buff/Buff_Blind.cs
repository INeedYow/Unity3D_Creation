using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Blind : Buff
{
    Effect m_effect;

    private void Awake() { 
        eBuff = EBuff.Blind; 
        isDebuff = true;
    }

    public override void Add(Character target, float duration, float buffRatio)
    {   // ratio를 데미지로
        if (target == null ) return;
        
        this.target = target;       
        target.buffs.AddLast(this);
        
        target.debuffCount++;

        dura = duration;
        ratio = target.attackRange - 3;     //Debug.Log("ratio : " + ratio);
        target.attackRange -= ratio;        //Debug.Log("range : " + target.attackRange);
        //target.ResetNavDistance();

        // eff
        m_effect = ObjectPool.instance.GetEffect((int)EEffect.Blind);
        m_effect.SetPosition(target);

        DungeonManager.instance.onChangeAnyBuff?.Invoke();
        DungeonManager.instance.onChangeAnyRange?.Invoke();
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
            target.attackRange += ratio;
            //target.ResetNavDistance();

            target.onDead -= Remove; 
            target.buffs.Remove(this);

            m_effect?.Return();

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
            DungeonManager.instance.onChangeAnyRange?.Invoke();
        }

        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {   
            target.debuffCount--;
            target.attackRange += ratio;
            //target.ResetNavDistance();
            
            target.onDead -= Remove; 
            target.buffs.Remove(this);
            target = null;

            m_effect?.Return();

            DungeonManager.instance.onChangeAnyBuff?.Invoke();
            DungeonManager.instance.onChangeAnyRange?.Invoke();
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
