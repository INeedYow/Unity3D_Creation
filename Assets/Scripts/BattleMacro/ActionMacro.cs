using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionMacro : BattleMacro
{
    // 마나로 하더라도 마나 없어서 스킬 못 쓴 경우 다음 조건 실행하게 bool 반환해야 할듯
        // 그럼 BattleMacro에서 한 번에 정의가능
    public abstract void Execute();

    public ActionMacro(string desc, Hero hero) : base(desc, hero) {}
}
