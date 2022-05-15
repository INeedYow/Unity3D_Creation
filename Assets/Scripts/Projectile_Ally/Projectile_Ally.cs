using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile_Ally : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed;
    public float duration;

    public abstract void Fire();
}
