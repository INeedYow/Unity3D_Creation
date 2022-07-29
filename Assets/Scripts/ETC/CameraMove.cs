using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform defaultTf;
    public Transform dungeonTf;
    bool isDefault = true;

    public void ToggleView()
    {
        if (isDefault)
        {
            transform.position = dungeonTf.position;
            transform.rotation = dungeonTf.rotation;
            isDefault = !isDefault;
        }
        else{
            transform.position = defaultTf.position;
            transform.rotation = defaultTf.rotation;
            isDefault = !isDefault;
        }
    }
}
