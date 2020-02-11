using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;

    private void Update() { transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); }
}