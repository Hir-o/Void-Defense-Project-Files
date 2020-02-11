using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class EnemyStatsPanel : MonoBehaviour
{
    public static EnemyStatsPanel Instance;

    [BoxGroup("Enemy Stats Panel")] [SerializeField]
    private GameObject enemyStatPanel;

    [BoxGroup("Enemy Stats Text")] [SerializeField]
    private TextMeshProUGUI enemyStatsText;

    private EnemyHealth _tempEnemy;
    private WaveSpawner _waveSpawner;

    private float health;

    [SerializeField] private float fPanelUpdateValue = .2f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() { _waveSpawner = ObjectReferenceHolder.Instance.waveSpawner; }

    public void ShowEnemyPanel(EnemyHealth enemy)
    {
        _tempEnemy = enemy;

        CancelInvoke(nameof(UpdateEnemyPanel));

        if (enemyStatPanel.activeSelf) enemyStatPanel.SetActive(false);

        if (enemyStatPanel.activeSelf == false) enemyStatPanel.SetActive(true);

        health = enemy.GetHealth();

        if (health < 0f) health = 0f;

        health = Mathf.Round(health * 10f) / 10f;

        if (enemy.isBoss)
        {
            enemyStatsText.text = "HEALTH: " + health + " / " + _waveSpawner.waves[_waveSpawner.nextWave].bossHealth +
                                  "\n"
                                  + "DAMAGE: "  + EnemyController.BossDamage      + "\n"
                                  + "SPEED: "   + EnemyController.BossSpeedFactor + "\n"
                                  + "Ep DROP: " + (EnemyController.BossRewardPower +
                                                   ((EnemyController.BossRewardPower / 100) *
                                                    ResourcesController.EnergyDropRate)) + "\n"
                                  + "Sp DROP: "                                          +
                                  (EnemyController.BossRewardScience +
                                   ((EnemyController.BossRewardScience / 100) *
                                    ResourcesController.ScienceDropRate));
        }
        else
        {
            enemyStatsText.text = "HEALTH: "                                       + health + " / " +
                                  _waveSpawner.waves[_waveSpawner.nextWave].health + "\n"
                                  + "DAMAGE: "                                     + EnemyController.Damage      + "\n"
                                  + "SPEED: "                                      + EnemyController.SpeedFactor + "\n"
                                  + "Ep DROP: "                                    + (EnemyController.RewardPower +
                                                   ((EnemyController.RewardPower / 100) *
                                                    ResourcesController.EnergyDropRate));
        }

        InvokeRepeating(nameof(UpdateEnemyPanel), fPanelUpdateValue, fPanelUpdateValue);
    }

    private void UpdateEnemyPanel()
    {
        health = _tempEnemy.GetHealth();

        if (health < 0f) health = 0f;
        
        health = Mathf.Round(health * 10f) / 10f;
        
        if (_tempEnemy.isBoss)
        {
            enemyStatsText.text = "HEALTH: " + health + " / " + _waveSpawner.waves[_waveSpawner.nextWave].bossHealth +
                                  "\n"
                                  + "DAMAGE: "  + EnemyController.BossDamage      + "\n"
                                  + "SPEED: "   + EnemyController.BossSpeedFactor + "\n"
                                  + "Ep DROP: " + (EnemyController.BossRewardPower +
                                                   ((EnemyController.BossRewardPower / 100) *
                                                    ResourcesController.EnergyDropRate)) + "\n"
                                  + "Sp DROP: "                                          +
                                  (EnemyController.BossRewardScience +
                                   ((EnemyController.BossRewardScience / 100) *
                                    ResourcesController.ScienceDropRate));
        }
        else
        {
            enemyStatsText.text = "HEALTH: "                                       + health + " / " +
                                  _waveSpawner.waves[_waveSpawner.nextWave].health + "\n"
                                  + "DAMAGE: "                                     + EnemyController.Damage      + "\n"
                                  + "SPEED: "                                      + EnemyController.SpeedFactor + "\n"
                                  + "Ep DROP: "                                    + (EnemyController.RewardPower +
                                                   ((EnemyController.RewardPower / 100) *
                                                    ResourcesController.EnergyDropRate));
        }
        
        if (_tempEnemy == null || _tempEnemy.gameObject.activeSelf == false)
            CancelInvoke();
    }

    public void HideEnemyPanel()
    {
        CancelInvoke();

        if (enemyStatPanel.activeSelf) enemyStatPanel.SetActive(false);
    }
}