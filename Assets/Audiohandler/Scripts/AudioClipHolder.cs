using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipHolder : MonoBehaviour
{
    public string Path;
    public AudioSource Prefab;
    public delegate AudioSource InitAudioSource(ClipName clipName, Transform parent);
    public static InitAudioSource initAudioSource;

    private Dictionary<ClipName, AudioClip> ClipMap = new Dictionary<ClipName, AudioClip>();

    private void Awake()
    {
        initAudioSource = initSource;

        var audioClips=ResourcesUtil.getResources<AudioClip>(Path);
        for(int i = 0; i < audioClips.Length; i++)
        {
            ClipMap.Add((ClipName)i, audioClips[i]);
        }
    }

    private AudioSource initSource(ClipName clipName, Transform parent=null)
    {
        AudioClip audioClip = ClipMap[clipName];

        AudioSource audioSource= Instantiate(Prefab, parent);
        audioSource.clip = audioClip;
        return audioSource;
    }
}

public enum ClipName
{
    Background,
    GoodSound,
    BadSound,
    BoxExplosion,
    BoxRotate,
    Enemy,
    Player,
    PlayerDeath
}
