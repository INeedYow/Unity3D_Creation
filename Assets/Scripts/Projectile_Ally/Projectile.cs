using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    float m_duration = 5f; 

    protected Character target;
    protected Vector3 lastPos;      // 중간에 타겟이 죽으면 그 때 가지고 있던 최종 위치까지 날아가고 사라지게 하고 싶어서
    protected float damage;
    protected float area;
    protected Vector3 dirVec;
}
