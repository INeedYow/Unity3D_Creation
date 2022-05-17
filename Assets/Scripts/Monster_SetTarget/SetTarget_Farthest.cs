using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTarget_Farthest : SetTarget
{
    public SetTarget_Farthest(Character owner) : base(owner){}
    public override Character GetTarget()
    {
        return null;
    }
}
