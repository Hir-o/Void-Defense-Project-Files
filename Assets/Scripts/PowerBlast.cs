using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PowerBlast : MonoBehaviour
{
    private EnemyHealth[] _enemyHealths;

    [SerializeField] private float blastRadius = 30f;

    private Collider[] hitColliders;

    private bool canDestroyEnemies = false;

    private void OnEnable() { canDestroyEnemies = true; }

    private void Update()
    {
        if (canDestroyEnemies == false) return;
        
        hitColliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider coll in hitColliders)
        {
            if (coll.gameObject.CompareTag(Tags.ENEMY_TAG))
                coll.GetComponentInParent<EnemyHealth>().ReduceHealth(SkillsController.EnergyBlastDamage, false);
        }

        canDestroyEnemies = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}