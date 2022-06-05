using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillObj_BuffRestoration : SkillObject
{
    public UnityAction<SkillObj_BuffRestoration> onRestoration;

    public override void Works()
    {
        if (skill.target == null)
        {
            FinishWorks();
        }
        

        // for문 중에 바로 Remove하면 for문 도는 데 오류가 생기기 때문에
        // 표식?찍는 느낌으로 이벤트에 등록만 해놓고 for문 끝나고 한 번에 발송하는 느낌으로 했음
        else
        {   //Debug.Log("count : " + skill.target.buffs.Count);
            foreach (Buff buff in skill.target.buffs)
            {   
                if (buff.IsDebuff())
                {   
                    onRestoration += buff.Restoration;
                }
               
            }
        }

        onRestoration?.Invoke(this);
        FinishWorks();
    }
}
