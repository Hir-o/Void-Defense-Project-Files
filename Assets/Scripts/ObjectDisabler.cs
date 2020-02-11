using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    [SerializeField] private float disableTimer = 5f;

    private void OnEnable() { Invoke(nameof(DisableObject), disableTimer); }

    public void DisableObject() { gameObject.SetActive(false); }
}