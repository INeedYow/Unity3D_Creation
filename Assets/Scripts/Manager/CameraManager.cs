using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public static CameraManager instance { get; private set; }

    public Camera mainCam;

    private void Awake() {
        if (mainCam == null) mainCam = Camera.main;
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)){
            SetCamSKillView();
        }
    }

    public void SetCamSKillView(){
        StartCoroutine("MoveFar");
    }

    IEnumerator MoveFar()
    {   
        float moveDist = 0f;
        float moveSpeed = 800f;

        while (moveDist < 200f)
        {   
            mainCam.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
            moveDist += moveSpeed * Time.deltaTime;

            if (moveDist > 160f)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, 40f, Time.deltaTime * 20f);
            }

            yield return null;
        }
    }
}
