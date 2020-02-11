using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    
    private void Update()
    {
        if (Time.timeScale > 0f)
            transform.Rotate(rotationVector, Space.Self);
    }
}
