using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MortarProjectile : MonoBehaviour
{
    // launch variables
    public Transform TargetObjectTF;
    [Range(1.0f, 15.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;
    [Range(0.0f, 10.0f)] public float TargetHeightOffsetFromGround;
    public bool RandomizeHeightOffset;

    // cache
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 initialPosition, projectileXZPos, targetXZPos, localVelocity, globalVelocity;
    private Quaternion initialRotation;
    private float R, G, tanAlpha, H, Vz, Vy;

    // Projectile
    [Header("Particle Code")]
    public GameObject impactParticle;

    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public GameObject[] trailParticles;

    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

    private bool hasCollided = false;

    private GameObject curTrail;
    private ParticleSystem[] trails;
    private ParticleSystem trail;

    [Header("Bomb Code")]
    public float explosionRadius = 2f;

    private EnemyHealth _enemyHealth;

    private Transform target;

    [SerializeField] private Collider[] _colliders;

    //-----------------------------------------------------------------------------------------------

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
        if (muzzleParticle)
        {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
        }

        Launch();
    }

    void OnCollisionEnter(Collision hit)
    {
        HitTargets();

        if (!hasCollided)
        {
            hasCollided = true;
            impactParticle =
                Instantiate(impactParticle, transform.position,
                    Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

            foreach (GameObject trail in trailParticles)
            {
                curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                curTrail.transform.parent = null;
                Destroy(curTrail, 3f);
            }

            Destroy(projectileParticle, 3f);
            Destroy(impactParticle, 5f);
            Destroy(gameObject, 1f);

            trails = GetComponentsInChildren<ParticleSystem>();
            //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++)
            {
                trail = trails[i];
                if (!trail.gameObject.name.Contains("Trail"))
                    continue;

                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2);
            }
            
            CameraShakeController.Instance.MortarProjectileImpactShake();
            
            SoundManager.Instance.PlayMortarTurretProjectileImpactSound();
        }
    }

    void HitTargets()
    {
        _colliders =  Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitCollider in _colliders)
        {
            if (hitCollider.gameObject.CompareTag(Tags.ENEMY_TAG))
            {
                _enemyHealth = hitCollider.GetComponentInParent<EnemyHealth>();
                _enemyHealth.ReduceHealth(MortarTurretController.ProjectileDamage, false);
            }
        }

        Destroy(gameObject);
    }

    // returns the distance between the red dot and the TargetObject's y-position
    // this is a very little offset considered the ranges in this demo so it shouldn't make a big difference.
    // however, if this code is tested on smaller values, the lack of this offset might introduce errors.
    // to be technically accurate, consider using this offset together with the target platform's y-position.
    float GetPlatformOffset()
    {
        float platformOffset = 0.0f;
        // 
        //          (SIDE VIEW OF THE PLATFORM)
        //
        //                   +------------------------- Mark (Sprite)
        //                   v
        //                  ___                                          -+-
        //    +-------------   ------------+         <- Platform (Cube)   |  platformOffset
        // ---|--------------X-------------|-----    <- TargetObject     -+-
        //    +----------------------------+
        //

        // we're iterating through Mark (Sprite) and Platform (Cube) Transforms. 
        foreach (Transform childTransform in TargetObjectTF.GetComponentsInChildren<Transform>())
        {
            // take into account the y-offset of the Mark gameobject, which essentially
            // is (y-offset + y-scale/2) of the Platform as we've set earlier through the editor.
            if (childTransform.name == "Mark")
            {
                platformOffset = childTransform.localPosition.y;
                break;
            }
        }

        return platformOffset;
    }

    private float distanceToEnemy;

    // launches the object towards the TargetObject with a given LaunchAngle
    void Launch()
    {
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        targetXZPos = new Vector3(TargetObjectTF.position.x, 0.0f, TargetObjectTF.position.z);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        R = Vector3.Distance(projectileXZPos, targetXZPos);
        G = Physics.gravity.y;
        tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        H = (TargetObjectTF.position.y + GetPlatformOffset()) - transform.position.y;

        distanceToEnemy = Vector3.Distance(transform.position, TargetObjectTF.position);

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        if (SkillsController.IsEMPEnabled)
            Vz = Mathf.Sqrt(G * R * R / ((distanceToEnemy / (distanceToEnemy / 3f) - .4f) * (H - R * tanAlpha)));
        else
            Vz = Mathf.Sqrt(G * R * R / ((distanceToEnemy / (distanceToEnemy / 3f) + (EnemyController.SpeedFactor / 5f)) * (H - R * tanAlpha)));
        
        Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        localVelocity = new Vector3(0f, Vy, Vz);
        globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        _rigidbody.velocity = globalVelocity;
    }
}