using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMortar : MonoBehaviour
{
    public static AutoMortar Instance;

    [HideInInspector] public bool isAutoMortar;

    private ObjectReferenceHolder _objectReferenceHolder;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _objectReferenceHolder = ObjectReferenceHolder.Instance;
    }

    public void ToggleAutoMortar()
    {
        isAutoMortar = !isAutoMortar;

        if (isAutoMortar)
        {
            _objectReferenceHolder.tmpMortarAuto.font = _objectReferenceHolder.fontAssetBlue;
            _objectReferenceHolder.tmpMortarAuto.material = _objectReferenceHolder.fontMaterialBlue;
        }
        else
        {
            _objectReferenceHolder.tmpMortarAuto.font = _objectReferenceHolder.fontAssetRed;
            _objectReferenceHolder.tmpMortarAuto.material = _objectReferenceHolder.fontMaterialRed;
        }
    }
}
