using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    [SerializeField] private float timer = 10f;
    
    //todo replace with ObjectDisabler
    private void Start() { Destroy(gameObject, timer); }
}
