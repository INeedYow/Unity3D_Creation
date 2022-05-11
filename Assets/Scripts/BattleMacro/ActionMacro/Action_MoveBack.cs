using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_FallBack : ActionMacro
{
    public Action_FallBack(string desc, Hero hero) : base(desc, hero) {}

    public override void Execute(){
        owner.Move(DungeonManager.instance.curDungeon.homeTransform);
    }
}
