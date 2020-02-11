using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpBlast : MonoBehaviour
{
    private void OnEnable()
    {
        SkillsController.IsEMPEnabled = true;
        
        ObjectReferenceHolder.Instance.enemyMovementJobSystem.DisableEnemyMovement();

        foreach (GameObject enemy in ObjectReferenceHolder.Instance.allEnemies)
            enemy.GetComponent<EnemyHealth>().isTargetable = true;
    }
}
