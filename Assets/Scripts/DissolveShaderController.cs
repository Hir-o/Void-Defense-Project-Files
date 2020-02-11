using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DissolveShaderController : MonoBehaviour
{
    public static DissolveShaderController Instance;
    
    [BoxGroup("Dissolve Shader Materials")]
    [SerializeField] private MeshRenderer groundDissolveRenderer;

    [SerializeField] private float radius = 16f, radiusIncreaseAmount = .2f, step = .2f;

    private static readonly string DissolveRadius = "_Radius";

    private Material _dissolveMaterial;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
            Destroy(gameObject);    
    }

    private void Start()
    {
        _dissolveMaterial = groundDissolveRenderer.material;

        _dissolveMaterial.SetFloat(DissolveRadius, radius);

        MakeWorldVisible();
    }

    public void MakeWorldVisible()
    {
        if (_dissolveMaterial.GetFloat(DissolveRadius) < 150f) StartCoroutine(IncreaseDissolveRadius());
    }
    
    public void MakeWorldInvisible() { _dissolveMaterial.SetFloat(DissolveRadius, radius); }

    private IEnumerator IncreaseDissolveRadius()
    {
        while (_dissolveMaterial.GetFloat(DissolveRadius) < 150f)
        {
            _dissolveMaterial.SetFloat(DissolveRadius,
                                       _dissolveMaterial.GetFloat(DissolveRadius) +
                                       radiusIncreaseAmount);

            yield return new WaitForSeconds(step);
        }
    }
}