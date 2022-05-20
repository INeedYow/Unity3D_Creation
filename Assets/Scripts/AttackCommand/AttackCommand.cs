using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCommand
{
    protected Character owner;

    public AttackCommand(Character owner)
    {
        this.owner = owner;
    }
    public abstract void Attack();
}
