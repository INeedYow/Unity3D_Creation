using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    public UnityAction onMoveFinish;
    public static CameraManager instance { get; private set; }

    public Camera mainCam;
    public Transform planetTF;
    public Transform dungeonTF;
    public Transform runeTreeTF;

    private void Awake() {
        instance = this;
        if (mainCam == null) mainCam = Camera.main;
    }

    public void SetPlanetView()
    {   //Debug.Log("SetPlanet");
        StartCoroutine("EnterPlanet");
    }

    public void SetDungeonView()
    {
        mainCam.transform.position = dungeonTF.position;
        mainCam.transform.rotation = dungeonTF.rotation;
    }

    public void SetRuneTreeView()
    {   //Debug.Log("SetTree");
        StartCoroutine("EnterRuneTree");
    }

    IEnumerator EnterRuneTree()
    { 
        float speed = 0f;

        while (speed < 1f)
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, runeTreeTF.position, speed);
            mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, runeTreeTF.rotation, speed);
            speed += Time.deltaTime * 0.2f;
            yield return null;
        }
        onMoveFinish?.Invoke();
        yield return null;
    }

    IEnumerator EnterPlanet()
    { 
        float speed = 0f;

        while (speed < 1f)
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, planetTF.position, speed);
            mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, planetTF.rotation, speed);
            speed += Time.deltaTime;
            yield return null;
        }
        onMoveFinish?.Invoke();
        yield return null;
    }
}
