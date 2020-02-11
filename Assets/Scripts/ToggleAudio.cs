using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    public void ToggleAudioListener()
    {
        if (AudioListener.volume <= 0f)
            AudioListener.volume = 1f;
        else
            AudioListener.volume = 0f;
    }
}