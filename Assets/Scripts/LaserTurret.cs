using System.Collections;
using System.Collections.Generic;
using System.Timers;
using NaughtyAttributes;
using Unity.Collections;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    private EnemyMovementJobSystem.Enemy targetEnemy;

    [Header("Setup in Unity")]
    public Transform target, partToRotate, firePoint;

    public GameObject bulletPrefab;

    public float turnSpeed = 10f;

    private Vector3 dir, rotation, end;

    private Quaternion lookRotation;

    private Collider[] hitColliders;
    private Collider   nearestEnemy;

    private float distanceToEnemy, shortestDistance, distance, damageCoolDownTimer;

    private GameObject bulletGameObject;

    private Bullet bullet;

    private GameObject       beamStart, beamEnd, beam;
    private LineRenderer     line;
    private RaycastHit       _hit;
    private EnemyHealth      _enemyHealth;
    private ParticleDisabler _particleDisabler;
    private AudioSource      _audioSource;

    [Header("Prefabs")]
    public GameObject beamLineRendererPrefab, beamStartPrefab, beamEndPrefab;

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned

    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 3;  //Length of the beam texture

    [BoxGroup("Enemy Layer")] [SerializeField]
    private LayerMask enemyMask;

    [SerializeField] private float laserVolume = .5f;

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.1f);

        beamStart = Instantiate(beamStartPrefab,        new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        beamEnd   = Instantiate(beamEndPrefab,          new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        beam      = Instantiate(beamLineRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        _particleDisabler = GetComponentInChildren<ParticleDisabler>();
        _audioSource      = GetComponent<AudioSource>();

        _audioSource.volume = 0f;

        DeactivateLaser();
    }

    void Update()
    {
        if (MainTurretController.Health <= 0f)
        {
            DeactivateLaser();
            return;
        }

        if (target == null)
        {
            DeactivateLaser();
            return;
        }
        else if (_enemyHealth != null && _enemyHealth.isEnemyDead)
        {
            DeactivateLaser();
            return;
        }

        dir          = target.position - transform.position;
        dir.y        = 0f;
        lookRotation = Quaternion.LookRotation(dir);
        rotation = Quaternion
                   .Slerp(partToRotate.rotation, lookRotation, Time.deltaTime * LaserTurretController.TurnSpeed)
                   .eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Shoot();
    }

    private void UpdateTarget() { NearestEnemy(transform.position, LaserTurretController.Range); }

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

        if (nearestEnemy != null && shortestDistance <= LaserTurretController.Range)
        {
            target       = nearestEnemy.transform;
            _enemyHealth = target.GetComponentInParent<EnemyHealth>();
        }
        else
            target = null;
    }

    private void Shoot()
    {
        beamStart.gameObject.SetActive(true);
        beamEnd.gameObject.SetActive(true);
        beam.gameObject.SetActive(true);

        if (Time.timeScale > 1)
            _audioSource.volume = 0f;
        else
            _audioSource.volume = laserVolume;

        line = beam.GetComponent<LineRenderer>();

        ShootBeamInDir(firePoint.position, partToRotate.transform.forward);
    }

    private void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        line.SetVertexCount(2);
        line.SetPosition(0, start);
        beamStart.transform.position = firePoint.position;

        end = Vector3.zero;

        if (Physics.Raycast(start, dir, out _hit, 10000000f, enemyMask))
        {
            end = _hit.point - (dir.normalized * beamEndOffset);

            if (_hit.transform.gameObject.CompareTag(Tags.ENEMY_TAG))
                _hit.transform.gameObject.GetComponentInParent<EnemyHealth>()
                    .ReduceHealth(LaserTurretController.DamagePerSecond, false);
        }
        else
            end = transform.position + (dir * 100);

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        distance                              =  Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale  =  new Vector2(distance       / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }

    public void DeactivateLaser()
    {
        if (beamStart != null) beamStart.gameObject.SetActive(false);
        if (beamEnd   != null) beamEnd.gameObject.SetActive(false);
        if (beam      != null) beam.gameObject.SetActive(false);

        if (_audioSource != null) _audioSource.volume = 0f;
    }

    void OnMouseOver()
    {
        if (enabled) ObjectReferenceHolder.Instance.lineRendererLaserTurret.enabled = true;
    }

    void OnMouseExit() { DisableLineRenderer(); }

    // called from despawn event
    public void DisableOverchargeParticle()
    {
        if (_particleDisabler.isActiveAndEnabled) _particleDisabler.StopParticle();

        DisableLineRenderer();
    }

    private void DisableLineRenderer() { ObjectReferenceHolder.Instance.lineRendererLaserTurret.enabled = false; }
}