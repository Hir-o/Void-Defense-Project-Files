using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnergyWave : MonoBehaviour
{
    public static AutoEnergyWave Instance;

    [HideInInspector] public bool isAutoEnergyWave;
    
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

    public void ToggleAutoEnergyWave()
    {
        isAutoEnergyWave = !isAutoEnergyWave;
        
        if (isAutoEnergyWave)
        {
            _objectReferenceHolder.tmpEnergyBlastAuto.font = _objectReferenceHolder.fontAssetBlue;
            _objectReferenceHolder.tmpEnergyBlastAuto.material = _objectReferenceHolder.fontMaterialBlue;
        }
        else
        {
            _objectReferenceHolder.tmpEnergyBlastAuto.font = _objectReferenceHolder.fontAssetRed;
            _objectReferenceHolder.tmpEnergyBlastAuto.material = _objectReferenceHolder.fontMaterialRed;
        }
    }
}
