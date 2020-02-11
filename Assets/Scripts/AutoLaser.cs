using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLaser : MonoBehaviour
{
    public static AutoLaser Instance;

    [HideInInspector] public bool isAutoLaser;
    
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

    public void ToggleAutoLaser()
    {
        isAutoLaser = !isAutoLaser;
        
        if (isAutoLaser)
        {
            _objectReferenceHolder.tmpLaserAuto.font = _objectReferenceHolder.fontAssetBlue;
            _objectReferenceHolder.tmpLaserAuto.material = _objectReferenceHolder.fontMaterialBlue;
        }
        else
        {
            _objectReferenceHolder.tmpLaserAuto.font = _objectReferenceHolder.fontAssetRed;
            _objectReferenceHolder.tmpLaserAuto.material = _objectReferenceHolder.fontMaterialRed;
        }
    }
}
