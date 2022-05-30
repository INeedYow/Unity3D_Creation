using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_FallBack : ActionMacro
{

    public override bool IsReady()
    {
        return true;
    }
    public override void Execute(Character target){
        // owner.transform.LookAt(Vector3.back);
        // owner.transform.Translate(Vector3.forward * owner.moveSpeed * Time.deltaTime);
        return;
    }
}
