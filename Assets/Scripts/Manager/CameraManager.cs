using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance { get; private set; }

    public Camera mainCam;

    public Transform[] camTransforms;

    private void Awake() {
        instance = this;
    }

    public void SetCam(int index){
        mainCam.transform.position = camTransforms[index].position;
        mainCam.transform.rotation = camTransforms[index].rotation;
    }
}
