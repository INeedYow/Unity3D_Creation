using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public float curCooldown;
    public float cooldown;

    public abstract void Use();
}