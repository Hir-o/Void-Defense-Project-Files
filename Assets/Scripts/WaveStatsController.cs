using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WaveStatsController : MonoBehaviour
{
    public static WaveStatsController Instance;

    [BoxGroup("Init Values")] [SerializeField]
    private float initWaveCount         = 5f,
                initSpawnRate         = 1f,
                initSpeedFactor       = 1f,
                initEnemyHealth       = 1f,
                initEnemyDamage       = 1f,
                initRewardPower       = 1f,
                initBossHealth        = 5f,
                initBossDamage        = 5f,
                initBossRewardPower   = 10f,
                initBossRewardScience = 15f;

    [BoxGroup("Multipliers")]
    public float waveCountMultiplier              = 1.07f,
                 waveSpawnRateMultiplier          = 1.01f,
                 waveEnemySpeedFactor             = 1.07f,
                 enemyHealthMultiplier            = 1.1f,
                 enemyDamageMultiplier            = 1.06f,
                 enemyRewardPowerMultiplier       = 1.09f,
                 enemyBossHealthMultiplier        = 1.1f,
                 enemyBossDamageMultiplier        = 1.08f,
                 enemyBossRewardPowerMultiplier   = 1.13f,
                 enemyBossRewardScienceMultiplier = 1.1f;

    [BoxGroup("Enemy Colors")]
    public Color[] bodyColor;

    [BoxGroup("Enemy Colors")]
    [ColorUsageAttribute(true, true)] public Color[] emissionColor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #region Calculate Methods

    public int CalculateWaveEnemyCount(int waveNumber)
    {
        return Mathf.CeilToInt(initWaveCount *
                               Mathf.Pow(waveCountMultiplier, waveNumber));
    }
    
    public float CalculateWaveEnemySpawnRate(int waveNumber)
    {
        return Mathf.Round(initSpawnRate                             *
                           Mathf.Pow(waveSpawnRateMultiplier, waveNumber) * 10) / 10;
    }

    public float CalculateWaveEnemySpeedFactor(int waveNumber)
    {
        return Mathf.Round(initSpeedFactor                             *
                           Mathf.Pow(waveEnemySpeedFactor, waveNumber) * 10) / 10;
    }

    public float CalculateWaveEnemyHealth(int waveNumber)
    {
        return Mathf.Round(initEnemyHealth                              *
                           Mathf.Pow(enemyHealthMultiplier, waveNumber) * 10) / 10;
    }

    public float CalculateWaveEnemyDamage(int waveNumber)
    {
        return Mathf.Round(initEnemyDamage                              *
                           Mathf.Pow(enemyDamageMultiplier, waveNumber) * 10) / 10;
    }

    public int CalculateWaveEnemyRewardPower(int waveNumber)
    {
        return Mathf.CeilToInt(initRewardPower *
                               Mathf.Pow(enemyRewardPowerMultiplier, waveNumber));
    }

    public float CalculateWaveBossEnemyHealth(int waveNumber)
    {
        return Mathf.Round(initBossHealth                                   *
                           Mathf.Pow(enemyBossHealthMultiplier, waveNumber) * 10) / 10;
    }

    public float CalculateWaveBossEnemyDamage(int waveNumber)
    {
        return Mathf.Round(initBossDamage                                   *
                           Mathf.Pow(enemyBossDamageMultiplier, waveNumber) * 10) / 10;
    }

    public int CalculateWaveBossEnemyRewardPower(int waveNumber)
    {
        return Mathf.CeilToInt(initBossRewardPower *
                               Mathf.Pow(enemyBossRewardPowerMultiplier, waveNumber));
    }

    public int CalculateWaveBossEnemyRewardScience(int waveNumber)
    {
        return Mathf.CeilToInt(initBossRewardScience *
                               Mathf.Pow(enemyBossRewardScienceMultiplier, waveNumber));
    }

    #endregion

    public void SetEnemyColor(WaveSpawner.Wave wave, int waveNumber)
    {
        if (waveNumber < 5)
        {
            wave.bodyColor     = bodyColor[0];
            wave.emissionColor = emissionColor[0];
        }
        else if (waveNumber >= 5 && waveNumber < 10)
        {
            wave.bodyColor     = bodyColor[1];
            wave.emissionColor = emissionColor[1];
        }
        else if (waveNumber >= 10 && waveNumber < 15)
        {
            wave.bodyColor     = bodyColor[2];
            wave.emissionColor = emissionColor[2];
        }
        else if (waveNumber >= 15 && waveNumber < 20)
        {
            wave.bodyColor     = bodyColor[3];
            wave.emissionColor = emissionColor[3];
        }
        else if (waveNumber >= 20 && waveNumber < 25)
        {
            wave.bodyColor     = bodyColor[4];
            wave.emissionColor = emissionColor[4];
        }
        else if (waveNumber >= 25 && waveNumber < 30)
        {
            wave.bodyColor     = bodyColor[5];
            wave.emissionColor = emissionColor[5];
        }
        else if (waveNumber >= 35 && waveNumber < 40)
        {
            wave.bodyColor     = bodyColor[7];
            wave.emissionColor = emissionColor[7];
        }
        else if (waveNumber >= 40 && waveNumber < 45)
        {
            wave.bodyColor     = bodyColor[8];
            wave.emissionColor = emissionColor[8];
        }
        else if (waveNumber >= 45)
        {
            wave.bodyColor     = bodyColor[9];
            wave.emissionColor = emissionColor[9];
        }
    }
}