using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand_Projectile : SkillCommand
{
    public Projectile projectile;
    public override bool Use()
    {
        return false;
    }
}
