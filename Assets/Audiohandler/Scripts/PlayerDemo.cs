using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : MonoBehaviour
{
    public AudioPlayer SoundHolder;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SoundHolder.playClip(ClipName.Background);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SoundHolder.stopClip(ClipName.Background);
        }
    }
}
