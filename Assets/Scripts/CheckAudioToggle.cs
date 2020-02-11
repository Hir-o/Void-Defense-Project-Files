using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAudioToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    private void Start()
    {
        if (AudioListener.volume <= 0f)
        {
            toggle.isOn = false;
            AudioListener.volume = 0f;
        }
        else
        {
            toggle.isOn = true;
            AudioListener.volume = 1f;
        }
    }
}
