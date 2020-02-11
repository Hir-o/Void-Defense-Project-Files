using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class SecondaryTurretController : TurretController
{
    public static SecondaryTurretController Instance;

    [BoxGroup("Secondary Turrets Values")]
    public float coolDown, activeTime, projectileDamage, projectileThrust, fireRate, turnSpeed, range;

    [ShowNonSerializedField]
    public static float CoolDown, ActiveTime, ProjectileDamage, ProjectileThrust, FireRate, TurnSpeed, Range;

    [BoxGroup("Prices")] [SerializeField]
    private float coolDownPrice,
                  activeTimePrice,
                  projectileDamagePrice,
                  projectileThrustPrice,
                  fireRatePrice,
                  turnSpeedPrice,
                  rangePrice;

    [ShowNonSerializedField]
    public static float CoolDownPrice,
                        ActiveTimePrice,
                        ProjectileDamagePrice,
                        ProjectileThrustPrice,
                        FireRatePrice,
                        TurnSpeedPrice,
                        RangePrice;

    [ShowNonSerializedField]
    public static int CoolDownUpgradeCount,
                      ActiveTimeUpgradeCount,
                      ProjectileDamageUpgradeCount,
                      ProjectileThrustUpgradeCount,
                      FireRateUpgradeCount,
                      TurnSpeedUpgradeCount,
                      RangeUpgradeCount;

    [ShowNonSerializedField]
    public static float InitProjectileDamage,
                        InitCoolDown,
                        InitActiveTime,
                        InitProjectileThrust,
                        InitFireRate,
                        InitTurnSpeed,
                        InitRange;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UpdateTurretStats();
    }

    public override void UpdateTurretStats()
    {
        InitCoolDown         = coolDown;
        InitActiveTime       = activeTime;
        InitProjectileDamage = projectileDamage;
        InitProjectileThrust = projectileThrust;
        InitFireRate         = fireRate;
        InitTurnSpeed        = turnSpeed;
        InitRange            = range;

        CoolDown         = coolDown;
        ActiveTime       = activeTime;
        ProjectileDamage = projectileDamage;
        ProjectileThrust = projectileThrust;
        FireRate         = fireRate;
        TurnSpeed        = turnSpeed;
        Range            = range;

        CoolDownPrice         = coolDownPrice;
        ActiveTimePrice       = activeTimePrice;
        ProjectileDamagePrice = projectileDamagePrice;
        ProjectileThrustPrice = projectileThrustPrice;
        FireRatePrice         = fireRatePrice;
        TurnSpeedPrice        = turnSpeedPrice;
        RangePrice            = rangePrice;

        ObjectReferenceHolder.Instance.rangeLineSecondaryTurret1.radius = range;
        ObjectReferenceHolder.Instance.rangeLineSecondaryTurret2.radius = range;
    }

    public override void Overcharge()
    {
        ProjectileDamage =  SkillsController.OverchargePowerup;
//        ProjectileThrust *= SkillsController.OverchargePowerup;
        ProjectileThrust *= 1.2f;
        FireRate         *= SkillsController.OverchargePowerup;
        TurnSpeed        *= SkillsController.OverchargePowerup;

        ObjectReferenceHolder.Instance.rangeLineSecondaryTurret1.radius = Range;
        ObjectReferenceHolder.Instance.rangeLineSecondaryTurret2.radius = Range;
    }

    #region Ugrade Methods

    public static void DecreaseCooldown(float value)
    {
        if (SkillUpgradeController.Instance.bSkillSecondaryTurretsCoolDown.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(CoolDownPrice,
                                                                               CoolDownUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeSecondaryTurretsCoolDownPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(CoolDownPrice,
                                                                                CoolDownUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeSecondaryTurretsCoolDownPriceMultiplier);

            if (CoolDown + ActiveTime - value >= 0)
            {
                CoolDownUpgradeCount++;
                CoolDown = InitCoolDown - (CoolDownUpgradeCount * value);
            }
        }

        if (CoolDownUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsCoolDown.interactable = false;
    }

    public static void IncreaseActiveDuration(float value)
    {
        if (SkillUpgradeController.Instance.bSkillSecondaryTurretsActiveTime.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(ActiveTimePrice,
                                                                               ActiveTimeUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeSecondaryTurretsActiveTimePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(ActiveTimePrice,
                                                                                ActiveTimeUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeSecondaryTurretsActiveTimePriceMultiplier);

            ActiveTimeUpgradeCount++;
            ActiveTime = InitActiveTime + (ActiveTimeUpgradeCount * value);
        }

        if (ActiveTimeUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsActiveTime.interactable = false;
    }

    public static void IncreaseDamage(float value)
    {
        if (SkillUpgradeController.Instance.bSkillSecondaryTurretsDamage.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(ProjectileDamagePrice,
                                                                               ProjectileDamageUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeSecondaryTurretsDamagePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(ProjectileDamagePrice,
                                                                                ProjectileDamageUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeSecondaryTurretsDamagePriceMultiplier);

            ProjectileDamageUpgradeCount++;
            ProjectileDamage =
                UpgradeSecondaryTurretsStatsController.IncreaseSecondaryTurretsDamageStat(InitProjectileDamage,
                                                                                          ProjectileDamageUpgradeCount);
        }

        if (ProjectileDamageUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsDamage.interactable = false;
    }

    public static void IncreaseBulletSpeed(float value)
    {
        if (SkillUpgradeController.Instance.bSkillSecondaryTurretsProjectileThrust.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(ProjectileThrustPrice,
                                                                               ProjectileThrustUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeSecondaryTurretsProjectileSpeedPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(ProjectileThrustPrice,
                                                                                ProjectileThrustUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeSecondaryTurretsProjectileSpeedPriceMultiplier);

            ProjectileThrustUpgradeCount++;
            ProjectileThrust = InitProjectileThrust + (ProjectileThrustUpgradeCount * value);
        }

        if (ProjectileThrustUpgradeCount ==
            UpgradeCountController.Instance.SkillSecondaryTurretsProjectileThrustUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsProjectileThrust.interactable = false;
    }

    public static void IncreaseFireRate(float fireRatevalue, float thrustValue, float turnSpeedValue)
    {
        if (SkillUpgradeController.Instance.bSkillSecondaryTurretsFireRate.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(FireRatePrice,
                                                                               FireRateUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeSecondaryTurretsFireRatePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(FireRatePrice,
                                                                                FireRateUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeSecondaryTurretsFireRatePriceMultiplier);

            FireRateUpgradeCount++;
            FireRate = InitFireRate + (FireRateUpgradeCount * fireRatevalue);
            
            ProjectileThrustUpgradeCount++;
            ProjectileThrust = InitProjectileThrust + (ProjectileThrustUpgradeCount * thrustValue);
            
            TurnSpeedUpgradeCount++;
            TurnSpeed = InitTurnSpeed + (TurnSpeedUpgradeCount * turnSpeedValue);
        }

        if (FireRateUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsFireRateUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsFireRate.interactable = false;
    }

    public static void IncreaseTurnSpeed(float value)
    {
        if (SkillUpgradeController.Instance.bSkillSecondaryTurretsTurnSpeed.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(TurnSpeedPrice,
                                                                               TurnSpeedUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeSecondaryTurretsTurnSpeedPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(TurnSpeedPrice,
                                                                                TurnSpeedUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeSecondaryTurretsTurnSpeedPriceMultiplier);

            TurnSpeedUpgradeCount++;
            TurnSpeed = InitTurnSpeed + (TurnSpeedUpgradeCount * value);
        }

        if (TurnSpeedUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsTurnSpeedUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsTurnSpeed.interactable = false;
    }

    public static void IncreaseRange(float value)
    {
        if (SkillUpgradeController.Instance.bSkillSecondaryTurretsRange.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(RangePrice,
                                                                               RangeUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeSecondaryTurretsRangePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(RangePrice,
                                                                                RangeUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeSecondaryTurretsRangePriceMultiplier);

            RangeUpgradeCount++;
            Range = InitRange + (RangeUpgradeCount * value);

            ObjectReferenceHolder.Instance.rangeLineSecondaryTurret1.radius = Range;
            ObjectReferenceHolder.Instance.rangeLineSecondaryTurret2.radius = Range;
            ObjectReferenceHolder.Instance.rangeLineSecondaryTurret1.CreatePoints();
            ObjectReferenceHolder.Instance.rangeLineSecondaryTurret2.CreatePoints();
        }

        if (RangeUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsRangeUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsRange.interactable = false;
    }

    #endregion

    public void LoadStats()
    {
        CoolDown   = InitCoolDown   - (CoolDownUpgradeCount   * SkillUpgradeController.Instance.fCannonCooldown);
        ActiveTime = InitActiveTime + (ActiveTimeUpgradeCount * SkillUpgradeController.Instance.fCannonDuration);

        ProjectileDamage =
            UpgradeSecondaryTurretsStatsController.IncreaseSecondaryTurretsDamageStat(InitProjectileDamage,
                                                                                      ProjectileDamageUpgradeCount);

        ProjectileThrust = InitProjectileThrust + (ProjectileThrustUpgradeCount * SkillUpgradeController.Instance.fCannonBulletSpeed);
        FireRate         = InitFireRate         + (FireRateUpgradeCount         * SkillUpgradeController.Instance.fCannonFireRate);
        TurnSpeed        = InitTurnSpeed        + (TurnSpeedUpgradeCount        * SkillUpgradeController.Instance.fCannonTurnSpeed);
        Range            = InitRange            + (RangeUpgradeCount            * SkillUpgradeController.Instance.fCannonRange);

        ObjectReferenceHolder.Instance.rangeLineSecondaryTurret1.radius = Range;
        ObjectReferenceHolder.Instance.rangeLineSecondaryTurret2.radius = Range;
        
        DisableMaxedUpgradeButtons();
        ObjectReferenceHolder.Instance.UpateRangeLines();
    }

    private void DisableMaxedUpgradeButtons()
    {
        if (CoolDownUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsCoolDown.interactable = false;
        if (ActiveTimeUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsActiveTime.interactable = false;
        if (ProjectileDamageUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsDamage.interactable = false;
        if (ProjectileThrustUpgradeCount ==
            UpgradeCountController.Instance.SkillSecondaryTurretsProjectileThrustUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsProjectileThrust.interactable = false;
        if (FireRateUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsFireRateUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsFireRate.interactable = false;
        if (TurnSpeedUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsTurnSpeedUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsTurnSpeed.interactable = false;
        if (RangeUpgradeCount == UpgradeCountController.Instance.SkillSecondaryTurretsRangeUpgradeCount)
            SkillUpgradeController.Instance.bSkillSecondaryTurretsRange.interactable = false;
    }
}