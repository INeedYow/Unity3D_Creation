using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    float m_duration = 5f; 

    protected Character target;
    protected float damage;
    protected float area;
    protected Vector3 dirVec;
}
