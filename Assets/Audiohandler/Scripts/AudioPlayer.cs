using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioDataModel> sounds= new List<AudioDataModel>();
    public static AudioPlayer Instance;
    private void Awake() {
        if (Instance == null)
            Instance = this;
        filtrateDuplicate();
    }
    private void Start()
    {
        foreach (var sound in sounds)
        {
            sound.setAudioSource(AudioClipHolder.initAudioSource?.Invoke(sound.ClipName, sound.Parent));
        }
    }
    public void stopClip(ClipName clip)
    {
        getAudioSource(clip).Stop();
    }
    public void playClip(ClipName clip)
    {
        getAudioSource(clip).Play();
    }
    public void playClip(ClipName clip, bool isLoop)
    {
        getAudioSource(clip).Play();
        getAudioSource(clip).loop = isLoop;
    }
    public void generateSource(ClipName clipName, Transform parent)
    {
        AudioSource audioSource = AudioClipHolder.initAudioSource?.Invoke(clipName, parent);
        sounds.Add(new AudioDataModel(clipName, parent, audioSource));
    }
    public void changeSource(ClipName clipName, ClipName newClipName)
    {
        AudioDataModel dataModel = sounds.Find(x => x.ClipName == clipName);
        int index = sounds.IndexOf(dataModel);
        sounds[index].setAudioSource(AudioClipHolder.initAudioSource?.Invoke(newClipName, dataModel.Parent));
        sounds[index].ClipName= newClipName;
    }
    public void removeSource(ClipName clipName) => sounds.Remove(sounds.Find(x => x.ClipName == clipName));
    public AudioSource getAudioSource(ClipName clipName) => sounds.Find(x => x.ClipName == clipName).getAudioSource();
    public void filtrateDuplicate()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            for (int j = 0; j < sounds.Count; j++)
            {
                if (i == j) continue;

                if (sounds[i].ClipName == sounds[j].ClipName)
                {
                    sounds.Remove(sounds[j]);
                    j--;
                }
            }
        }
    }
}