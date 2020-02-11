using System;
using UnityEngine;
using System.Collections;
using AnimatorParams;
using NaughtyAttributes;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING
    };

    [System.Serializable]
    public class Wave
    {
        public string    name;
        public Transform enemy;
        public int       count;
        public float     rate;
        public Color     bodyColor;

        [ColorUsageAttribute(true, true)] public Color emissionColor;

        public float   speedFactor;
        public Vector3 scale;
        public float   health;
        public float   rewardPower;
        public float   rewardScience;
        public float   damage;
        public Vector3 bossScale;
        public float   bossHealth;
        public float   bossRewardPower;
        public float   bossRewardScience;
        public float   bossDamage;

        public bool isLastWave;
    }

    public Wave[] waves = new Wave[50];

    [BoxGroup("Next Wave")] public int nextWave = -1;

    [BoxGroup("Spawn Points")] public Transform[] spawnPoints; //todo delete

    private float searchCountdown = 1f;

    [BoxGroup("Spawn State")] public SpawnState state = SpawnState.WAITING;

    private ObjectPools _objectPool;
    private Transform   _spawnPoint;
    private Material    _tempMaterial;
    private GameObject  _tempGameObject;
    private EnemyHealth _tempGameObjectHealth;
    private SpawnPoints _spawnPoints;
    private Vector3     _newSpawnPos;

    [BoxGroup("Next Wave")] [Header("Next Wave Button")] [SerializeField]
    private Button _btnNextWave;

    [BoxGroup("Current Wave Count")] public int currentWaveCount;

    private bool autoNextWave;

    private int initWaveCount = 5;

    private bool _isLastWave, _isTransitioningToGameOver;

    void Start()
    {
        if (spawnPoints.Length == 0) Debug.LogError("No spawn points referenced.");

        _objectPool = ObjectPools.Instance;

        ObjectReferenceHolder.Instance.tmpWaveNumber.text = "WAVE " + (nextWave + 1);

        _spawnPoints = GetComponent<SpawnPoints>();

        CreateWaves();
    }

    private void Update()
    {
        if (_isLastWave)
        {
            if (currentWaveCount                   <= 0
                && _isTransitioningToGameOver      == false
                && GameManager.Instance.isGameOver == false)
            {
                _isTransitioningToGameOver = true;

                LevelManager.Instance.LoadGameOver();
                
                //Application.ExternalCall("kongregate.stats.submit", "Waves Completed", 50);

                _btnNextWave.interactable = false;
                SkillButtons.Instance.SetNewNextWavePanelPosition();
            }
        }

        if (_isTransitioningToGameOver) return;

        if (state == SpawnState.WAITING && currentWaveCount <= 0)
        {
            if (_btnNextWave.interactable == false)
            {
                _btnNextWave.interactable = true;
                SkillButtons.Instance.DisableSkillButtons();
                SkillButtons.Instance.ResetNextWavePanelPosition();
                SkillsController.Instance.ResetAllSkillTimers();
                SkillsController.Instance.DisableOverchargeParticles();
                ObjectReferenceHolder.Instance.tmpEnemyCounter.gameObject.SetActive(false);
                CameraResetPosition.Instance.ResetCameraPosition();
                
                //Application.ExternalCall("kongregate.stats.submit", "Waves Completed", nextWave + 1);
            }

            //if the auto next wave checkbox is ticked starts new wave immediately
            if (AutoNextWave.Instance.isAutoNextWaveEnabled && GameManager.Instance.isGameOver == false)
                ObjectReferenceHolder.Instance.waveSpawner.InitiateNextWave();
        }
        else
        {
            _btnNextWave.interactable = false;
            SkillButtons.Instance.EnableSkillbuttons();
            SkillButtons.Instance.SetNewNextWavePanelPosition();
        }
    }

    private void CreateWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].name              = "Wave " + (i + 1);
            waves[i].count             = WaveStatsController.Instance.CalculateWaveEnemyCount(i);
            waves[i].rate              = WaveStatsController.Instance.CalculateWaveEnemySpawnRate(i);
            waves[i].speedFactor       = WaveStatsController.Instance.CalculateWaveEnemySpeedFactor(i);
            waves[i].health            = WaveStatsController.Instance.CalculateWaveEnemyHealth(i);
            waves[i].damage            = WaveStatsController.Instance.CalculateWaveEnemyDamage(i);
            waves[i].rewardPower       = WaveStatsController.Instance.CalculateWaveEnemyRewardPower(i);
            waves[i].rewardScience     = 0f;
            waves[i].bossHealth        = WaveStatsController.Instance.CalculateWaveBossEnemyHealth(i);
            waves[i].bossDamage        = WaveStatsController.Instance.CalculateWaveBossEnemyDamage(i);
            waves[i].bossRewardPower   = WaveStatsController.Instance.CalculateWaveBossEnemyRewardPower(i);
            waves[i].bossRewardScience = WaveStatsController.Instance.CalculateWaveBossEnemyRewardScience(i);
            waves[i].isLastWave        = false;
            WaveStatsController.Instance.SetEnemyColor(waves[i], i);
        }

        waves[waves.Length - 1].isLastWave = true;
    }

    private IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        currentWaveCount = _wave.count;
        ObjectReferenceHolder.Instance.tmpEnemyCounter.gameObject.SetActive(true);
        ObjectReferenceHolder.Instance.tmpEnemyCounter.text = "ENEMIES LEFT: " + currentWaveCount;

        for (int i = 0; i < _wave.count; i++)
        {
            if (i == _wave.count - 1)
                SpawnBossEnemy(_wave.enemy);
            else
                SpawnEnemy(_wave.enemy);

            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    public void StopSpawningWave()
    {
        StopAllCoroutines();

        currentWaveCount = 0;

        state = SpawnState.WAITING;
    }

    #region SpawnEnemy

    private void SpawnEnemy(Transform _enemy) { InitializeSpawn(false); }

    private void SpawnBossEnemy(Transform _enemy) { InitializeSpawn(true); }

    private void InitializeSpawn(bool isBoss)
    {
        _newSpawnPos = _spawnPoints.GetRandomSpawnPosition();

        _tempGameObject       = _objectPool.SpawnFromPool(Tags.POOL_ENEMY, _newSpawnPos);
        _tempGameObjectHealth = _tempGameObject.GetComponent<EnemyHealth>();

        if (_tempGameObject == null) return;

        _tempGameObjectHealth.isEnemyDead = false;

        if (isBoss)
        {
            _tempGameObject.transform.localScale = waves[nextWave].bossScale;
            _tempGameObjectHealth.SetHealth(waves[nextWave].bossHealth);
            _tempGameObjectHealth.SetIsBoss(true);

            EnemyController.BossSpeedFactor   = waves[nextWave].speedFactor;
            EnemyController.BossDamage        = waves[nextWave].bossDamage;
            EnemyController.BossRewardPower   = waves[nextWave].bossRewardPower;
            EnemyController.BossRewardScience = waves[nextWave].bossRewardScience;
        }
        else
        {
            _tempGameObjectHealth.SetHealth(waves[nextWave].health);
            _tempGameObject.transform.localScale = waves[nextWave].scale;
            _tempGameObjectHealth.SetIsBoss(false);

            EnemyController.SpeedFactor   = waves[nextWave].speedFactor;
            EnemyController.Damage        = waves[nextWave].damage;
            EnemyController.RewardPower   = waves[nextWave].rewardPower;
            EnemyController.RewardScience = waves[nextWave].rewardScience;
        }

        SetEnemyColor(); // selects enemy color from the color array and the emission color from the HSV color array
    }

    private void SetEnemyColor()
    {
        _tempMaterial = _tempGameObject.GetComponentInChildren<MeshRenderer>().material;

        _tempMaterial.color = waves[nextWave].bodyColor;                         // sets the material color
        _tempMaterial.SetColor("_EmissionColor", waves[nextWave].emissionColor); // sets the emission material color
    }

    #endregion

    public void InitiateNextWave()
    {
        SaveGameController.SaveGame();
        
        if (state != SpawnState.SPAWNING)
        {
            nextWave++;
            _isLastWave                                       = waves[nextWave].isLastWave;
            ObjectReferenceHolder.Instance.tmpWaveNumber.text = "WAVE " + (nextWave + 1);
            StartCoroutine(SpawnWave(waves[nextWave]));

            WaveText.Instance.DisplayWaveText();
        }
    }

    public void ResetLastWave()
    {
        _isLastWave                = false;
        _isTransitioningToGameOver = false;
    }

    public void ResetCurrentWaveText()
    {
        ObjectReferenceHolder.Instance.tmpWaveNumber.text = "WAVE " + (nextWave + 1);
    }
}