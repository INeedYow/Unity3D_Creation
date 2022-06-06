using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_FindNearHero : RuneSkillCursor
{   
    [Header("RuneCursor_Target")]
    Character _target;
    public Character target{
        get { return _target; }
        set {
            if (_target != null)
            {
                _target.transform.localScale = normalScale;
                //focusedEff.SetPosition(null);
                //focusedEff.gameObject.SetActive(false);
                _target.onDead -= NullTarget;
                _target.onUntouchableGetThis -= NullTarget;
            }

            _target = value;

            if (_target != null)
            {   //Debug.Log("Target " + _target.name);
                _target.transform.localScale = focusedScale;
                //focusedEff.SetPosition(_target);
                //focusedEff.gameObject.SetActive(true);
                _target.onDead += NullTarget;
                _target.onUntouchableGetThis += NullTarget;
            }
        }
    }
    Vector3 normalScale = new Vector3(2f, 2f, 2f);
    Vector3 focusedScale = new Vector3(2.8f, 2.8f, 2.8f);
    Character m_tempTarget;

    //public Effect focusedEff;

    float m_sqrDist;
    float m_min;
    float m_lastTime;

    void NullTarget()               { target = null; }
    void NullTarget(Character ch)   { NullTarget(); }

    protected override void CursorPosition() 
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("RunePlane")))
        {   
            transform.position = hit.point;
        }

        GetNearestHero();
    }

    void GetNearestHero()
    {
        if (Time.time < m_lastTime + 0.1f) return;

        m_lastTime = Time.time;

        m_min = Mathf.Infinity;

        foreach (Character ch in PartyManager.instance.heroParty)
        {
            if (ch.isDead || ch.isStop) continue;

            m_sqrDist = (ch.transform.position - transform.position).sqrMagnitude;

            if (m_min > m_sqrDist)
            {
                m_min = m_sqrDist;
                target = ch;
            }
        }
    }
        
    
    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0) && target != null)
        {   //
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("RunePlane")))
            {   
                if (skillObj is RuneSkillObj_TargetBuff)
                {   
                    m_tempTarget = target;
                    target = null;

                    (skillObj as RuneSkillObj_TargetBuff).SetTarget(m_tempTarget);
                    skillObj.gameObject.SetActive(true);
                    //target = null;

                    onWorks?.Invoke();
                    gameObject.SetActive(false);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {   
            target = null;
            Cancel();
        }
    }
}
