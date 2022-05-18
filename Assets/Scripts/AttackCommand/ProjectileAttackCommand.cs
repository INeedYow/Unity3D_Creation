using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackCommand : AttackCommand
{

    public ProjectileAttackCommand(Character owner) : base(owner){}
    public override void Attack()
    {
        // if (Time.time < lastAttackTime + owner.attackDelay) return;
        
        // lastAttackTime = Time.time;
        // owner.anim.SetTrigger("Attack"); 
    }
}