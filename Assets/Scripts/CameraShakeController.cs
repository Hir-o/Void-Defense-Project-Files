using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public static CameraShakeController Instance;

    [BoxGroup("EMP Shake Values")] [SerializeField]
    private float empShakeDuration   = .5f,
                  empShakeStrength   = 1f,
                  empShakeVibratio   = 1f,
                  empShakeRandomness = 45;

    [BoxGroup("Energy Blast Shake Values")] [SerializeField]
    private float energyBlastShakeDuration   = .5f,
                  energyBlastShakeStrength   = 1f,
                  energyBlastShakeVibratio   = 1f,
                  energyBlastShakeRandomness = 45;

    [BoxGroup("Enemy Kill Shake Values")] [SerializeField]
    private float enemyKillShakeDuration   = .05f,
                  enemyKillShakeStrength   = .1f,
                  enemyKillShakeVibratio   = 2f,
                  enemyKillShakeRandomness = 90;

    [BoxGroup("Mortar Projectile Impact Shake Values")] [SerializeField]
    private float mortarProjectileShakeDuration   = .1f,
                  mortarProjectileShakeStrength   = .4f,
                  mortarProjectileShakeVibratio   = 10f,
                  mortarProjectileShakeRandomness = 90;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EMPShake()
    {
        ObjectReferenceHolder.Instance.mainCamera.DOShakePosition(empShakeDuration, empShakeStrength,
                                                                  Mathf.RoundToInt(empShakeVibratio)
                                                                  , empShakeRandomness, true);
    }

    public void EnergyBlastShake()
    {
        ObjectReferenceHolder.Instance.mainCamera.DOShakePosition(energyBlastShakeDuration, energyBlastShakeStrength,
                                                                  Mathf.RoundToInt(energyBlastShakeVibratio)
                                                                  , energyBlastShakeRandomness, true);
    }

    public void EnemyKillShake()
    {
        ObjectReferenceHolder.Instance.mainCamera.DOShakePosition(enemyKillShakeDuration, enemyKillShakeStrength,
                                                                  Mathf.RoundToInt(enemyKillShakeVibratio)
                                                                  , enemyKillShakeRandomness, true);
    }

    public void MortarProjectileImpactShake()
    {
        ObjectReferenceHolder.Instance.mainCamera.DOShakePosition(mortarProjectileShakeDuration,
                                                                  mortarProjectileShakeStrength,
                                                                  Mathf.RoundToInt(mortarProjectileShakeVibratio)
                                                                  , mortarProjectileShakeRandomness, true);
    }
}