using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Damaged(float damage, float damageRate, bool isMagic = false);

    void Death();
}
