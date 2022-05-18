using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // public Transform defaultTf;
    // public Transform dungeonTf;
    bool isDefault = true;

    public void ToggleView()
    {
        if (isDefault)
        {
            transform.Translate(40f, 0f, 0f);
        }
        else{
            transform.Translate(-40f, 0f, 0f);
        }
    }
}
