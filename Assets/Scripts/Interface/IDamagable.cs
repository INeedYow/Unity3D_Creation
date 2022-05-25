using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Damaged(float damage, float damageRate, Character attacker, bool isMagic = false, bool isThorns = false);
    void Healed(float heal);

    void Death();
}
