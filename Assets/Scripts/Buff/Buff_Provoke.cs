using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Provoke : Buff
{   // 이러면 함수 호출이 안됨
    Character m_provoker;

    private void Awake() { eBuff = EBuff.Provoke; }
    public override void Add(Character target, float duration, float buffRatio) {}

    public void Add(Character target, Character provoker, float duration)
    {
        if (target == null) return;

        this.target = target;
        target.buffs.AddLast(this);

        dura = duration;
        m_provoker = provoker;
        target.provoker = provoker;
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
            if (target.provoker == m_provoker)
            {   // 혹시 다른 대상에게 또 도발 당했는데 지워버리면 안 되니까
                target.provoker = null;
            }
            target.buffs.Remove(this);
            target = null;
        }

        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public override void Remove()
    {
        StopCoroutine("Timer");

        if (target != null)
        {
            if (target.provoker == m_provoker)
            {   
                target.provoker = null;
            }
            target.buffs.Remove(this);
            target = null;
        }
        
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
