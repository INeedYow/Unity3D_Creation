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
                //_target.transform.localScale = normalScale;

                _target.onDead -= NullTarget;
                _target.onUntouchableGetThis -= NullTarget;
            }

            _target = value;

            if (_target != null)
            {   //Debug.Log("Target " + _target.name);
                //_target.transform.localScale = focusedScale;

                _target.onDead += NullTarget;
                _target.onUntouchableGetThis += NullTarget;
            }
        }
    }

    //Vector3 normalScale = new Vector3(2f, 2f, 2f);
    //Vector3 focusedScale = new Vector3(2.8f, 2.8f, 2.8f);

    public LineRenderer line;
    Vector3[] m_vecArr = new Vector3[2];

    float m_sqrDist;
    float m_min;
    float m_lastTime;


    private void Start() {
        if (line == null) line = GetComponent<LineRenderer>();

        if (line != null)
        {
            line.startWidth = 0.7f;
            line.endWidth = 0.35f;
        }
        
    }

    void NullTarget()               { target = null; }
    void NullTarget(Character ch)   { NullTarget(); }

    protected override void CursorPosition() 
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("RunePlane")))
        {   
            transform.position = hit.point;

            if (target != null)
            {
                m_vecArr[0] = hit.point;
                m_vecArr[1] = target.transform.position;
                line.SetPositions(m_vecArr);
            }
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
                    (skillObj as RuneSkillObj_TargetBuff).SetTarget(target);
                    skillObj.gameObject.SetActive(true);
                    target = null;

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
