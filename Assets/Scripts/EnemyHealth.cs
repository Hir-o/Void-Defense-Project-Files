using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimatorParams;
using DG.Tweening;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private float _health;

    private EnemyDestroy _enemyDestroy;
    private float        _randomValue, _enemyHealthToSteal, _totalHealth, _damageToIgnore, _blockChance, _randomNumber;

    public bool isEnemyDead, isBoss, isTargetable = true;

    private Material thisObjectMaterial;

    private ObjectPools _objectPool;
    private GameObject  _tempGameObject, _critTMPObject;

    private Vector3 critParticleOffset = new Vector3(0f, 1f, 0f);

    private EnemyTagChanger _enemyTagChanger;

    private WaveSpawner _waveSpawner;

    private void OnEnable() { isTargetable = true; }

    private void Awake()
    {
        _animator        = GetComponent<Animator>();
        _enemyDestroy    = GetComponent<EnemyDestroy>();
        _enemyTagChanger = GetComponent<EnemyTagChanger>();

        thisObjectMaterial = GetComponentInChildren<MeshRenderer>().material;

        _objectPool = ObjectPools.Instance;

        _waveSpawner = ObjectReferenceHolder.Instance.waveSpawner;
    }

    public void ReduceHealth(float damage, bool checkForLifeSteal) // called when hit
    {
        if (isEnemyDead) return;

        _health -= damage;

        if (_health <= 0f)
        {
            _enemyDestroy.DestroyEnemy(thisObjectMaterial, isBoss);
            ObjectReferenceHolder.Instance.waveSpawner.currentWaveCount--;
            ObjectReferenceHolder.Instance.tmpEnemyCounter.text = "ENEMIES LEFT: " + _waveSpawner.currentWaveCount;

            isEnemyDead = true;

            if (isBoss)
            {
                ResourcesController.SciencePoints += (EnemyController.BossRewardScience +
                                                      ((EnemyController.BossRewardScience / 100) *
                                                       ResourcesController.ScienceDropRate));

                ResourcesController.EnergyPoints += (EnemyController.BossRewardPower +
                                                     ((EnemyController.BossRewardPower / 100) *
                                                      ResourcesController.EnergyDropRate));
            }
            else
                ResourcesController.EnergyPoints += (EnemyController.RewardPower +
                                                     ((EnemyController.RewardPower / 100) *
                                                      ResourcesController.EnergyDropRate));

            UIUpdater.Instance.UpdateResourceTexts();

            if (MainTurretController.Lifesteal > 0f && MainTurretController.Health < 100f && checkForLifeSteal)
            {
                _enemyHealthToSteal = (MainTurretController.ProjectileDamage * MainTurretController.Lifesteal) / 100f;

                MainTurretController.Health += _enemyHealthToSteal;

                if (MainTurretController.Health > 100f) MainTurretController.Health = 100f;

                UIUpdater.Instance.UpdateHP();
            }

            CameraShakeController.Instance.EnemyKillShake();
        }
    }

    public void Kill()
    {
        if (isEnemyDead) return;

        _enemyDestroy.DestroyEnemy(thisObjectMaterial, isBoss);
        ObjectReferenceHolder.Instance.waveSpawner.currentWaveCount--;
        ObjectReferenceHolder.Instance.tmpEnemyCounter.text = "ENEMIES LEFT: " + _waveSpawner.currentWaveCount;

        isEnemyDead = true;

        UIUpdater.Instance.UpdateResourceTexts();
    }

    public void HitMainTurret() // called when this enemy hits the main turret
    {
        if (isEnemyDead) return;

        isEnemyDead = true;

        _randomValue = Random.Range(0, 10f);

        if (_randomValue > 5)
            AnimatorParam.SetAnimatorParameter(_animator, AnimatorParameters.ENEMY_DEACTIVATE, true,
                                               AnimatorParameters.ParameterTypeBool);
        else
            AnimatorParam.SetAnimatorParameter(_animator, AnimatorParameters.ENEMY_DEACTIVATE_ALTERNATE, true,
                                               AnimatorParameters.ParameterTypeBool);

        _enemyTagChanger.ChangeEnemyTag();

        ObjectReferenceHolder.Instance.waveSpawner.currentWaveCount--;
        ObjectReferenceHolder.Instance.tmpEnemyCounter.text = "ENEMIES LEFT: " + _waveSpawner.currentWaveCount;

        if (CanBlock())
        {
            // ignore damage
            if (ObjectReferenceHolder.Instance.TMPBlocked.activeSelf == false)
                ObjectReferenceHolder.Instance.TMPBlocked.SetActive(true);

            return;
        }

        if (isBoss)
        {
            if (MainTurretController.Defense > 0f)
            {
                _damageToIgnore             =  (EnemyController.BossDamage * MainTurretController.Defense) / 100f;
                MainTurretController.Health -= EnemyController.BossDamage - _damageToIgnore;
            }
            else
                MainTurretController.Health -= EnemyController.BossDamage;
        }
        else
        {
            if (MainTurretController.Defense > 0f)
            {
                _damageToIgnore             =  (EnemyController.Damage * MainTurretController.Defense) / 100f;
                MainTurretController.Health -= EnemyController.Damage - _damageToIgnore;
            }
            else
                MainTurretController.Health -= EnemyController.Damage;
        }

        if (MainTurretController.Health < 0f)
        {
            MainTurretController.Health = 0f;
            GameManager.Instance.EndGame();
        }

        UIUpdater.Instance.UpdateHP();
    }

    public void PlayCritParticle()
    {
        _tempGameObject =
            _objectPool.SpawnFromCritParticlePool(Tags.POOL_CRIT_PARTICLE, transform.position + critParticleOffset);

        _critTMPObject = _objectPool.SpawnFromCritTMPPool(Tags.POOL_CRIT_TMP, transform.position + critParticleOffset);
    }

    private bool CanBlock()
    {
        _blockChance  = MainTurretController.BlockChance;
        _randomNumber = Mathf.RoundToInt(Random.value * 100);

        if (_randomNumber < _blockChance)
        {
            return true;
        }

        return false;
    }

    public float GetHealth() { return _health; }

    public void SetHealth(float value) { _health = _totalHealth = value; }

    public void SetIsBoss(bool value) { isBoss = value; }
}