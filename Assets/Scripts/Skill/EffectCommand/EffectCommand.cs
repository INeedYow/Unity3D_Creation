using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectCommand
{
    public Skill skill;
    
    public abstract void Works();

    public EffectCommand(Skill skill) { this.skill = skill; }
}
