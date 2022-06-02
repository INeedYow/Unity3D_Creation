using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillObj_GroupTarget : RuneSkillObject
{
    public EGroup eTargetGroup;

    public override void Works()
    {
        if (eTargetGroup == EGroup.Monster)
        {
            foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
            {
                if (ch.isDead || ch.isStop) continue;

                ch.Damaged(data.power);
                AddBuff(ch);

                if (data.eOnWorksEffect != EEffect.None)
                {
                    eff = ObjectPool.instance.GetEffect((int)data.eOnWorksEffect);
                    eff.transform.position = ch.targetTF.position;
                }

            }
        }

        else
        {
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;

                ch.Damaged(data.power);
                AddBuff(ch);

                if (data.eOnWorksEffect != EEffect.None)
                {
                    eff = ObjectPool.instance.GetEffect((int)data.eOnWorksEffect);
                    eff.transform.position = ch.targetTF.position;
                }
            }
        }
    }
}