using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj_GroupHeal : SkillObject
{
    public EGroup eTargetGroup;


    public override void Works()
    {
        StartCoroutine("OnWorks");
        
    }

    IEnumerator OnWorks()
    {
        for (int i = 0; i < data.repeat; i++)
        {
            if (eTargetGroup == EGroup.Monster)
            {
                foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
                {
                    if (ch.isDead || ch.isStop) continue;

                    ch.Healed(data.powerRatio * skill.owner.magicDamage);

                    if (data.eTargetEffect != EEffect.None)
                    {
                        eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                        eff.SetPosition(ch);
                    }
                }
            }   

            else
            {
                foreach (Character ch in PartyManager.instance.heroParty)
                {
                    if (ch.isDead || ch.isStop) continue;

                    ch.Healed(data.powerRatio * skill.owner.magicDamage);

                    if (data.eTargetEffect != EEffect.None)
                    {
                        eff = ObjectPool.instance.GetEffect((int)data.eTargetEffect);
                        eff.SetPosition(ch);
                    }
                }
            }
        
            if (data.eUserEffect != EEffect.None)
            {   
                eff = ObjectPool.instance.GetEffect((int)data.eUserEffect);
                eff.SetPosition(skill.owner);
            }

            yield return new WaitForSeconds(data.interval);
        }
        
        FinishWorks();
    }
}
