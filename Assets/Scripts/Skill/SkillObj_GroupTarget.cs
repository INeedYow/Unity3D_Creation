using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_GroupTarget : SkillObject
{
    public EGroup eTargetGroup;

    public override void Works()
    {
        if (eTargetGroup == EGroup.Monster)
        {
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (ch.isDead || ch.isStop) continue;

                ch.Damaged(data.);
                AddBuff(ch);
            }
        }
    }
}
