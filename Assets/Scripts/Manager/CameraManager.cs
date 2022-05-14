using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public static CameraManager instance { get; private set; }

    public Camera mainCam;
    public Transform defaultTf;     
    public Transform setTf;         
    public Transform DungeonTf;

    private void Awake() { 
        instance = this; 
        SetCam(defaultTf);
    }

    public void SetCam(Transform tf){
        mainCam.transform.position = tf.position;
        mainCam.transform.rotation = tf.rotation;
    }
}
