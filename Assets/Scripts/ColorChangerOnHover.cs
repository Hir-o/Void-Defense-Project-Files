using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerOnHover : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Color        _startColor;

    [ColorUsageAttribute(true, true)] [SerializeField]
    private Color currentEmissionColor, newEmissionColor;

    private void Start() { _renderer = GetComponent<MeshRenderer>(); }

    void OnMouseEnter()
    {
        currentEmissionColor = _renderer.material.GetColor("_EmissionColor");
        _renderer.material.SetColor("_EmissionColor", newEmissionColor);
        _startColor              = _renderer.material.color;
        _renderer.material.color = Color.white;
    }

    void OnMouseExit()
    {
        _renderer.material.color = _startColor;
        _renderer.material.SetColor("_EmissionColor", currentEmissionColor);
    }
}