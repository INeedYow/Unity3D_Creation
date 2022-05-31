using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCursor_SingleTarget : MonoBehaviour
{
    public EGroup eTargetGroup;
    public float value;

    public void SetCursor(EGroup eTargetGroup, float value)
    {
        this.eTargetGroup = eTargetGroup;
        this.value = value;
    }

    private void Update() 
    {
        if (eTargetGroup == EGroup.Monster)
        {

        }
    }
}
