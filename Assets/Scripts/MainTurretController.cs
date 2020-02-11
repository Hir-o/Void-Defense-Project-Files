using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class MainTurretController : TurretController
{
    public static MainTurretController Instance;

    #region non-static variables

    [BoxGroup("Main Turret Values")]
    public float health,
                 projectileDamage,
                 projectileThrust,
                 fireRate,
                 turnSpeed,
                 range,
                 regen,
                 lifesteal,
                 defense,
                 blockChance,
                 bounceChance,
                 bounceAmount,
                 critChance,
                 critDamage;

    [BoxGroup("Initial Prices")] [SerializeField]
    private float totalHealthPrice,
                  projectileDamagePrice,
                  projectileThrustPrice,
                  fireRatePrice,
                  turnSpeedPrice,
                  rangePrice,
                  regenPrice,
                  lifestealPrice,
                  defensePrice,
                  blockChancePrice,
                  bounceChancePrice,
                  bounceAmountPrice,
                  critChancePrice,
                  critDamagePrice;

    #endregion

    #region static variables

    [ShowNonSerializedField]
    public static float TotalHealth,
                        Health, // mos krijo metode per kit sen
                        ProjectileDamage,
                        ProjectileThrust,
                        FireRate,
                        TurnSpeed,
                        Range,
                        Regen,
                        Lifesteal,
                        Defense,
                        BlockChance,
                        BounceChance,
                        BounceAmount,
                        CritChance,
                        CritDamage;

    [ShowNonSerializedField]
    public static float TotalHealthPrice,
                        ProjectileDamagePrice,
                        ProjectileThrustPrice,
                        FireRatePrice,
                        TurnSpeedPrice,
                        RangePrice,
                        RegenPrice,
                        LifestealPrice,
                        DefensePrice,
                        BlockChancePrice,
                        BounceChancePrice,
                        BounceAmountPrice,
                        CritChancePrice,
                        CritDamagePrice;

    [ShowNonSerializedField]
    public static int TotalHealthUpgradeCount,
                      ProjectileDamageUpgradeCount,
                      ProjectileThrustUpgradeCount,
                      FireRateUpgradeCount,
                      TurnSpeedUpgradeCount,
                      RangeUpgradeCount,
                      RegenUpgradeCount,
                      LifestealUpgradeCount,
                      DefenseUpgradeCount,
                      BlockChanceUpgradeCount,
                      BounceChanceUpgradeCount,
                      BounceAmountUpgradeCount,
                      CritChanceUpgradeCount,
                      CritDamageUpgradeCount;

    [ShowNonSerializedField]
    public static float InitProjectileDamage,
                        InitProjectileThrust,
                        InitFireRate,
                        InitTurnSpeed,
                        InitRange,
                        InitRegen,
                        InitLifesteal,
                        InitDefense,
                        InitBlockChance,
                        InitBounceChance,
                        InitBounceAmount,
                        InitCritChance,
                        InitCritDamage;

    #endregion

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
        InitProjectileDamage = projectileDamage;
        InitProjectileThrust = projectileThrust;
        InitFireRate         = fireRate;
        InitTurnSpeed        = turnSpeed;
        InitRange            = range;
        InitRegen            = regen;
        InitLifesteal        = lifesteal;
        InitDefense          = defense;
        InitBlockChance      = blockChance;
        InitBounceChance     = bounceChance;
        InitBounceAmount     = bounceAmount;
        InitCritChance       = critChance;
        InitCritDamage       = critDamage;

        Health           = health;
        TotalHealth      = Health;
        ProjectileDamage = projectileDamage;
        ProjectileThrust = projectileThrust;
        FireRate         = fireRate;
        TurnSpeed        = turnSpeed;
        Range            = range;
        Regen = regen;

        Lifesteal    = lifesteal;
        Defense      = defense;
        BlockChance  = blockChance;
        BounceChance = bounceChance;
        BounceAmount = bounceAmount;
        CritChance   = critChance;
        CritDamage   = critDamage;

        ObjectReferenceHolder.Instance.rangeLineMainTurret.radius = range;

        TotalHealthPrice      = totalHealthPrice;
        ProjectileDamagePrice = projectileDamagePrice;
        ProjectileThrustPrice = projectileThrustPrice;
        FireRatePrice         = fireRatePrice;
        TurnSpeedPrice        = turnSpeedPrice;
        RangePrice            = rangePrice;
        RegenPrice            = regenPrice;
        LifestealPrice        = lifestealPrice;
        DefensePrice          = defensePrice;
        BlockChancePrice      = blockChancePrice;
        BounceChancePrice     = bounceChancePrice;
        BounceAmountPrice     = bounceAmountPrice;
        CritChancePrice       = critChancePrice;
        CritDamagePrice       = critDamagePrice;
    }

    public override void Overcharge()
    {
        ProjectileDamage *= SkillsController.OverchargePowerup;
//        ProjectileThrust *= SkillsController.OverchargePowerup;
        ProjectileThrust *= 1.2f;
        FireRate         *= SkillsController.OverchargePowerup;
        TurnSpeed        *= SkillsController.OverchargePowerup;

        ObjectReferenceHolder.Instance.rangeLineMainTurret.radius = Range;
    }

    ////////////////////////////////////////////////////////////////
    public static void IncreaseTotalHealth(float value)
    {
        // For version 2.0
    }

    public static void IncreaseProjectileDamage(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretDamage.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(ProjectileDamagePrice,
                                                                              ProjectileDamageUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretDamagePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(ProjectileDamagePrice,
                                                                               ProjectileDamageUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretDamagePriceMultiplier);

            ProjectileDamageUpgradeCount++;
            ProjectileDamage = UpgradeMainTurretStatsController.IncreaseUpgradeStat(InitProjectileDamage,
                                                                                    ProjectileDamageUpgradeCount);
        }

        if (ProjectileDamageUpgradeCount == UpgradeCountController.Instance.MainTurretProjectileDamageUpgradeCount)
            MainUpgradeController.Instance.bMainTurretDamage.interactable = false;
    }

    public static void IncreaseBulletSpeed(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretBulletSpeed.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(ProjectileThrustPrice,
                                                                              ProjectileThrustUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretProjectileSpeedPriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(ProjectileThrustPrice,
                                                                               ProjectileThrustUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretProjectileSpeedPriceMultiplier);

            ProjectileThrustUpgradeCount++;
            ProjectileThrust = InitProjectileThrust + (ProjectileThrustUpgradeCount * value);
        }

        if (ProjectileThrustUpgradeCount == UpgradeCountController.Instance.MainTurretProjectileThrustUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBulletSpeed.interactable = false;
    }

    public static void IncreaseFireRate(float fireRateValue, float thrustValue, float turnSpeedValue)
    {
        if (MainUpgradeController.Instance.bMainTurretFireRate.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(FireRatePrice,
                                                                              FireRateUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretFireRatePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(FireRatePrice,
                                                                               FireRateUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretFireRatePriceMultiplier);

            FireRateUpgradeCount++;
            FireRate = InitFireRate + (FireRateUpgradeCount * fireRateValue);
            
            ProjectileThrustUpgradeCount++;
            ProjectileThrust = InitProjectileThrust + (ProjectileThrustUpgradeCount * thrustValue);
            
            TurnSpeedUpgradeCount++;
            TurnSpeed = InitTurnSpeed + (TurnSpeedUpgradeCount * turnSpeedValue);
        }

        if (FireRateUpgradeCount == UpgradeCountController.Instance.MainTurretFireRateUpgradeCount)
            MainUpgradeController.Instance.bMainTurretFireRate.interactable = false;
    }

    public static void IncreaseTurnSpeed(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretTurnSpeed.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(TurnSpeedPrice,
                                                                              TurnSpeedUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretTurnSpeedPriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(TurnSpeedPrice,
                                                                               TurnSpeedUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretTurnSpeedPriceMultiplier);

            TurnSpeedUpgradeCount++;
            TurnSpeed = InitTurnSpeed + (TurnSpeedUpgradeCount * value);
        }

        if (TurnSpeedUpgradeCount == UpgradeCountController.Instance.MainTurretTurnSpeedUpgradeCount)
            MainUpgradeController.Instance.bMainTurretTurnSpeed.interactable = false;
    }

    public static void IncreaseRange(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretRange.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(RangePrice,
                                                                              RangeUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretRangePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(RangePrice,
                                                                               RangeUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretRangePriceMultiplier);

            RangeUpgradeCount++;
            Range = InitRange + (RangeUpgradeCount * value);

            ObjectReferenceHolder.Instance.rangeLineMainTurret.radius = Range;
            ObjectReferenceHolder.Instance.rangeLineMainTurret.CreatePoints();
        }

        if (RangeUpgradeCount == UpgradeCountController.Instance.MainTurretRangeUpgradeCount)
            MainUpgradeController.Instance.bMainTurretRange.interactable = false;
    }

    public static void IncreaseRegen(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretRegen.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(RegenPrice,
                                                                              RegenUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretRegenPriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(RegenPrice,
                                                                               RegenUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretRegenPriceMultiplier);

            RegenUpgradeCount++;
            Regen = UpgradeMainTurretStatsController.IncreaseMainTurretRegenStat(InitRegen,
                                                                                 RegenUpgradeCount);
        }

        if (RegenUpgradeCount == UpgradeCountController.Instance.MainTurretRegenUpgradeCount)
            MainUpgradeController.Instance.bMainTurretRegen.interactable = false;
    }

    public static void IncreaseLifeSteal(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretLifeSteal.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(LifestealPrice,
                                                                              LifestealUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretLifeStealPriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(LifestealPrice,
                                                                               LifestealUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretLifeStealPriceMultiplier);

            LifestealUpgradeCount++;
            Lifesteal = InitLifesteal + (LifestealUpgradeCount * value);
        }

        if (LifestealUpgradeCount == UpgradeCountController.Instance.MainTurretLifestealUpgradeCount)
            MainUpgradeController.Instance.bMainTurretLifeSteal.interactable = false;
    }

    public static void IncreaseDefense(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretDefense.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(DefensePrice,
                                                                              DefenseUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretDefensePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(DefensePrice,
                                                                               DefenseUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretDefensePriceMultiplier);

            DefenseUpgradeCount++;
            Defense = InitDefense + (DefenseUpgradeCount * value);
        }

        if (DefenseUpgradeCount == UpgradeCountController.Instance.MainTurretDefenseUpgradeCount)
            MainUpgradeController.Instance.bMainTurretDefense.interactable = false;
    }

    public static void IncreaseBlockChance(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretBlockChance.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(BlockChancePrice,
                                                                              BlockChanceUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretBlockChancePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(BlockChancePrice,
                                                                               BlockChanceUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretBlockChancePriceMultiplier);

            BlockChanceUpgradeCount++;
            BlockChance = InitBlockChance + (BlockChanceUpgradeCount * value);
        }

        if (BlockChanceUpgradeCount == UpgradeCountController.Instance.MainTurretBlockChanceUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBlockChance.interactable = false;
    }

    public static void IncreaseCriticalChance(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretCriticalChance.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(CritChanceUpgradeCount,
                                                                              CritChanceUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretCritChancePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(CritChanceUpgradeCount,
                                                                               CritChanceUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretCritChancePriceMultiplier);

            CritChanceUpgradeCount++;
            CritChance = InitCritChance + (CritChanceUpgradeCount * value);
        }

        if (CritChanceUpgradeCount == UpgradeCountController.Instance.MainTurretCritChanceUpgradeCount)
            MainUpgradeController.Instance.bMainTurretCriticalChance.interactable = false;
    }

    public static void IncreaseCriticalDamage(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretCricticalDamage.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(CritDamagePrice,
                                                                              CritDamageUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretCritDamagePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(CritDamagePrice,
                                                                               CritDamageUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretCritDamagePriceMultiplier);

            CritDamageUpgradeCount++;
            CritDamage = InitCritDamage + (CritDamageUpgradeCount * value);
        }

        if (CritDamageUpgradeCount == UpgradeCountController.Instance.MainTurretCritDamageUpgradeCount)
            MainUpgradeController.Instance.bMainTurretCricticalDamage.interactable = false;
    }

    public static void IncreaseBounceChance(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretBounceChance.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(BounceChancePrice,
                                                                              BounceChanceUpgradeCount,
                                                                              PriceController
                                                                                  .Instance
                                                                                  .upgradeMainTurretBounceChancePriceMultiplier)
        )
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(BounceChancePrice,
                                                                               BounceChanceUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMainTurretBounceChancePriceMultiplier);

            BounceChanceUpgradeCount++;
            BounceChance = InitBounceChance + (BounceChanceUpgradeCount * value);
        }

        if (BounceChanceUpgradeCount == UpgradeCountController.Instance.MainTurretBounceChanceUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBounceChance.interactable = false;
    }

    public static void IncreaseBounceAmount(float value)
    {
        if (MainUpgradeController.Instance.bMainTurretBounceAmount.interactable == false) return;

        if (ResourcesController.EnergyPoints >= PriceController.CalculatePrice(BounceAmountPrice,
                                                                              BounceAmountUpgradeCount,
                                                                              PriceController.UpgradePriceMultiplier))
        {
            ResourcesController.EnergyPoints -= PriceController.CalculatePrice(BounceAmountPrice,
                                                                               BounceAmountUpgradeCount,
                                                                               PriceController.UpgradePriceMultiplier);

            BounceAmountUpgradeCount++;
            BounceAmount = InitBounceAmount + (BounceAmountUpgradeCount * value);
        }

        if (BounceAmountUpgradeCount == UpgradeCountController.Instance.MainTurretBounceAmountUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBounceAmount.interactable = false;
    }

    public void LoadStats()
    {
        ProjectileDamage =
            UpgradeMainTurretStatsController.IncreaseUpgradeStat(InitProjectileDamage, ProjectileDamageUpgradeCount);

        ProjectileThrust = InitProjectileThrust +
                           (ProjectileThrustUpgradeCount * MainUpgradeController.Instance.fMainTurretBulletSpeed);
        FireRate  = InitFireRate  + (FireRateUpgradeCount  * MainUpgradeController.Instance.fMainTurretFireRate);
        TurnSpeed = InitTurnSpeed + (TurnSpeedUpgradeCount * MainUpgradeController.Instance.fMainTurretTurnSpeed);
        Range     = InitRange     + (RangeUpgradeCount     * MainUpgradeController.Instance.fMainTurretRange);
        
        Regen = UpgradeMainTurretStatsController.IncreaseMainTurretRegenStat(InitRegen, RegenUpgradeCount);

        Lifesteal = InitLifesteal + (LifestealUpgradeCount * MainUpgradeController.Instance.fMainTurretLifeSteal);
        Defense   = InitDefense   + (DefenseUpgradeCount   * MainUpgradeController.Instance.fMainTurretDefense);
        BlockChance = InitBlockChance +
                      (BlockChanceUpgradeCount * MainUpgradeController.Instance.fMainTurretBlockChance);
        CritChance = InitCritChance +
                     (CritChanceUpgradeCount * MainUpgradeController.Instance.fMainTurretCriticalChance);
        CritDamage = InitCritDamage +
                     (CritDamageUpgradeCount * MainUpgradeController.Instance.fMainTurretCricticalDamage);
        BounceChance = InitBounceChance +
                       (BounceChanceUpgradeCount * MainUpgradeController.Instance.fMainTurretBounceChance);

        ObjectReferenceHolder.Instance.rangeLineMainTurret.radius = Range;
        ObjectReferenceHolder.Instance.UpateRangeLines();

        DisableMaxedUpgradeButtons();
    }

    private void DisableMaxedUpgradeButtons()
    {
        if (ProjectileDamageUpgradeCount == UpgradeCountController.Instance.MainTurretProjectileDamageUpgradeCount)
            MainUpgradeController.Instance.bMainTurretDamage.interactable = false;
        if (ProjectileThrustUpgradeCount == UpgradeCountController.Instance.MainTurretProjectileThrustUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBulletSpeed.interactable = false;
        if (FireRateUpgradeCount == UpgradeCountController.Instance.MainTurretFireRateUpgradeCount)
            MainUpgradeController.Instance.bMainTurretFireRate.interactable = false;
        if (TurnSpeedUpgradeCount == UpgradeCountController.Instance.MainTurretTurnSpeedUpgradeCount)
            MainUpgradeController.Instance.bMainTurretTurnSpeed.interactable = false;
        if (RangeUpgradeCount == UpgradeCountController.Instance.MainTurretRangeUpgradeCount)
            MainUpgradeController.Instance.bMainTurretRange.interactable = false;
        if (RegenUpgradeCount == UpgradeCountController.Instance.MainTurretRegenUpgradeCount)
            MainUpgradeController.Instance.bMainTurretRegen.interactable = false;
        if (LifestealUpgradeCount == UpgradeCountController.Instance.MainTurretLifestealUpgradeCount)
            MainUpgradeController.Instance.bMainTurretLifeSteal.interactable = false;
        if (DefenseUpgradeCount == UpgradeCountController.Instance.MainTurretDefenseUpgradeCount)
            MainUpgradeController.Instance.bMainTurretDefense.interactable = false;
        if (BlockChanceUpgradeCount == UpgradeCountController.Instance.MainTurretBlockChanceUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBlockChance.interactable = false;
        if (CritChanceUpgradeCount == UpgradeCountController.Instance.MainTurretCritChanceUpgradeCount)
            MainUpgradeController.Instance.bMainTurretCriticalChance.interactable = false;
        if (CritDamageUpgradeCount == UpgradeCountController.Instance.MainTurretCritDamageUpgradeCount)
            MainUpgradeController.Instance.bMainTurretCricticalDamage.interactable = false;
        if (BounceChanceUpgradeCount == UpgradeCountController.Instance.MainTurretBounceChanceUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBounceChance.interactable = false;
        if (BounceAmountUpgradeCount == UpgradeCountController.Instance.MainTurretBounceAmountUpgradeCount)
            MainUpgradeController.Instance.bMainTurretBounceAmount.interactable = false;
    }
}