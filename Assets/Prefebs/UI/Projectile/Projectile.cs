using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public EGroup eTargetGroup;
    public float speed;
    public bool isMagic;
    public float remainTime;        // 도착 후 잔존시간

    Character   m_owner;
    bool        m_isLaunch;
    float       m_damage;           // 중간에 오너가 죽어도 데미지 줄 수 있게 받아 놓음
    float       m_powerRate;
    float       m_area;
    Character   m_target;
    Vector3     m_lastPos;          // 중간에 타겟이 죽으면 그 때 가지고 있던 최종 위치까지 날아가고 사라지게
    float       m_sqrDist;

    EEffect     m_eEffect;
    EBuff       m_eBuff;
    float       m_buffDura;
    float       m_buffRatio;

    public void Launch(Character target, Character owner, float damage, float powerRate, float area, EEffect eEffect = EEffect.None, 
        EBuff eBuff = EBuff.None, float buffDuration = 0f, float buffRatio = 0f)
    {
        m_target = target;
        m_owner = owner;
        m_damage = damage;
        m_powerRate = powerRate;
        m_area = area;
        m_eEffect = eEffect;
        m_eBuff = eBuff;
        m_buffDura = buffDuration;
        m_buffRatio = buffRatio;

        InvokeRepeating("LookTarget", 0f, 0.2f);
        m_isLaunch = true;
        Invoke("Return", 10f);
    }

    private void Update() {
        if (!m_isLaunch) return;
        Move(); 
    }

    private void FixedUpdate() { // 타겟과의 거리로 맞았는지 판정
        if (!m_isLaunch) return;
        m_sqrDist = (transform.position - m_lastPos).sqrMagnitude;
        
        if (m_sqrDist < 0.5f)
        {   
            m_isLaunch = false;
            if (m_area != 0f)   { AreaHit(); }
            else                { Hit(); }
        }
    }

    // 처음에 Space.World로 안 해줬더니 휘어서 날아감
    void Move(){
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void Hit()
    {
        if (null != m_target && !m_target.isStop && !m_target.isDead)
        {   //Debug.Log("proj's owner : " + m_owner);
            m_target.Damaged(m_damage, m_powerRate, m_owner, isMagic);

            if (m_eEffect != EEffect.None)
            {
                ObjectPool.instance.GetEffect((int)m_eEffect).SetPosition(m_target);
            }
        }
        else{
            CancelInvoke("Return");
            Return();
            return;
        }

        if (m_target.isDead)
        {
            CancelInvoke("Return");
            Return();
            return;
        }

        if (m_eBuff != EBuff.None)
        {
            ObjectPool.instance.GetBuff((int)m_eBuff).Add(m_target, m_buffDura, m_buffRatio);
        }

        CancelInvoke("Return");

        if (remainTime > 0f)
        {
            m_target.onDeadGetThis += StopRemain;           //Debug.Log("Add Event StopRemain()");
            gameObject.transform.SetParent(m_target.transform);
            Invoke("Return", remainTime);
        }
        else{
            Return();
        }
    }

    void StopRemain(Character owner)
    {   // 잔존 중 대상이 사망한 경우
        owner.onDeadGetThis -= StopRemain;
        CancelInvoke("Return");                             
        Return();
    }

    void AreaHit()
    {
        if (m_eEffect != EEffect.None)
        {
            Effect eff = ObjectPool.instance.GetEffect((int)m_eEffect);
            eff.SetPosition(m_target);
        }

        if (eTargetGroup == EGroup.Monster)
        {
            foreach (Monster mon in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (mon.isDead || mon.isStop) continue;

                m_sqrDist = (m_lastPos - mon.transform.position).sqrMagnitude;

                if (m_area * m_area < m_sqrDist) continue;
                
                mon.Damaged(m_damage, m_powerRate, m_owner, isMagic ? true : false);

                if (mon.isDead) continue;

                if (m_eBuff != EBuff.None)
                {
                    Buff buff = ObjectPool.instance.GetBuff((int)m_eBuff);
                    buff.Add(mon, m_buffDura, m_buffRatio);
                }
            }
        }
        else
        {
            foreach (Hero hero in PartyManager.instance.heroParty)
            {
                if (hero.isDead || hero.isStop) continue;

                m_sqrDist = (m_lastPos - hero.transform.position).sqrMagnitude;

                if (m_area * m_area < m_sqrDist) continue;
                
                hero.Damaged(m_damage, m_powerRate, m_owner, isMagic ? true : false);

                if (hero.isDead) continue;

                if (m_eBuff != EBuff.None)
                {
                    Buff buff = ObjectPool.instance.GetBuff((int)m_eBuff);
                    buff.Add(hero, m_buffDura, m_buffRatio);
                }
            }
        }

        


        // Collider[] colls;
        // if (eTargetGroup == EGroup.Monster)
        // {
        //     colls = Physics.OverlapSphere(transform.position, m_area, LayerMask.GetMask("Monster"));
        // }
        // else{
        //     colls = Physics.OverlapSphere(transform.position, m_area, LayerMask.GetMask("Hero"));
        // }

        // foreach (Collider coll in colls)
        // {
        //     IDamagable damagableTarget = coll.gameObject.GetComponent<IDamagable>();
        //     damagableTarget?.Damaged(m_damage, m_powerRate, m_owner, isMagic);
        // }

        if (m_target.isDead || m_target.isStop)
        {
            CancelInvoke("Return");
            Return();
            return;
        }

        CancelInvoke("Return");

        if (remainTime > 0f)
        {
            m_target.onDeadGetThis += StopRemain;               
            gameObject.transform.SetParent(m_target.transform);
            Invoke("Return", remainTime);
        }
        else{
            Return();
        }
    }
    

    void LookTarget()
    {
        if (null != m_target)
        {
            m_lastPos = m_target.targetTF.position;
            transform.LookAt(m_lastPos);    
        }
        else{
            StopCoroutine("LookTarget");
        }
    }

    void Return(){
        ObjectPool.instance.ReturnObj(this.gameObject);
    }
}
