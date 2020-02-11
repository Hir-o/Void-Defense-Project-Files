using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSecondaryTurrets : MonoBehaviour
{
    public static AutoSecondaryTurrets Instance;

    [HideInInspector] public bool isAutoSecondaryTurrets;
    
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

    public void ToggleAutoSecondaryTurrets()
    {
        isAutoSecondaryTurrets = !isAutoSecondaryTurrets;
        
        if (isAutoSecondaryTurrets)
        {
            _objectReferenceHolder.tmpSecondaryTurretsAuto.font = _objectReferenceHolder.fontAssetBlue;
            _objectReferenceHolder.tmpSecondaryTurretsAuto.material = _objectReferenceHolder.fontMaterialBlue;
        }
        else
        {
            _objectReferenceHolder.tmpSecondaryTurretsAuto.font = _objectReferenceHolder.fontAssetRed;
            _objectReferenceHolder.tmpSecondaryTurretsAuto.material = _objectReferenceHolder.fontMaterialRed;
        }
    }
}
