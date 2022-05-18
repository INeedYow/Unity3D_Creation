using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public EGroup eOwnerGroup;
    public float speed;
    public bool isMagic;
    public float remainTime;      // 도착 후 잔존시간

    bool        m_isLaunch;
    float       m_damage;
    float       m_powerRate;
    float       m_area;
    Character   m_target;
    Vector3     m_lastPos;      // 중간에 타겟이 죽으면 그 때 가지고 있던 최종 위치까지 날아가고 사라지게
    float       m_sqrDist;

    public void Launch(Character target, float damage, float powerRate, float area)
    {
        m_target = target;
        m_damage = damage;
        m_powerRate = powerRate;
        m_area = area;

        InvokeRepeating("LookTarget", 0f, 0.2f);
        m_isLaunch = true;
    }

    private void Update() {
        if (!m_isLaunch) return;
        Move(); 
    }

    private void FixedUpdate() {
        if (!m_isLaunch) return;
        m_sqrDist = (transform.position - m_lastPos).sqrMagnitude;
        
        if (m_sqrDist < 1.5f)
        {   
            m_isLaunch = false;
            if (m_area != 0f)   { AreaHit(); }
            else                { Hit(); }
        }
    }

    void Move(){
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    void Hit(){
        if (null != m_target)
        {
            IDamagable target = m_target.GetComponent<IDamagable>();
            target?.Damaged(m_damage, m_powerRate, isMagic);
        }
        
        if (m_target != null)
        {
            gameObject.transform.SetParent(m_target.transform);
        }
        Destroy(gameObject, remainTime);
    }

    void AreaHit()
    {
        Collider[] colls;
        if (eOwnerGroup == EGroup.Ally)
        {
            colls = Physics.OverlapSphere(transform.position, m_area, LayerMask.GetMask("Monster"));
        }
        else{
            colls = Physics.OverlapSphere(transform.position, m_area, LayerMask.GetMask("Hero"));
        }

        foreach (Collider coll in colls)
        {
            IDamagable damagableTarget = coll.gameObject.GetComponent<IDamagable>();
            damagableTarget?.Damaged(m_damage, m_powerRate);
        }

        gameObject.transform.SetParent(m_target.transform);
        Destroy(gameObject, remainTime);
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
}
