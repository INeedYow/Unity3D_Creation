using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    protected Character owner;

    public void SetOwner(Character owner) { this.owner = owner; }

    public abstract bool SetOptionText1to3(int optionNumber, ItemOptionUnit optionUnit);

    public abstract void OnEquip();
    public abstract void OnUnEquip();
    public abstract void OnAttack(float damage);
    public abstract void OnDamagedGetAttacker(Character attacker);
    public abstract void OnKill();


}
