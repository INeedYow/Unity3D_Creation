using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem particle;

    private void Awake() {
        if (particle == null) particle = GetComponentInParent<ParticleSystem>();
    }

    private void OnEnable() { particle.Play(); }
    public void SetDuration(float duration) { Invoke("Return", duration); }

    public void Return()
    {   // 지속시간 동안 반복하는 이펙트랑 한 번 재생하고 사라지면 되는 이펙트 어떻게 구분해서 사용
        particle.Pause(); 
        ObjectPool.instance.ReturnObj(gameObject);
    }
}
