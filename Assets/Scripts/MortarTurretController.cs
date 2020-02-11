using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MortarTurretController : TurretController
{
    public static MortarTurretController Instance;

    [BoxGroup("Mortar Values")] public float coolDown, activeTime, projectileDamage, fireRate, range, minRange;

    [ShowNonSerializedField] public static float CoolDown, ActiveTime, ProjectileDamage, FireRate, Range, MinRange;

    [BoxGroup("Prices")] [SerializeField]
    private float coolDownPrice,
                  activeTimePrice,
                  projectileDamagePrice,
                  fireRatePrice,
                  rangePrice;

    [ShowNonSerializedField]
    public static float CoolDownPrice,
                        ActiveTimePrice,
                        ProjectileDamagePrice,
                        FireRatePrice,
                        RangePrice;

    [ShowNonSerializedField]
    public static int CoolDownUpgradeCount,
                      ActiveTimeUpgradeCount,
                      ProjectileDamageUpgradeCount,
                      FireRateUpgradeCount,
                      RangeUpgradeCount;

    [ShowNonSerializedField]
    public static float InitCoolDown,
                        InitActiveTime,
                        InitProjectileDamage,
                        InitFireRate,
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
        InitFireRate         = fireRate;
        InitRange            = range;

        CoolDown         = coolDown;
        ActiveTime       = activeTime;
        ProjectileDamage = projectileDamage;
        FireRate         = fireRate;
        Range            = range;
        MinRange         = minRange;

        CoolDownPrice         = coolDownPrice;
        ActiveTimePrice       = activeTimePrice;
        ProjectileDamagePrice = projectileDamagePrice;
        FireRatePrice         = fireRatePrice;
        RangePrice            = rangePrice;

        ObjectReferenceHolder.Instance.rangeLineMortarTurret.radius      = range;
        ObjectReferenceHolder.Instance.rangeLineMortarTurretChild.radius = minRange; // needed for min range
    }

    public override void Overcharge()
    {
        ProjectileDamage *=  SkillsController.OverchargePowerup;
        FireRate         *= SkillsController.OverchargePowerup;

        ObjectReferenceHolder.Instance.rangeLineMortarTurret.radius = Range;
    }

    public static void DecreaseCooldown(float value)
    {
        if (SkillUpgradeController.Instance.bSkillMortarCoolDown.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(CoolDownPrice,
                                                                               CoolDownUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMortarCoolDownPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(CoolDownPrice,
                                                                                CoolDownUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeMortarCoolDownPriceMultiplier);

            if (CoolDown + ActiveTime - value >= 0)
            {
                CoolDownUpgradeCount++;
                CoolDown = InitCoolDown - (CoolDownUpgradeCount * value);
            }
        }

        if (CoolDownUpgradeCount == UpgradeCountController.Instance.SkillMortarCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarCoolDown.interactable = false;
    }

    public static void IncreaseDuration(float value)
    {
        if (SkillUpgradeController.Instance.bSkillMortarActiveTime.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(ActiveTimePrice,
                                                                               ActiveTimeUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMortarActiveTimePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(ActiveTimePrice,
                                                                                ActiveTimeUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeMortarActiveTimePriceMultiplier);

            ActiveTimeUpgradeCount++;
            ActiveTime = InitActiveTime + (ActiveTimeUpgradeCount * value);
        }

        if (ActiveTimeUpgradeCount == UpgradeCountController.Instance.SkillMortarActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarActiveTime.interactable = false;
    }

    public static void IncreaseDamage(float value)
    {
        if (SkillUpgradeController.Instance.bSkillMortarDamage.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(ProjectileDamagePrice,
                                                                               ProjectileDamageUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMortarDamagePriceMultiplier))
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(ProjectileDamagePrice,
                                                                                ProjectileDamageUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeMortarDamagePriceMultiplier);

            ProjectileDamageUpgradeCount++;
            ProjectileDamage = UpgradeMortarStatsController.IncreaseMortarDamageStat(InitProjectileDamage,
                                                                                     ProjectileDamageUpgradeCount);
        }

        if (ProjectileDamageUpgradeCount == UpgradeCountController.Instance.SkillMortarDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarDamage.interactable = false;
    }

    public static void IncreaseFireRate(float value)
    {
        if (SkillUpgradeController.Instance.bSkillMortarFireRate.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(FireRatePrice,
                                                                               FireRateUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMortarFireRatePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(FireRatePrice,
                                                                                FireRateUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeMortarFireRatePriceMultiplier);

            FireRateUpgradeCount++;
            FireRate = InitFireRate + (FireRateUpgradeCount * value);
        }

        if (FireRateUpgradeCount == UpgradeCountController.Instance.SkillMortarFireRateUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarFireRate.interactable = false;
    }

    public static void IncreaseRange(float value)
    {
        if (SkillUpgradeController.Instance.bSkillMortarRange.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(RangePrice,
                                                                               RangeUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeMortarRangePriceMultiplier))
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(RangePrice,
                                                                                RangeUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeMortarRangePriceMultiplier);

            RangeUpgradeCount++;
            Range = InitRange + (RangeUpgradeCount * value);

            ObjectReferenceHolder.Instance.rangeLineMortarTurret.radius = Range;
            ObjectReferenceHolder.Instance.rangeLineMortarTurret.CreatePoints();
        }

        if (RangeUpgradeCount == UpgradeCountController.Instance.SkillMortarRangeUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarRange.interactable = false;
    }

    public void LoadStats()
    {
        CoolDown   = InitCoolDown   - (CoolDownUpgradeCount   * SkillUpgradeController.Instance.fMortarCooldown);
        ActiveTime = InitActiveTime + (ActiveTimeUpgradeCount * SkillUpgradeController.Instance.fMortarDuration);

        ProjectileDamage = UpgradeMortarStatsController.IncreaseMortarDamageStat(InitProjectileDamage,
                                                                                 ProjectileDamageUpgradeCount);

        FireRate = InitFireRate + (FireRateUpgradeCount * SkillUpgradeController.Instance.fMortarFireRate);
        Range    = InitRange    + (RangeUpgradeCount    * SkillUpgradeController.Instance.fMortarRange);

        ObjectReferenceHolder.Instance.rangeLineMortarTurret.radius = Range;

        DisableMaxedUpgradeButtons();
        ObjectReferenceHolder.Instance.UpateRangeLines();
    }

    private void DisableMaxedUpgradeButtons()
    {
        if (CoolDownUpgradeCount == UpgradeCountController.Instance.SkillMortarCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarCoolDown.interactable = false;
        if (ActiveTimeUpgradeCount == UpgradeCountController.Instance.SkillMortarActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarActiveTime.interactable = false;
        if (ProjectileDamageUpgradeCount == UpgradeCountController.Instance.SkillMortarDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarDamage.interactable = false;
        if (FireRateUpgradeCount == UpgradeCountController.Instance.SkillMortarFireRateUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarFireRate.interactable = false;
        if (RangeUpgradeCount == UpgradeCountController.Instance.SkillMortarRangeUpgradeCount)
            SkillUpgradeController.Instance.bSkillMortarRange.interactable = false;
    }
}