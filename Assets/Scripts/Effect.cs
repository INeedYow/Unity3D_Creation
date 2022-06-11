using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem particle;

    [Range(0f, 30f)]public float duration;
    public bool isFollowOwner = false;

    [Tooltip("0 : Ground / 1 : Mid / 2 : Over Head")]
    [Range(0, 2)] public int heightTF = 0;


    private void Awake() {
        if (particle == null) particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable() 
    { 
        particle.Play(); 

        if (duration != 0f)
        {
            SetDuration(duration);
        }
    }
    
    public void SetPosition(Character owner)
    {
        if (owner == null) 
        {
            transform.SetParent(null);
            return;
        }

        switch (heightTF)
        {
            case 0: transform.position = owner.transform.position;  break;
            case 1: transform.position = owner.targetTF.position;   break;
            case 2: transform.position = owner.HpBarTF.position;    break;
        }

        if (isFollowOwner)
        {   
            transform.SetParent(owner.transform);
            owner.onDeadGetThis += NullOwner;
        }
    }

    public void SetDuration(float duration) { Invoke("Return", duration); }

    public void Return()
    {   
        particle.Stop();

        if (isFollowOwner)
        {
            transform.SetParent(null);
        }
        
        ObjectPool.instance.ReturnObj(gameObject);
    }

    public void NullOwner(Character owner)
    {
        owner.onDeadGetThis -= NullOwner;
        CancelInvoke("Return");
        Return();
    }
}
