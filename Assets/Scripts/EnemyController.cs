using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 3f,
        maxSpeed = 6f,
        speedFactor,
        bossSpeedFactor,
        stoppingDistance,
        damage,
        bossDamage,
        rewardPower,
        bossRewardPower,
        rewardScience,
        disabledTimer,
        bossRewardScience;

    public static float MinSpeed,
        MaxSpeed,
        SpeedFactor,
        BossSpeedFactor,
        StoppingDistance,
        Damage,
        BossDamage,
        RewardPower,
        BossRewardPower,
        DisabledTimer,
        RewardScience,
        BossRewardScience;

    private void Awake()
    {
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
        SpeedFactor = speedFactor;
        StoppingDistance = stoppingDistance;
        DisabledTimer = disabledTimer;
    }
}