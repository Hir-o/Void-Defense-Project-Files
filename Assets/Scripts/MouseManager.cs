using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    
    [SerializeField] private LayerMask clickableLayer;

    private RaycastHit _rayHit;

    // Update is called once per frame
    void Update() { CheckForSelected(); }
    
    // Check if player is clicking on the enemy
    private void CheckForSelected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ObjectReferenceHolder.Instance.mainCamera.ScreenPointToRay(Input.mousePosition),
                                out _rayHit, Mathf.Infinity, clickableLayer))
            {
                EnemyStatsPanel.Instance.ShowEnemyPanel(_rayHit.collider.GetComponentInParent<EnemyHealth>());
            }
        }
    }
}
