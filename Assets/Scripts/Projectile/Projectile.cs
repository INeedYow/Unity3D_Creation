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

    public void Launch(Character target, Character owner, float damage, float powerRate, float area)
    {
        m_target = target;
        m_owner = owner;
        m_damage = damage;
        m_powerRate = powerRate;
        m_area = area;

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
        
        if (m_sqrDist < 1.5f)
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

    void Hit(){
        if (null != m_target && !m_target.isStop)
        {   //Debug.Log("proj's owner : " + m_owner);
            IDamagable target = m_target.GetComponent<IDamagable>();
            target?.Damaged(m_damage, m_powerRate, m_owner, isMagic);
        }
        
        //if (m_target != null)
        //{   // 오브젝트 풀 적용 중인데 몬스터 삭제하면서 화살도 같이 사라져버려서 일단 주석처리
            //gameObject.transform.SetParent(m_target.transform);
        //}

        CancelInvoke("Return");
        Invoke("Return", remainTime);
    }

    void AreaHit()
    {
        if (eTargetGroup == EGroup.Monster)
        {
            foreach (Monster mon in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (mon.isDead || mon.isStop) continue;
                if (m_target == null) break;

                m_sqrDist = (m_target.transform.position - mon.transform.position).sqrMagnitude;

                if (m_area * m_area < m_sqrDist) continue;

                IDamagable tempTarget = mon.GetComponent<IDamagable>();
                
                m_target?.Damaged(m_damage, m_powerRate, m_owner, isMagic ? true : false);
            }
        }
        else
        {
            foreach (Hero hero in PartyManager.instance.heroParty)
            {
                if (hero.isDead || hero.isStop) continue;
                if (m_target == null) break;

                m_sqrDist = (m_target.transform.position - hero.transform.position).sqrMagnitude;

                if (m_area * m_area < m_sqrDist) continue;

                IDamagable tempTarget = hero.GetComponent<IDamagable>();
                
                m_target?.Damaged(m_damage, m_powerRate, m_owner, isMagic ? true : false);
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

        //gameObject.transform.SetParent(m_target.transform); //
        
        CancelInvoke("Return");
        Invoke("Return", remainTime);
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
        ObjectPool.instance.ReturnObj(gameObject);
    }
}
