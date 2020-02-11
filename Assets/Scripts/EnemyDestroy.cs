using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimatorParams;
using Random = UnityEngine.Random;

public class EnemyDestroy : MonoBehaviour
{
    private Animator               _animator;
    private GameObject             _tempGameObject;
    private Material               _tempMaterial;
    private ObjectPools            _objectPool;
    private ParticleSystemRenderer _deathParticleRenderer;
    private ParticleSystem[]       _deathParticles;

    [SerializeField] private Vector3 enemyBodyDeathParticlesize, enemyStandDeathParticlesize;

    [SerializeField] private int enemyDeathParticlesBodyCount, enemyDeathParticlesStandCount;

    [SerializeField] private float bodyParticleStartSize,
                                   standParticleStartSize,
                                   bossBodyParticleStartSize,
                                   bossStandParticleStartSize;

    private Quaternion _tempRotation;

    private int bounceChance, randomNumber;

    private void Start()
    {
        _animator   = GetComponent<Animator>();
        _objectPool = ObjectPools.Instance;
    }

    public void DestroyEnemy(Material newMaterial, bool isBoss)
    {
        InitiateDeactivation();
                
        if (HasBounced())
        {
            MainTurret.instance.BounceBullet(transform);
        }

        _tempGameObject = _objectPool.SpawnFromDeathParticlePool(Tags.POOL_ENEMY_DEATH_PARTICLE, transform.position,
                                                                 transform.rotation.eulerAngles);
        
        _tempGameObject.GetComponentInChildren<EnemyResourceDrop>().UpdateText(isBoss);

        UpdateParticleSizeAndColor(newMaterial, isBoss);
    }

    private void UpdateParticleSizeAndColor(Material newMaterial, bool isBoss)
    {
        // assign particle material same as the enemy material
        _deathParticleRenderer          = _tempGameObject.GetComponentInChildren<ParticleSystemRenderer>();
        _deathParticleRenderer.material = newMaterial;

        _deathParticles = _tempGameObject.GetComponentsInChildren<ParticleSystem>();
        ParticleSystem.ShapeModule deathParticleBodyShape  = _deathParticles[0].shape;
        ParticleSystem.ShapeModule deathParticleStandShape = _deathParticles[1].shape;

        ParticleSystem.MainModule bodyParticle  = _deathParticles[0].main;
        ParticleSystem.MainModule standParticle = _deathParticles[1].main;

        if (isBoss)
        {
            bodyParticle.startSize  = bossBodyParticleStartSize;
            standParticle.startSize = bossStandParticleStartSize;

            deathParticleBodyShape.scale = new Vector3(enemyBodyDeathParticlesize.x * 2,
                                                       enemyBodyDeathParticlesize.y * 2,
                                                       enemyBodyDeathParticlesize.z * 2);
            deathParticleStandShape.scale = new Vector3(enemyStandDeathParticlesize.x * 2,
                                                        enemyStandDeathParticlesize.y * 2,
                                                        enemyStandDeathParticlesize.z * 2);
        }
        else
        {
            bodyParticle.startSize  = bodyParticleStartSize;
            standParticle.startSize = standParticleStartSize;

            deathParticleBodyShape.scale  = enemyBodyDeathParticlesize;
            deathParticleStandShape.scale = enemyStandDeathParticlesize;
        }
    }

    public void DeactivateEnemyEvent() // called from enemy deactivate and deactivate_alternate events
    {
        InitiateDeactivation();
    }
    
    public bool HasBounced()
    {
        bounceChance = Mathf.RoundToInt(MainTurretController.BounceChance);
        randomNumber = Mathf.RoundToInt(Random.value * 100);

        if (randomNumber < bounceChance)
        {
            return true;
        }

        return false;
    }

    private void InitiateDeactivation()
    {
        gameObject.SetActive(false);

        AnimatorParam.SetAnimatorParameter(_animator, AnimatorParameters.ENEMY_DEACTIVATE, false,
                                           AnimatorParameters.ParameterTypeBool);

        AnimatorParam.SetAnimatorParameter(_animator, AnimatorParameters.ENEMY_DEACTIVATE_ALTERNATE, false,
                                           AnimatorParameters.ParameterTypeBool);
    }
}