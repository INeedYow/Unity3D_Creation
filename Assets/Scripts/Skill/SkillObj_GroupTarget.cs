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

                ch.Damaged(10, 1f, null, true);
                AddBuff(ch);
            }
        }

        else
        {
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;

                ch.Damaged(10, 1f, null, true);
                AddBuff(ch);
            }
        }
    }
}
