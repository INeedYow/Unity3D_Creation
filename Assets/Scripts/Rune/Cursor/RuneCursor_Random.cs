using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_Random : RuneSkillCursor
{
    public Transform skillObjsParentTF;
    List<RuneSkillObject> skillObjs;

    private void Awake() {
        skillObjs = new List<RuneSkillObject>();
        skillObjsParentTF.gameObject.SetActive(true);

        for (int i = 0; i < skillObjsParentTF.childCount; i++)
        {
            skillObjs.Add(skillObjsParentTF.GetChild(i).GetComponent<RuneSkillObject>());
        }
    }
    protected override void CursorPosition() {}

    protected override void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            int random = Random.Range(-1, skillObjs.Count);

            while (random < 0)
            {   
                skillObjs[Random.Range(0, skillObjs.Count)].gameObject.SetActive(true);
                random = Random.Range(-1, skillObjs.Count);
            }
            skillObjs[random].gameObject.SetActive(true);

            onWorks?.Invoke();
            gameObject.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1))
        {   
            Cancel();
        }
    }
}
