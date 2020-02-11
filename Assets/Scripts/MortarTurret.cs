using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTurret : MonoBehaviour
{
    private EnemyMovementJobSystem.Enemy targetEnemy;

    [Header("Setup in Unity")]
    public Transform target, firePoint;

    public GameObject bulletPrefab;

    public float fireCountdown;

    private Vector3 dir, rotation;

    private Quaternion lookRotation;

    private Collider[] hitColliders;
    private Collider   randomEnemy;

    private float distanceToEnemy, shortestDistance;

    private GameObject       bulletGameObject;
    private Bullet           bullet;
    private ParticleDisabler _particleDisabler;

    private void Start()
    {
        _particleDisabler = GetComponentInChildren<ParticleDisabler>();
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    private void Update()
    {
//        if (fireCountdown > 0f)
//            fireCountdown -= Time.deltaTime;
//        else
//            fireCountdown = 0f;

        if (MainTurretController.Health <= 0f) return;

        if (target == null) return;

        dir   = target.position - transform.position;
        dir.y = 0f;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / MortarTurretController.FireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void UpdateTarget() { RandomEnemy(transform.position, MortarTurretController.Range); }

    private float randomEnemyChance;

    private void RandomEnemy(Vector3 center, float radius)
    {
        hitColliders = Physics.OverlapSphere(center, radius);

        //shortestDistance = Mathf.Infinity;
        randomEnemy = null;

        foreach (Collider enemy in hitColliders)
        {
            if (enemy.gameObject.CompareTag(Tags.ENEMY_TAG))
            {
                randomEnemyChance = Random.Range(0f, 10f);

                if (randomEnemyChance > 5f) continue;

                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy >= MortarTurretController.MinRange)
                {
                    shortestDistance = distanceToEnemy;
                    randomEnemy      = enemy;
                }
            }
        }

        if (randomEnemy != null && shortestDistance >= MortarTurretController.MinRange)
            target = randomEnemy.transform;
        else
            target = null;
    }

    private void Shoot()
    {
        bulletGameObject =
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGameObject.GetComponent<MortarProjectile>().TargetObjectTF = target;

        RandomEnemy(transform.position, MortarTurretController.Range);
        
        SoundManager.Instance.PlayMortarTurretProjectileSound();
    }

    void OnMouseOver()
    {
        if (enabled)
        {
            ObjectReferenceHolder.Instance.lineRendererMortarTurret.enabled      = true;
            ObjectReferenceHolder.Instance.lineRendererMortarTurretChild.enabled = true;
        }
    }

    void OnMouseExit() { DisableLineRenderer(); }

    // called from despawn event
    public void DisableOverchargeParticle()
    {
        if (_particleDisabler.isActiveAndEnabled)
            _particleDisabler.StopParticle();

        DisableLineRenderer();
    }

    private void DisableLineRenderer()
    {
        ObjectReferenceHolder.Instance.lineRendererMortarTurret.enabled      = false;
        ObjectReferenceHolder.Instance.lineRendererMortarTurretChild.enabled = false;
    }
}