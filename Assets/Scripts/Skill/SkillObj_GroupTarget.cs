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

                //ch.Damaged(10, 1f, null, true);
                //AddBuff(ch);

                // if (data.eUserEffect != EEffect.None)
                // {
                //     eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                //     eff.transform.position = skill.owner.transform.position;
                // }

                // if (data.eTargetEffect != EEffect.None)
                // {
                //     eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                //     m_target.Effected(eff);
                // }
            }
        }

        else
        {
            foreach (Character ch in PartyManager.instance.heroParty)
            {
                if (ch.isDead || ch.isStop) continue;

                //ch.Damaged(10, 1f, null, true);
                //AddBuff(ch);

                // if (data.eUserEffect != EEffect.None)
                // {
                //     eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                //     eff.transform.position = skill.owner.transform.position;
                // }

                // if (data.eTargetEffect != EEffect.None)
                // {
                //     eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                //     m_target.Effected(eff);
                // }
            }
        }
    }
}
