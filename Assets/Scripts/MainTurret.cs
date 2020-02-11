using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Networking.PlayerConnection;

public class MainTurret : MonoBehaviour
{
    private EnemyMovementJobSystem.Enemy targetEnemy;

    [Header("Setup in Unity")] public Transform target, partToRotate, firePointHolder, firePoint;

    public GameObject bulletPrefab;

    public float fireCountdown, lifeRegenCountDown = 1f;

    [Tooltip(
        "1 - Means the turret is looking right toward the enemy, while -1 means the turret is looking in the opposite site of the enemy")]
    [Range(0, 1f)]
    public float minFireAngle = .95f;

    private Vector3 dir, rotation, flattenTransform, flattenTargetTransform, positionWithOffset;

    private Quaternion lookRotation;

    private Collider[] hitColliders;
    private Collider nearestEnemy;

    private float distanceToEnemy, shortestDistance, angleToEnemy, tempDamage, tempBulletCount, _lifeRegenCountDown;

    private GameObject bulletGameObject;
    private Transform bounceTarget;

    private Bullet bullet;
    private EnemyHealth targetHealth;

    private int bulletCount = 0;

    public static MainTurret instance;

    [SerializeField] private Vector3 offset;

    private WaveSpawner _waveSpawner;

    void Start()
    {
        target = null;
        instance = this;
        _lifeRegenCountDown = lifeRegenCountDown;
        _waveSpawner = ObjectReferenceHolder.Instance.waveSpawner;
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void Update()
    {
        if (MainTurretController.Health <= 0f) return;

        if (_waveSpawner.currentWaveCount > 0 && MainTurretController.Regen > 0f && MainTurretController.Health < 100f) RegenLife();

        if (target == null) return;

        dir = target.position - transform.position;
        dir.y = 0f;

        lookRotation = Quaternion.LookRotation(dir);
        rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * MainTurretController.TurnSpeed)
            .eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        firePointHolder.LookAt(target);

        angleToEnemy = Vector3.Dot((target.transform.position - partToRotate.position).normalized,
            partToRotate.forward);

        if (angleToEnemy > minFireAngle)
        {
            if (fireCountdown <= 0f && targetHealth.isTargetable)
            {
                if (bulletCount > 0 || targetHealth.GetHealth() > 0)
                {
                    bulletCount--;
                    Shoot();
                    fireCountdown = 1f / MainTurretController.FireRate;
                }
                else
                {
                    targetHealth.isTargetable = false;
                    target = null;
                    bulletCount = 0;
                }
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void RegenLife()
    {
        if (_lifeRegenCountDown <= 0f)
        {
            MainTurretController.Health += MainTurretController.Regen;
            _lifeRegenCountDown = lifeRegenCountDown;

            if (MainTurretController.Health > 100f) MainTurretController.Health = 100f;
            
            UIUpdater.Instance.UpdateHP();
        }

        _lifeRegenCountDown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        //sometimes doesn't work need to recheck
        //only search for a target if u have none or someone else killed it already
        //if(target == null || (targetHealth.GetHealth() <= 0))
        NearestEnemy(transform.position, MainTurretController.Range);
    }

    void NearestEnemy(Vector3 center, float radius)
    {
        hitColliders = Physics.OverlapSphere(center, radius);

        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (Collider enemy in hitColliders)
        {
            if (enemy.gameObject.CompareTag(Tags.ENEMY_TAG))
            {
                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }


        if (nearestEnemy != null && shortestDistance <= MainTurretController.Range)
        {
            target = nearestEnemy.transform;
            targetHealth = target.gameObject.GetComponentInParent<EnemyHealth>();

            tempDamage = MainTurretController.ProjectileDamage;

            tempBulletCount = targetHealth.GetHealth() / tempDamage;

            //Debug.Log("Health: "+targetHealth.GetHealth());
            //Debug.Log("Damage per bullet: "+tempDamage);
            //Debug.Log("Health/Damage per bullet: "+ tempBulletCount);
            //Debug.Log("Health/Damage per bullet ceilToInt: "+ Mathf.CeilToInt(tempBulletCount));


            //define the number of bullets needed to kill enemy
            if (tempBulletCount <= 0)
            {
                //Debug.Log("Bullet count was 0 or lower number of bullets is 1");
                bulletCount = 1;
            }
            else
            {
                //Debug.Log("Bullet count was greater than 0number of bullets is: "+Mathf.CeilToInt(tempBulletCount));
                bulletCount = Mathf.CeilToInt(tempBulletCount);
                
                //Debug.Log("Target health: "+targetHealth.GetHealth());
                //Debug.Log("Turret damage: "+tempDamage);
                //Debug.Log("Bullet count: "+tempBulletCount);
                //Debug.Log("Bullet count ceiltoInt: "+bulletCount);
            }
        }
        else
        {
            targetHealth = null;
            target = null;
        }
    }

    void Shoot()
    {
        bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SoundManager.Instance.PlayMainTurretProjectileSound();
    }

    public void BounceBullet(Transform bulletBouncedFrom)
    {
        //Find the closest target
        bounceTarget = NearestBounceEnemy(bulletBouncedFrom.position, MainTurretController.Range);

        if (bounceTarget == null) return;

        //Turn towards the target
        bulletBouncedFrom.LookAt(bounceTarget);

        //Rotate down a bit
        bulletBouncedFrom.Rotate(3, 0, 0, Space.Self);

        //Apply an offset to make it spawn over the target in order to not collide with it
        positionWithOffset = bulletBouncedFrom.position + offset;

        //spawn the bullet
        Instantiate(bulletPrefab, positionWithOffset, bulletBouncedFrom.rotation);
    }

    Transform NearestBounceEnemy(Vector3 center, float radius)
    {
        hitColliders = Physics.OverlapSphere(center, radius);

        shortestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (Collider enemy in hitColliders)
        {
            if (enemy.gameObject.CompareTag(Tags.ENEMY_TAG))
            {
                distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= MainTurretController.Range)
        {
            return nearestEnemy.transform;
        }

        return null;
    }

    void OnMouseOver()
    {
        ObjectReferenceHolder.Instance.lineRenderer.enabled = true;
    }

    void OnMouseExit()
    {
        ObjectReferenceHolder.Instance.lineRenderer.enabled = false;
    }
}