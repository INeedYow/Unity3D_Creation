using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCommand_Projectile : EffectCommand
{
    public EProjectile eProj;
    public EffectCommand_Projectile(Skill skill, EProjectile eProjectile) 
        : base(skill)
    { eProj = eProjectile; }

    public override void Works(){
        // 
    }

    
}