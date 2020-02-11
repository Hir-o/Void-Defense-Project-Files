using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPCrit : MonoBehaviour
{
    private void OnEnable() { transform.LookAt(ObjectReferenceHolder.Instance.mainCamera.transform); }
}