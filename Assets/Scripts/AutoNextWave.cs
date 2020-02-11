using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoNextWave : MonoBehaviour
{
    public static AutoNextWave Instance;

    [HideInInspector] public bool isAutoNextWaveEnabled;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        isAutoNextWaveEnabled = false;
    }

    public void ToggleAutoNextWave() { isAutoNextWaveEnabled = !isAutoNextWaveEnabled; }
}