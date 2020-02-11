using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOvercharge : MonoBehaviour
{
    public static AutoOvercharge Instance;

    [HideInInspector] public bool isAutoOvercharge;
    
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

    public void ToggleAutoOvercharge()
    {
        isAutoOvercharge = !isAutoOvercharge;
        
        if (isAutoOvercharge)
        {
            _objectReferenceHolder.tmpOverchargeAuto.font = _objectReferenceHolder.fontAssetBlue;
            _objectReferenceHolder.tmpOverchargeAuto.material = _objectReferenceHolder.fontMaterialBlue;
        }
        else
        {
            _objectReferenceHolder.tmpOverchargeAuto.font = _objectReferenceHolder.fontAssetRed;
            _objectReferenceHolder.tmpOverchargeAuto.material = _objectReferenceHolder.fontMaterialRed;
        }
    }
}

