using System;
using System.Collections;
using System.Collections.Generic;
using AnimatorParams;
using NaughtyAttributes;
using UnityEngine;

public class SkillsController : MonoBehaviour
{
    public static SkillsController Instance;

    private ObjectPools _objectPool;
    private WaveSpawner _waveSpawner;

    [BoxGroup("Initial Prices - Overcharge")] [SerializeField]
    private float overchargeCoolDownPrice,
                  overchargeActiveDurationPrice,
                  overchargePowerupPrice;

    [BoxGroup("Initial Prices - EMP")] [SerializeField]
    private float empCoolDownPrice,
                  empStunDurationPrice;

    [BoxGroup("Initial Prices - Energy Blast")] [SerializeField]
    private float energyBlastCoolDownPrice,
                  energyBlastDamagePrice;

    [ShowNonSerializedField] public static float OverchargeCoolDownPrice,
                                                 OverchargeActiveDurationPrice,
                                                 OverchargePowerupPrice,
                                                 EmpCoolDownPrice,
                                                 EmpStunDurationPrice,
                                                 EnergyBlastCoolDownPrice,
                                                 EnergyBlastDamagePrice;

    [ShowNonSerializedField] public static int OverchargeCoolDownUpgradeCount,
                                               OverchargeActiveDurationUpgradeCount,
                                               OverchargePowerupUpgradeCount,
                                               EmpCoolDownUpgradeCount,
                                               EmpStunDurationUpgradeCount,
                                               EnergyBlastCoolDownUpgradeCount,
                                               EnergyBlastDamageUpgradeCount;

    [ShowNonSerializedField]
    private static float InitOverchargeCoolDown,
                         InitOverchargeActiveTime,
                         InitOverchargePowerup,
                         InitEMPCoolDown,
                         InitEMPStunDuration,
                         InitEnergyCoolDown,
                         InitEnergyBlastDamage;

    // skill static variables
    public static float OverchargeCoolDown,
                        EmpCoolDown,
                        EnergyCoolDown;

    public static float OverchargeActiveTime;

    public static float OverchargePowerup, EmpStunDuration, EnergyBlastDamage;

    //skill cooldowns
    [BoxGroup("Cooldown Timers")] [SerializeField]
    private float overChargeCoolDown = 60f,
                  empCoolDown        = 30f,
                  energyCoolDown     = 60f;

    //skill active timers
    [BoxGroup("Active & Delay Timers")] [SerializeField]
    private float overChargeActiveTime = 5f,
    overchargeInitializeDelay = .4f;

    //skill values
    [BoxGroup("Skill Values")] [SerializeField]
    private float overchargePowerup = 1.2f,
                  energyBlastDamage = 5f;

    private float overchargeCooldownTimer,
                  empCooldownTimer,
                  secondaryTurretCooldDownTimer,
                  laserCoolDownTimer,
                  mortarCoolDownTimer,
                  energyCoolDownTimer;

    [BoxGroup("Skill Activation Bools")] [SerializeField]
    private bool canActivateOvercharge      = true,
                 canActivateEmp             = true,
                 canActivateSecondaryTurret = true,
                 canActivateLaser           = true,
                 canActivateMortar          = true,
                 canActivateEnergy          = true;

    [BoxGroup("Overcharge Particles")] [SerializeField]
    private ParticleSystem[] overchargeParticles;

    // skill prefabs
    [SerializeField] private Transform empBlastPosition;

    public static bool IsEMPEnabled = false;

    // Unmodified Active & Cooldown timers
    private float currentOverchargeCooldown,
                  currentOverchargeActiveTime,
                  currentEMPCooldown,
                  currentSecondaryTurretCooldown,
                  currentSecondaryTurretActiveTime,
                  currentLaserCooldown,
                  currentLaserActiveTime,
                  currentMortarCooldown,
                  currentMortarActiveTime,
                  currentEnergyBlastCoolDown;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InitOverchargeCoolDown   = overChargeCoolDown;
        InitOverchargeActiveTime = overChargeActiveTime;
        InitOverchargePowerup    = overchargePowerup;
        InitEMPCoolDown          = empCoolDown;
        InitEnergyCoolDown       = energyCoolDown;
        InitEnergyBlastDamage    = energyBlastDamage;

        EmpCoolDown        = empCoolDown;
        EnergyCoolDown     = energyCoolDown;
        OverchargeCoolDown = overChargeCoolDown;

        OverchargeActiveTime = overChargeActiveTime;

        OverchargePowerup = overchargePowerup;
        EnergyBlastDamage = energyBlastDamage;

        OverchargeCoolDownPrice       = overchargeCoolDownPrice;
        OverchargeActiveDurationPrice = overchargeActiveDurationPrice;
        OverchargePowerupPrice        = overchargePowerupPrice;

        EmpCoolDownPrice     = empCoolDownPrice;
        EmpStunDurationPrice = empStunDurationPrice;

        EnergyBlastCoolDownPrice = energyBlastCoolDownPrice;
        EnergyBlastDamagePrice   = energyBlastDamagePrice;

        _objectPool  = ObjectPools.Instance;
        _waveSpawner = ObjectReferenceHolder.Instance.waveSpawner;
    }

    private void Update()
    {
        if (_waveSpawner.state == WaveSpawner.SpawnState.WAITING && _waveSpawner.currentWaveCount == 0) return;

        CalculateOverchargeCoolDown();
        CalculateEMPCoolDown();
        CalculateSecondaryTurretCoolDown();
        CalculateLaserCoolDown();
        CalculateMortarCoolDown();
        CalculateEnergyCoolDown();
    }

    # region Create Skills

    public void TriggerOvercharge()
    {
        if (canActivateOvercharge         == false) return;
        if (_waveSpawner.currentWaveCount <= 0) return;

        SkillButtons.Instance.overchargeOverlay.color = SkillButtons.Instance.activeColor;
        SkillButtons.Instance.overchargeOverlayMaterial.SetFloat("_Hologram_Value_1", 1f);

        if (AutoOvercharge.Instance.isAutoOvercharge)
            Invoke(nameof(InitializeOvercharge), overchargeInitializeDelay);
        else
            Invoke(nameof(InitializeOvercharge), 0f);

        canActivateOvercharge = false;

        SkillButtons.Instance.overchargeActiveImg.SetActive(true);

        currentOverchargeCooldown   = OverchargeCoolDown;
        currentOverchargeActiveTime = OverchargeActiveTime;

        SoundManager.Instance.PlayOvercharge();
    }

    private void InitializeOvercharge()
    {
        foreach (ParticleSystem particle in overchargeParticles)
        {
            if (particle.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("spawn") ||
                particle.GetComponent<ParticleDisabler>().isMainTurret)
            {
                particle.Play(true);
                particle.GetComponent<ParticleDisabler>().StopParticle(OverchargeActiveTime);
            }
        }

        MainTurretController.Instance.Overcharge();

        if (canActivateSecondaryTurret == false) SecondaryTurretController.Instance.Overcharge();

        if (canActivateLaser == false) LaserTurretController.Instance.Overcharge();

        if (canActivateMortar == false) MortarTurretController.Instance.Overcharge();

        ObjectReferenceHolder.Instance.UpateRangeLines();

        Invoke(nameof(DisableOvercharge), OverchargeActiveTime);
    }

    public void CreateEmpBlast()
    {
        if (canActivateEmp                == false) return;
        if (_waveSpawner.currentWaveCount <= 0) return;

        SkillButtons.Instance.empOverlay.color = SkillButtons.Instance.cooldownColor;
        SkillButtons.Instance.empOverlayMaterial.SetFloat("_Hologram_Value_1", 1f);

        _objectPool.SpawnFromEmpParticlePool(Tags.POOL_EMP_PARTICLE, empBlastPosition.position);
        canActivateEmp = false;

        CameraShakeController.Instance.EMPShake();

        currentEMPCooldown = EmpCoolDown;

        SoundManager.Instance.PlayEMPBlast();
    }

    public void AddSecondaryTurrets()
    {
        if (canActivateSecondaryTurret    == false) return;
        if (_waveSpawner.currentWaveCount <= 0) return;

        SkillButtons.Instance.secondaryTurretOverlay.color = SkillButtons.Instance.activeColor;
        SkillButtons.Instance.secondaryTurretOverlayMaterial.SetFloat("_Hologram_Value_1", 1f);

        SoundManager.Instance.PlaySecondaryTurretDeploySound();

        Invoke(nameof(DisableSecondaryTurrets), SecondaryTurretController.ActiveTime);

        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.secondaryTurretAnimator_1,
                                           AnimatorParameters.SPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.secondaryTurretAnimator_2,
                                           AnimatorParameters.SPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        canActivateSecondaryTurret = false;

        SkillButtons.Instance.secondaryTurretActiveImg.SetActive(true);

        currentSecondaryTurretCooldown   = SecondaryTurretController.CoolDown;
        currentSecondaryTurretActiveTime = SecondaryTurretController.ActiveTime;
    }

    public void AddLaser()
    {
        if (canActivateLaser              == false) return;
        if (_waveSpawner.currentWaveCount <= 0) return;

        SkillButtons.Instance.laserOverlay.color = SkillButtons.Instance.activeColor;
        SkillButtons.Instance.laserOverlayMaterial.SetFloat("_Hologram_Value_1", 1f);

        SoundManager.Instance.PlayLaserDeploySound();

        Invoke(nameof(DisableLaser), LaserTurretController.ActiveTime);

        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.laserTurretAnimator, AnimatorParameters.SPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        canActivateLaser = false;

        SkillButtons.Instance.laserActiveImg.SetActive(true);

        currentLaserCooldown   = LaserTurretController.CoolDown;
        currentLaserActiveTime = LaserTurretController.ActiveTime;
    }

    public void AddMortar()
    {
        if (canActivateMortar             == false) return;
        if (_waveSpawner.currentWaveCount <= 0) return;

        SkillButtons.Instance.mortarOverlay.color = SkillButtons.Instance.activeColor;
        SkillButtons.Instance.mortarOverlayMaterial.SetFloat("_Hologram_Value_1", 1f);

        SoundManager.Instance.PlayMortarDeploySound();

        Invoke(nameof(DisableMortar), MortarTurretController.ActiveTime);

        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.mortarTurretAnimator,
                                           AnimatorParameters.SPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        canActivateMortar = false;

        SkillButtons.Instance.mortarActiveImg.SetActive(true);

        currentMortarCooldown   = MortarTurretController.CoolDown;
        currentMortarActiveTime = MortarTurretController.ActiveTime;
    }

    public void CreateEnergyBlast()
    {
        if (canActivateEnergy             == false) return;
        if (_waveSpawner.currentWaveCount <= 0) return;

        SkillButtons.Instance.energyOverlay.color = SkillButtons.Instance.cooldownColor;
        SkillButtons.Instance.energyOverlayMaterial.SetFloat("_Hologram_Value_1", 1f);

        _objectPool.SpawnFromEmpParticlePool(Tags.POOL_POWER_PARTICLE, empBlastPosition.position);
        canActivateEnergy = false;

        CameraShakeController.Instance.EnergyBlastShake();

        currentEnergyBlastCoolDown = EnergyCoolDown;

        SoundManager.Instance.PlayEnergyBlast();
    }

    #endregion

    #region Skill Cool Down Calculate Methods

    private void CalculateOverchargeCoolDown()
    {
        if (canActivateOvercharge == false)
        {
            if (SkillButtons.Instance.overchargeOverlay.gameObject.activeSelf == false)
                SkillButtons.Instance.overchargeOverlay.gameObject.SetActive(true);

            if (overchargeCooldownTimer >= currentOverchargeActiveTime)
            {
                if (SkillButtons.Instance.tmpOverchargeTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpOverchargeTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpOverchargeTimer.text =
                    Mathf.RoundToInt((currentOverchargeCooldown + 1) + currentOverchargeActiveTime -
                                     overchargeCooldownTimer)
                         .ToString();

                if (SkillButtons.Instance.overchargeOverlay.color != SkillButtons.Instance.cooldownColor)
                    SkillButtons.Instance.overchargeOverlay.color = SkillButtons.Instance.cooldownColor;
            }
            else
            {
                if (SkillButtons.Instance.tmpOverchargeTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpOverchargeTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpOverchargeTimer.text =
                    Mathf.RoundToInt((currentOverchargeActiveTime + 1) - overchargeCooldownTimer)
                         .ToString();
            }

            if (overchargeCooldownTimer < currentOverchargeCooldown + currentOverchargeActiveTime)
                overchargeCooldownTimer += Time.deltaTime;
            else
            {
                overchargeCooldownTimer = 0f;
                canActivateOvercharge   = true;

                SkillButtons.Instance.overchargeOverlay.gameObject.SetActive(false);
                SkillButtons.Instance.tmpOverchargeTimer.gameObject.SetActive(false);

                SkillButtons.Instance.overchargeOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);

                if (AutoOvercharge.Instance.isAutoOvercharge) TriggerOvercharge();
            }
        }
        else if (AutoOvercharge.Instance.isAutoOvercharge) TriggerOvercharge();
    }

    private void CalculateEMPCoolDown()
    {
        if (canActivateEmp == false)
        {
            if (SkillButtons.Instance.empOverlay.gameObject.activeSelf == false)
            {
                SkillButtons.Instance.empOverlay.gameObject.SetActive(true);
                SkillButtons.Instance.tmpEmpTimer.gameObject.SetActive(true);
            }

            SkillButtons.Instance.tmpEmpTimer.text =
                Mathf.RoundToInt((currentEMPCooldown + 1) - empCooldownTimer).ToString();

            if (empCooldownTimer < currentEMPCooldown)
                empCooldownTimer += Time.deltaTime;
            else
            {
                empCooldownTimer = 0f;
                canActivateEmp   = true;

                SkillButtons.Instance.empOverlay.gameObject.SetActive(false);
                SkillButtons.Instance.tmpEmpTimer.gameObject.SetActive(false);

                if (AutoEMP.Instance.isAutoEMP) CreateEmpBlast();
            }
        }
        else if (AutoEMP.Instance.isAutoEMP) CreateEmpBlast();
    }

    private void CalculateSecondaryTurretCoolDown()
    {
        if (canActivateSecondaryTurret == false)
        {
            if (SkillButtons.Instance.secondaryTurretOverlay.gameObject.activeSelf == false)
                SkillButtons.Instance.secondaryTurretOverlay.gameObject.SetActive(true);

            if (secondaryTurretCooldDownTimer >= currentSecondaryTurretActiveTime)
            {
                if (SkillButtons.Instance.tmpSecondaryTurretTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpSecondaryTurretTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpSecondaryTurretTimer.text =
                    Mathf.RoundToInt((currentSecondaryTurretCooldown + 1) + currentSecondaryTurretActiveTime -
                                     secondaryTurretCooldDownTimer).ToString();

                if (SkillButtons.Instance.secondaryTurretOverlay.color != SkillButtons.Instance.cooldownColor)
                    SkillButtons.Instance.secondaryTurretOverlay.color = SkillButtons.Instance.cooldownColor;
            }
            else
            {
                if (SkillButtons.Instance.tmpSecondaryTurretTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpSecondaryTurretTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpSecondaryTurretTimer.text =
                    Mathf.RoundToInt((currentSecondaryTurretActiveTime + 1) - secondaryTurretCooldDownTimer)
                         .ToString();
            }

            if (secondaryTurretCooldDownTimer <
                currentSecondaryTurretCooldown + currentSecondaryTurretActiveTime)
                secondaryTurretCooldDownTimer += Time.deltaTime;
            else
            {
                secondaryTurretCooldDownTimer = 0f;
                canActivateSecondaryTurret    = true;

                SkillButtons.Instance.secondaryTurretOverlay.gameObject.SetActive(false);
                SkillButtons.Instance.tmpSecondaryTurretTimer.gameObject.SetActive(false);

                SkillButtons.Instance.secondaryTurretOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);

                if (AutoSecondaryTurrets.Instance.isAutoSecondaryTurrets) AddSecondaryTurrets();
            }
        }
        else if (AutoSecondaryTurrets.Instance.isAutoSecondaryTurrets) AddSecondaryTurrets();
    }

    private void CalculateLaserCoolDown()
    {
        if (canActivateLaser == false)
        {
            if (SkillButtons.Instance.laserOverlay.gameObject.activeSelf == false)
                SkillButtons.Instance.laserOverlay.gameObject.SetActive(true);

            if (laserCoolDownTimer >= currentLaserActiveTime)
            {
                if (SkillButtons.Instance.tmpLaserTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpLaserTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpLaserTimer.text =
                    Mathf.RoundToInt((currentLaserCooldown + 1) + currentLaserActiveTime -
                                     laserCoolDownTimer)
                         .ToString();

                if (SkillButtons.Instance.laserOverlay.color != SkillButtons.Instance.cooldownColor)
                    SkillButtons.Instance.laserOverlay.color = SkillButtons.Instance.cooldownColor;
            }
            else
            {
                if (SkillButtons.Instance.tmpLaserTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpLaserTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpLaserTimer.text =
                    Mathf.RoundToInt((currentLaserActiveTime + 1) - laserCoolDownTimer)
                         .ToString();
            }

            if (laserCoolDownTimer < currentLaserCooldown + currentLaserActiveTime)
                laserCoolDownTimer += Time.deltaTime;
            else
            {
                laserCoolDownTimer = 0f;
                canActivateLaser   = true;

                SkillButtons.Instance.laserOverlay.gameObject.SetActive(false);
                SkillButtons.Instance.tmpLaserTimer.gameObject.SetActive(false);

                SkillButtons.Instance.laserOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);

                if (AutoLaser.Instance.isAutoLaser) AddLaser();
            }
        }
        else if (AutoLaser.Instance.isAutoLaser) AddLaser();
    }

    private void CalculateMortarCoolDown()
    {
        if (canActivateMortar == false)
        {
            if (SkillButtons.Instance.mortarOverlay.gameObject.activeSelf == false)
                SkillButtons.Instance.mortarOverlay.gameObject.SetActive(true);

            if (mortarCoolDownTimer >= currentMortarActiveTime)
            {
                if (SkillButtons.Instance.tmpMortarTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpMortarTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpMortarTimer.text =
                    Mathf.RoundToInt((currentMortarCooldown + 1) + currentMortarActiveTime -
                                     mortarCoolDownTimer).ToString();

                if (SkillButtons.Instance.mortarOverlay.color != SkillButtons.Instance.cooldownColor)
                    SkillButtons.Instance.mortarOverlay.color = SkillButtons.Instance.cooldownColor;
            }
            else
            {
                if (SkillButtons.Instance.tmpMortarTimer.gameObject.activeSelf == false)
                    SkillButtons.Instance.tmpMortarTimer.gameObject.SetActive(true);

                SkillButtons.Instance.tmpMortarTimer.text =
                    Mathf.RoundToInt((currentMortarActiveTime + 1) - mortarCoolDownTimer)
                         .ToString();
            }

            if (mortarCoolDownTimer < currentMortarCooldown + currentMortarActiveTime)
                mortarCoolDownTimer += Time.deltaTime;
            else
            {
                mortarCoolDownTimer = 0f;
                canActivateMortar   = true;

                SkillButtons.Instance.mortarOverlay.gameObject.SetActive(false);
                SkillButtons.Instance.tmpMortarTimer.gameObject.SetActive(false);

                SkillButtons.Instance.mortarOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);

                if (AutoMortar.Instance.isAutoMortar) AddMortar();
            }
        }
        else if (AutoMortar.Instance.isAutoMortar) AddMortar();
    }

    private void CalculateEnergyCoolDown()
    {
        if (canActivateEnergy == false)
        {
            if (SkillButtons.Instance.energyOverlay.gameObject.activeSelf == false)
            {
                SkillButtons.Instance.energyOverlay.gameObject.SetActive(true);
                SkillButtons.Instance.tmpEnergyTimer.gameObject.SetActive(true);
            }

            SkillButtons.Instance.tmpEnergyTimer.text =
                Mathf.RoundToInt((currentEnergyBlastCoolDown + 1) - energyCoolDownTimer).ToString();

            if (energyCoolDownTimer < currentEnergyBlastCoolDown)
                energyCoolDownTimer += Time.deltaTime;
            else
            {
                energyCoolDownTimer = 0f;
                canActivateEnergy   = true;

                SkillButtons.Instance.energyOverlay.gameObject.SetActive(false);
                SkillButtons.Instance.tmpEnergyTimer.gameObject.SetActive(false);

                SkillButtons.Instance.energyOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);

                if (AutoEnergyWave.Instance.isAutoEnergyWave) CreateEnergyBlast();
            }
        }
        else if (AutoEnergyWave.Instance.isAutoEnergyWave) CreateEnergyBlast();
    }

    #endregion

    private void DisableOvercharge()
    {
        // Damage
        MainTurretController.ProjectileDamage =
            UpgradeMainTurretStatsController.IncreaseUpgradeStat(MainTurretController.InitProjectileDamage,
                                                                 MainTurretController.ProjectileDamageUpgradeCount);

        SecondaryTurretController.ProjectileDamage =
            UpgradeSecondaryTurretsStatsController
                .IncreaseSecondaryTurretsDamageStat(SecondaryTurretController.InitProjectileDamage,
                                                    SecondaryTurretController.ProjectileDamageUpgradeCount);

        LaserTurretController.DamagePerSecond = LaserTurretController.InitProjectileDamagePerSecond +
                                                (LaserTurretController.ProjectileDamagePerSecondUpgradeCount *
                                                 SkillUpgradeController.Instance.fRaygunDamage);
        ;

        MortarTurretController.ProjectileDamage =
            UpgradeMortarStatsController.IncreaseMortarDamageStat(MortarTurretController.InitProjectileDamage,
                                                                  MortarTurretController.ProjectileDamageUpgradeCount);
        ;

        // Projectile Thrust
        MainTurretController.ProjectileThrust = MainTurretController.InitProjectileThrust +
                                                (MainTurretController.ProjectileThrustUpgradeCount *
                                                 MainUpgradeController.Instance.fMainTurretBulletSpeed);

        SecondaryTurretController.ProjectileThrust = SecondaryTurretController.InitProjectileThrust +
                                                     (SecondaryTurretController.ProjectileThrustUpgradeCount *
                                                      SkillUpgradeController.Instance.fCannonBulletSpeed);

        // Fire rate
        MainTurretController.FireRate = MainTurretController.InitFireRate +
                                        (MainTurretController.FireRateUpgradeCount *
                                         MainUpgradeController.Instance.fMainTurretFireRate);

        SecondaryTurretController.FireRate = SecondaryTurretController.InitFireRate +
                                             (SecondaryTurretController.FireRateUpgradeCount *
                                              SkillUpgradeController.Instance.fCannonFireRate);

        MortarTurretController.FireRate = MortarTurretController.InitFireRate +
                                          (MortarTurretController.FireRateUpgradeCount *
                                           SkillUpgradeController.Instance.fMortarFireRate);

        // Turn Speed
        MainTurretController.TurnSpeed = MainTurretController.InitTurnSpeed +
                                         (MainTurretController.TurnSpeedUpgradeCount *
                                          MainUpgradeController.Instance.fMainTurretTurnSpeed);

        SecondaryTurretController.TurnSpeed = SecondaryTurretController.InitTurnSpeed +
                                              (SecondaryTurretController.TurnSpeedUpgradeCount *
                                               SkillUpgradeController.Instance.fCannonTurnSpeed);

        LaserTurretController.TurnSpeed = LaserTurretController.InitTurnSpeed +
                                          (LaserTurretController.TurnSpeedUpgradeCount *
                                           SkillUpgradeController.Instance.fRaygunTurnSpeed);

        ObjectReferenceHolder.Instance.UpateRangeLines();

        // Disable Active Skill Icon
        SkillButtons.Instance.overchargeActiveImg.SetActive(false);
    }

    public void DisableOverchargeParticles()
    {
        foreach (ParticleDisabler p in ObjectReferenceHolder.Instance.particleDisablers) p.StopParticle();
    }

    public void DisableSecondaryTurrets()
    {
        SkillButtons.Instance.secondaryTurretActiveImg.SetActive(false);

        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.secondaryTurretAnimator_1,
                                           AnimatorParameters.DESPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.secondaryTurretAnimator_2,
                                           AnimatorParameters.DESPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        ObjectReferenceHolder.Instance.secondaryTurret1.DisableOverchargeParticle();
        ObjectReferenceHolder.Instance.secondaryTurret2.DisableOverchargeParticle();
    }

    public void DisableLaser()
    {
        SkillButtons.Instance.laserActiveImg.SetActive(false);

        ObjectReferenceHolder.Instance.laserTurret.DeactivateLaser();
        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.laserTurretAnimator,
                                           AnimatorParameters.DESPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        ObjectReferenceHolder.Instance.laserTurret.DisableOverchargeParticle();
    }

    public void DisableMortar()
    {
        SkillButtons.Instance.mortarActiveImg.SetActive(false);

        AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.mortarTurretAnimator,
                                           AnimatorParameters.DESPAWN,
                                           AnimatorParameters.ParameterTypeTrigger);

        ObjectReferenceHolder.Instance.mortarTurret.DisableOverchargeParticle();
    }

    #region Reset Timer Methods

    public void ResetAllSkillTimers()
    {
        ResetOverchargeTimers();
        ResetEmpTimer();
        ResetSecondaryTurretTimers();
        ResetLaserTimers();
        ResetMortarTimers();
        ResetEnergyTimer();
    }

    public void ResetOverchargeTimers()
    {
        if (canActivateOvercharge) return;

        DisableOvercharge();
        overchargeCooldownTimer = 0f;
        canActivateOvercharge   = true;

        if (SkillButtons.Instance.overchargeOverlay.gameObject.activeSelf)
            SkillButtons.Instance.overchargeOverlay.gameObject.SetActive(false);

        if (SkillButtons.Instance.tmpOverchargeTimer.gameObject.activeSelf)
            SkillButtons.Instance.tmpOverchargeTimer.gameObject.SetActive(false);

        SkillButtons.Instance.overchargeActiveImg.SetActive(false);
    }

    public void ResetEmpTimer()
    {
        if (canActivateEmp) return;

        empCooldownTimer = 0f;
        canActivateEmp   = true;

        if (SkillButtons.Instance.empOverlay.gameObject.activeSelf)
            SkillButtons.Instance.empOverlay.gameObject.SetActive(false);

        if (SkillButtons.Instance.tmpEmpTimer.gameObject.activeSelf)
            SkillButtons.Instance.tmpEmpTimer.gameObject.SetActive(false);
    }

    public void ResetSecondaryTurretTimers()
    {
        if (canActivateSecondaryTurret) return;

        secondaryTurretCooldDownTimer = 0f;
        canActivateSecondaryTurret    = true;

        if (SkillButtons.Instance.secondaryTurretOverlay.gameObject.activeSelf)
            SkillButtons.Instance.secondaryTurretOverlay.gameObject.SetActive(false);

        if (SkillButtons.Instance.tmpSecondaryTurretTimer.gameObject.activeSelf)
            SkillButtons.Instance.tmpSecondaryTurretTimer.gameObject.SetActive(false);

        SkillButtons.Instance.secondaryTurretActiveImg.SetActive(false);

        if (ObjectReferenceHolder.Instance.secondaryTurretAnimator_1.GetCurrentAnimatorStateInfo(0).IsName("spawn"))
        {
            AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.secondaryTurretAnimator_1,
                                               AnimatorParameters.DESPAWN,
                                               AnimatorParameters.ParameterTypeTrigger);

            ObjectReferenceHolder.Instance.secondaryTurret1.DisableOverchargeParticle();
        }

        if (ObjectReferenceHolder.Instance.secondaryTurretAnimator_2.GetCurrentAnimatorStateInfo(0).IsName("spawn"))
        {
            AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.secondaryTurretAnimator_2,
                                               AnimatorParameters.DESPAWN,
                                               AnimatorParameters.ParameterTypeTrigger);

            ObjectReferenceHolder.Instance.secondaryTurret2.DisableOverchargeParticle();
        }

        CancelInvoke(nameof(DisableSecondaryTurrets));
    }

    public void ResetLaserTimers()
    {
        if (canActivateLaser) return;

        laserCoolDownTimer = 0f;
        canActivateLaser   = true;

        if (SkillButtons.Instance.laserOverlay.gameObject.activeSelf)
            SkillButtons.Instance.laserOverlay.gameObject.SetActive(false);

        if (SkillButtons.Instance.tmpLaserTimer.gameObject.activeSelf)
            SkillButtons.Instance.tmpLaserTimer.gameObject.SetActive(false);

        SkillButtons.Instance.laserActiveImg.SetActive(false);

        ObjectReferenceHolder.Instance.laserTurret.DeactivateLaser();

        if (ObjectReferenceHolder.Instance.laserTurretAnimator.GetCurrentAnimatorStateInfo(0).IsName("spawn"))
        {
            AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.laserTurretAnimator,
                                               AnimatorParameters.DESPAWN,
                                               AnimatorParameters.ParameterTypeTrigger);

            ObjectReferenceHolder.Instance.laserTurret.DisableOverchargeParticle();
        }

        CancelInvoke(nameof(DisableLaser));
    }

    public void ResetMortarTimers()
    {
        if (canActivateMortar) return;

        mortarCoolDownTimer = 0f;
        canActivateMortar   = true;

        if (SkillButtons.Instance.mortarOverlay.gameObject.activeSelf)
            SkillButtons.Instance.mortarOverlay.gameObject.SetActive(false);

        if (SkillButtons.Instance.tmpMortarTimer.gameObject.activeSelf)
            SkillButtons.Instance.tmpMortarTimer.gameObject.SetActive(false);

        SkillButtons.Instance.mortarActiveImg.SetActive(false);

        if (ObjectReferenceHolder.Instance.mortarTurretAnimator.GetCurrentAnimatorStateInfo(0).IsName("spawn"))
        {
            AnimatorParam.SetAnimatorParameter(ObjectReferenceHolder.Instance.mortarTurretAnimator,
                                               AnimatorParameters.DESPAWN,
                                               AnimatorParameters.ParameterTypeTrigger);

            ObjectReferenceHolder.Instance.mortarTurret.DisableOverchargeParticle();
        }

        CancelInvoke(nameof(DisableMortar));
    }

    public void ResetEnergyTimer()
    {
        if (canActivateEnergy) return;

        energyCoolDownTimer = 0f;
        canActivateEnergy   = true;

        if (SkillButtons.Instance.energyOverlay.gameObject.activeSelf)
            SkillButtons.Instance.energyOverlay.gameObject.SetActive(false);

        if (SkillButtons.Instance.tmpEnergyTimer.gameObject.activeSelf)
            SkillButtons.Instance.tmpEnergyTimer.gameObject.SetActive(false);
    }

    //upgrade skills functions

    //Overcharge
    public static void DecreaseOverchargeCooldown(float value)
    {
        if (SkillUpgradeController.Instance.bSkillOverchargeCoolDown.interactable == false) return;

        if (ResourcesController.SciencePoints > PriceController.CalculatePrice(OverchargeCoolDownPrice,
                                                                               OverchargeCoolDownUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeOverchargeCooldownPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(OverchargeCoolDownPrice,
                                                                                OverchargeCoolDownUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeOverchargeCooldownPriceMultiplier);

            if (OverchargeCoolDown + OverchargeActiveTime - value >= 0)
            {
                OverchargeCoolDownUpgradeCount++;
                OverchargeCoolDown = InitOverchargeCoolDown - (OverchargeCoolDownUpgradeCount * value);
            }
        }

        if (OverchargeCoolDownUpgradeCount == UpgradeCountController.Instance.SkillOverchargeCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillOverchargeCoolDown.interactable = false;
    }

    public static void IncreaseOverchargeDuration(float value)
    {
        if (SkillUpgradeController.Instance.bSkillOverchargeActiveTime.interactable == false) return;

        if (ResourcesController.SciencePoints > PriceController.CalculatePrice(OverchargeActiveDurationPrice,
                                                                               OverchargeActiveDurationUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeOverchargeActiveTimePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(OverchargeActiveDurationPrice,
                                                                                OverchargeActiveDurationUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeOverchargeActiveTimePriceMultiplier);

            OverchargeActiveDurationUpgradeCount++;
            OverchargeActiveTime = InitOverchargeActiveTime + (OverchargeActiveDurationUpgradeCount * value);
        }

        if (OverchargeActiveDurationUpgradeCount ==
            UpgradeCountController.Instance.SkillOverchargeActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillOverchargeActiveTime.interactable = false;
    }

    public static void IncreaseOverchargePowerup(float value)
    {
        if (SkillUpgradeController.Instance.bSkilloverchargePowerup.interactable == false) return;

        if (ResourcesController.SciencePoints > PriceController.CalculatePrice(OverchargePowerupPrice,
                                                                               OverchargePowerupUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeOverchargePowerupPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(OverchargePowerupPrice,
                                                                                OverchargePowerupUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeOverchargePowerupPriceMultiplier);

            OverchargePowerupUpgradeCount++;
            OverchargePowerup = InitOverchargePowerup + (OverchargePowerupUpgradeCount * value);
        }

        if (OverchargePowerupUpgradeCount ==
            UpgradeCountController.Instance.SkillOverchargePowerupUpgradeCount)
            SkillUpgradeController.Instance.bSkilloverchargePowerup.interactable = false;
    }

    //EMP Burst
    public static void DecreaseEMPCooldown(float value)
    {
        if (SkillUpgradeController.Instance.bSkillEMPCoolDown.interactable == false) return;

        if (ResourcesController.SciencePoints > PriceController.CalculatePrice(EmpCoolDownPrice,
                                                                               EmpCoolDownUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeEMPCooldownPriceMultiplier))
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(EmpCoolDownPrice,
                                                                                EmpCoolDownUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeEMPCooldownPriceMultiplier);

            if (EmpCoolDown - value >= 0)
            {
                EmpCoolDownUpgradeCount++;
                EmpCoolDown = InitEMPCoolDown - (EmpCoolDownUpgradeCount * value);
            }
        }

        if (EmpCoolDownUpgradeCount == UpgradeCountController.Instance.SkillEMPCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillEMPCoolDown.interactable = false;
    }

    public static void IncreaseEMPStunDuration(float value)
    {
        if (SkillUpgradeController.Instance.bSkillEMPStunDuration.interactable == false) return;

        if (ResourcesController.SciencePoints > PriceController.CalculatePrice(EmpStunDurationPrice,
                                                                               EmpStunDurationUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeEMPStunDurationPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(EmpStunDurationPrice,
                                                                                EmpStunDurationUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeEMPStunDurationPriceMultiplier);

            EmpStunDurationUpgradeCount++;
            EmpStunDuration = InitEMPStunDuration + (EmpStunDurationUpgradeCount * value);
        }

        if (EmpStunDurationUpgradeCount ==
            UpgradeCountController.Instance.SkillEMPStunDurationUpgradeCount)
            SkillUpgradeController.Instance.bSkillEMPStunDuration.interactable = false;
    }

    #endregion

    public static void DecreaseEnergyWaveCooldown(float value)
    {
        if (SkillUpgradeController.Instance.bSkillEnergyBlastCoolDown.interactable == false) return;

        if (ResourcesController.SciencePoints > PriceController.CalculatePrice(EnergyBlastCoolDownPrice,
                                                                               EnergyBlastCoolDownUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeEnergyBlastCooldownPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(EnergyBlastCoolDownPrice,
                                                                                EnergyBlastCoolDownUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeEnergyBlastCooldownPriceMultiplier);

            if (EnergyCoolDown - value >= 0)
            {
                EnergyBlastCoolDownUpgradeCount++;
                EnergyCoolDown = InitEnergyCoolDown - (EnergyBlastCoolDownUpgradeCount * value);
            }
        }

        if (EnergyBlastCoolDownUpgradeCount == UpgradeCountController.Instance.SkillEnergyBlastCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillEnergyBlastCoolDown.interactable = false;
    }

    public static void IncreaseEnergyWaveDamage(float value)
    {
        if (SkillUpgradeController.Instance.bSkillEnergyBlastDamage.interactable == false) return;

        if (ResourcesController.SciencePoints > PriceController.CalculatePrice(EnergyBlastDamagePrice,
                                                                               EnergyBlastDamageUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeEnergyBlastDamagePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(EnergyBlastDamagePrice,
                                                                                EnergyBlastDamageUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeEnergyBlastDamagePriceMultiplier);

            EnergyBlastDamageUpgradeCount++;
            EnergyBlastDamage = UpgradeSkillsStatsController.IncreaseEnergyBlastDamageStat(InitEnergyBlastDamage,
                                                                                           EnergyBlastDamageUpgradeCount);
        }

        if (EnergyBlastDamageUpgradeCount == UpgradeCountController.Instance.SkillEnergyBlastDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillEnergyBlastDamage.interactable = false;
    }

    public void LoadStats()
    {
        OverchargeCoolDown = InitOverchargeCoolDown -
                             (OverchargeCoolDownUpgradeCount * SkillUpgradeController.Instance.fOverchargeCooldown);
        OverchargeActiveTime = InitOverchargeActiveTime +
                               (OverchargeActiveDurationUpgradeCount *
                                SkillUpgradeController.Instance.fOverchargeDuration);
        OverchargePowerup = InitOverchargePowerup +
                            (OverchargePowerupUpgradeCount * SkillUpgradeController.Instance.fOverchargePowerup);
        EmpCoolDown = InitEMPCoolDown - (EmpCoolDownUpgradeCount * SkillUpgradeController.Instance.fEMPCooldown);
        EmpStunDuration = InitEMPStunDuration +
                          (EmpStunDurationUpgradeCount * SkillUpgradeController.Instance.fEMPStunDuration);
        EnergyCoolDown = InitEnergyCoolDown -
                         (EnergyBlastCoolDownUpgradeCount * SkillUpgradeController.Instance.fEnergyWaveCooldown);

        EnergyBlastDamage = UpgradeSkillsStatsController.IncreaseEnergyBlastDamageStat(InitEnergyBlastDamage,
                                                                                       EnergyBlastDamageUpgradeCount);

        DisableMaxedUpgradeButtons();
    }

    private void DisableMaxedUpgradeButtons()
    {
        if (OverchargeCoolDownUpgradeCount == UpgradeCountController.Instance.SkillOverchargeCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillOverchargeCoolDown.interactable = false;
        if (OverchargeActiveDurationUpgradeCount ==
            UpgradeCountController.Instance.SkillOverchargeActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillOverchargeActiveTime.interactable = false;
        if (OverchargePowerupUpgradeCount ==
            UpgradeCountController.Instance.SkillOverchargePowerupUpgradeCount)
            SkillUpgradeController.Instance.bSkilloverchargePowerup.interactable = false;
        if (EmpCoolDownUpgradeCount == UpgradeCountController.Instance.SkillEMPCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillEMPCoolDown.interactable = false;
        if (EmpStunDurationUpgradeCount ==
            UpgradeCountController.Instance.SkillEMPStunDurationUpgradeCount)
            SkillUpgradeController.Instance.bSkillEMPStunDuration.interactable = false;
        if (EnergyBlastCoolDownUpgradeCount == UpgradeCountController.Instance.SkillEnergyBlastCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillEnergyBlastCoolDown.interactable = false;
        if (EnergyBlastDamageUpgradeCount == UpgradeCountController.Instance.SkillEnergyBlastDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillEnergyBlastDamage.interactable = false;
    }
}