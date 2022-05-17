using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SetTarget
{
    public Character owner;
    protected Character target;
    public abstract Character GetTarget();

    public SetTarget(Character owner)
    {
        this.owner = owner;
    }
}
