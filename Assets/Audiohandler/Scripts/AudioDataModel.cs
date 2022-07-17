using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioDataModel
{
    public ClipName ClipName;
    public Transform Parent;
    private AudioSource AudioSource;

    public AudioDataModel(ClipName clipName, Transform parent, AudioSource audioSource)
    {
        ClipName = clipName;
        Parent = parent;
        AudioSource = audioSource;
    }

    public void setAudioSource(AudioSource audioSource)
    {
        AudioSource = audioSource;
    }
    public AudioSource getAudioSource() => AudioSource;
}
