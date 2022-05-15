using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_FallBack : ActionMacro
{
    public Action_FallBack(Hero hero) : base(hero) {}

    public override bool Execute(){
        //owner.Move(DungeonManager.instance.curDungeon.homeTransform);
        owner.transform.LookAt(Vector3.back);
        owner.transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime);
        return true;
    }
}
