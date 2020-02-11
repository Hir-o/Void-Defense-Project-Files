using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEMP : MonoBehaviour
{
    public static AutoEMP Instance;

    [HideInInspector] public bool isAutoEMP;
    
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

    public void ToggleAutoEMP()
    {
        isAutoEMP = !isAutoEMP;
        
        if (isAutoEMP)
        {
            _objectReferenceHolder.tmpEMPAuto.font = _objectReferenceHolder.fontAssetBlue;
            _objectReferenceHolder.tmpEMPAuto.material = _objectReferenceHolder.fontMaterialBlue;
        }
        else
        {
            _objectReferenceHolder.tmpEMPAuto.font = _objectReferenceHolder.fontAssetRed;
            _objectReferenceHolder.tmpEMPAuto.material = _objectReferenceHolder.fontMaterialRed;
        }
    }
}
