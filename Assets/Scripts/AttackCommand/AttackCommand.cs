using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCommand
{
    protected Character owner;
    protected Character target;

    public AttackCommand(Character owner)
    {
        this.owner = owner;
    }
    public abstract void Attack();
    public void SetTarget(Character target)
    {
        this.target = target;
    }
}
