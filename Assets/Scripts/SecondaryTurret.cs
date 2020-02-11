using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

public class SecondaryTurret : MonoBehaviour
{
    private EnemyMovementJobSystem.Enemy targetEnemy;

    [Header("Setup in Unity")]
    public Transform target, partToRotate, firePoint, firePointAlternate;

    public GameObject bulletPrefab;

    public float fireCountdown = 0f;

    private Vector3 dir, rotation;

    private Quaternion lookRotation;

    private Collider[] hitColliders;
    private Collider   nearestEnemy;

    private float distanceToEnemy, shortestDistance;

    private GameObject       bulletGameObject;
    private Bullet           bullet;
    private ParticleDisabler _particleDisabler;

    [SerializeField] private enum Turret
    {
        TurretLeft,
        TurretRight
    }

    [SerializeField] private Turret _turret = Turret.TurretLeft;

    [SerializeField] private enum FirePoint
    {
        Left,
        Right
    }

    [SerializeField] private FirePoint _firePoint = FirePoint.Left;

    private void Start()
    {
        _particleDisabler = GetComponentInChildren<ParticleDisabler>();
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    private void Update()
    {
        if (MainTurretController.Health <= 0f) return;

        if (target == null) return;

        dir          = target.position - transform.position;
        dir.y        = 0f;
        lookRotation = Quaternion.LookRotation(dir);
        rotation = Quaternion
                   .Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * SecondaryTurretController.TurnSpeed)
                   .eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / SecondaryTurretController.FireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void UpdateTarget() { NearestEnemy(transform.position, SecondaryTurretController.Range); }

    private void NearestEnemy(Vector3 center, float radius)
    {
        hitColliders = Physics.OverlapSphere(center, radius);

        shortestDistance = Mathf.Infinity;
        nearestEnemy     = null;

        foreach (Collider enemy in hitColliders)
        {
            if (enemy.gameObject.CompareTag(Tags.ENEMY_TAG))
            {
                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy     = enemy;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= SecondaryTurretController.Range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    private Vector3    bulletSpawnPosition;
    private Quaternion bulletSpawnRotation;

    private void Shoot()
    {
        switch (_firePoint)
        {
            case FirePoint.Left:
                bulletSpawnPosition = firePoint.position;
                bulletSpawnRotation = firePoint.rotation;
                _firePoint          = FirePoint.Right;
                break;
            case FirePoint.Right:
                bulletSpawnPosition = firePointAlternate.position;
                bulletSpawnRotation = firePointAlternate.rotation;
                _firePoint          = FirePoint.Left;
                break;
        }

        bulletGameObject = Instantiate(bulletPrefab, bulletSpawnPosition, bulletSpawnRotation);
        SoundManager.Instance.PlaySecondaryTurretProjectileSound();
    }

    void OnMouseOver()
    {
        if (enabled)
        {
            if (_turret == Turret.TurretLeft)
                ObjectReferenceHolder.Instance.lineRendererSecondaryTurret1.enabled = true;
            else if (_turret == Turret.TurretRight)
                ObjectReferenceHolder.Instance.lineRendererSecondaryTurret2.enabled = true;
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
        if (_turret == Turret.TurretLeft)
            ObjectReferenceHolder.Instance.lineRendererSecondaryTurret1.enabled = false;
        else if (_turret == Turret.TurretRight)
            ObjectReferenceHolder.Instance.lineRendererSecondaryTurret2.enabled = false;
    }
}