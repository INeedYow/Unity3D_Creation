using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem particle;
    [Range(0f, 10f)]public float duration;

    private void Awake() {
        if (particle == null) particle = GetComponentInParent<ParticleSystem>();
    }

    private void OnEnable() 
    { 
        particle.Play(); 

        if (duration != 0f)
        {
            SetDuration(duration);
        }
    }

    public void SetDuration(float duration) { Invoke("Return", duration); }

    public void Return()
    {   
        particle.Stop();
        ObjectPool.instance.ReturnObj(gameObject);
    }
}
