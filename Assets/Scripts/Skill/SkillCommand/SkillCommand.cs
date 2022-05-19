using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillCommand
{
    public Skill skill;
    public float lastSkillTime;
    public bool isUsing;

    public abstract bool Use();

    public SkillCommand(Skill skill) { this.skill = skill; }
}
