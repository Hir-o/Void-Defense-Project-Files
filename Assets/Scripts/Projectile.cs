using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public GameObject   impactParticle;
    public GameObject   projectileParticle;
    public GameObject   muzzleParticle;
    public GameObject[] trailParticles;

    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

    private bool hasCollided = false;

    private GameObject       curTrail;
    private ParticleSystem[] trails;
    private ParticleSystem   trail;

    [Header("Bullet Script")]
    public float explosionRadius = 0f;

    private EnemyHealth _enemyHealth;

    private Transform target;

    [SerializeField] private Rigidbody _rigidbody;

    public enum TurretTypeProjectile
    {
        MainTurret,
        SecondaryTurret,
        LaserTurret
    }

    public TurretTypeProjectile turretTypeProjectile = TurretTypeProjectile.MainTurret;

    private int   critChance, randomNumber;
    private float critDamagePercent;

    void Start()
    {
        switch (turretTypeProjectile)
        {
            case TurretTypeProjectile.MainTurret:
                _rigidbody.AddForce(transform.forward * MainTurretController.ProjectileThrust);
                break;
            case TurretTypeProjectile.SecondaryTurret:
                _rigidbody.AddForce(transform.forward * SecondaryTurretController.ProjectileThrust);
                break;
        }

        projectileParticle =
            Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag(Tags.ENEMY_TAG)) HitTarget(hit);

        //if (!hit.gameObject.CompareTag(Tags.ENEMY_TAG)) return;

        if (!hasCollided)
        {
            hasCollided = true;
            impactParticle =
                Instantiate(impactParticle, transform.position,
                            Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

            foreach (GameObject trail in trailParticles)
            {
                curTrail                  = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                curTrail.transform.parent = null;
                Destroy(curTrail, 3f);
            }

            Destroy(projectileParticle, 3f);
            Destroy(impactParticle,     5f);
            Destroy(gameObject);

            trails = GetComponentsInChildren<ParticleSystem>();
            //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++)
            {
                trail = trails[i];
                if (!trail.gameObject.name.Contains("Trail")) continue;

                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2);
            }

            switch (turretTypeProjectile)
            {
                case TurretTypeProjectile.MainTurret:
                    SoundManager.Instance.PlayMainTurretProjectileImpactSound();
                    break;
                case TurretTypeProjectile.SecondaryTurret:
                    SoundManager.Instance.PlaySecondaryTurretProjectileImpactSound();
                    break;
            }
        }
    }

    void HitTarget(Collision hit)
    {
        _enemyHealth      = hit.gameObject.GetComponentInParent<EnemyHealth>();
        critDamagePercent = MainTurretController.CritDamage;

        if (_enemyHealth != null)
        {
            switch (turretTypeProjectile)
            {
                case TurretTypeProjectile.MainTurret:
                    if (HasCritcalHit())
                    {
                        _enemyHealth
                            .ReduceHealth(MainTurretController.ProjectileDamage + (MainTurretController.ProjectileDamage * critDamagePercent),
                                          true);
                        _enemyHealth.PlayCritParticle();
                    }
                    else
                    {
                        _enemyHealth.ReduceHealth(MainTurretController.ProjectileDamage, true);
                    }

                    break;
                case TurretTypeProjectile.SecondaryTurret:
                    _enemyHealth.ReduceHealth(SecondaryTurretController.ProjectileDamage, false);
                    break;
            }
        }

        Destroy(gameObject);
    }

    private bool HasCritcalHit()
    {
        critChance   = Mathf.RoundToInt(MainTurretController.CritChance);
        randomNumber = Mathf.RoundToInt(Random.value * 100);

        if (randomNumber < critChance)
        {
            return true;
        }

        return false;
    }
}