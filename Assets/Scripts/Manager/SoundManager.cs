using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set;}

    [SerializeField] AudioSource audioSource;
    public AudioClip clip_bgm;
    public float curBGMVolume;




    private void Awake() {
        instance = this;
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        Init();
    }

    void Init()
    {
        audioSource.clip = clip_bgm;
        curBGMVolume = 0.01f;
        audioSource.volume = curBGMVolume;
        audioSource.Play();
    }

}
