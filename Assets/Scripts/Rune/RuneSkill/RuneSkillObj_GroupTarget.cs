using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkillObj_GroupTarget : RuneSkillObject
{
    public EGroup eTargetGroup;
    [Range(0f, 5f)] public float targetInterval;

    public override void Works()
    {   
        StartCoroutine("IntervalWorks");
    }

    IEnumerator IntervalWorks()
    {   

        if (eTargetGroup == EGroup.Monster)
        {
            for (int i = 0; i < data.repeat; i++)
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
                    yield return new WaitForSeconds(targetInterval);
                }
                yield return new WaitForSeconds(data.repeatInterval);
            }
            

        }

        else
        {
            for (int i = 0; i < data.repeat; i++)
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
                    yield return new WaitForSeconds(targetInterval);
                }
                yield return new WaitForSeconds(data.repeatInterval);
            }
        }
        
        FinishWorks();
    }
}

