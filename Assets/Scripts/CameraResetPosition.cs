using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraResetPosition : MonoBehaviour
{
    public static CameraResetPosition Instance;

    private static Vector3 cameraStartPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        cameraStartPosition = transform.position;
    }

    public void ResetCameraPosition() { transform.DOLocalMove(cameraStartPosition, .1f); }
}