using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LaserTurretController : TurretController
{
    public static LaserTurretController Instance;

    [BoxGroup("Laser Values")] public float coolDown, activeTime, turnSpeed, range, damagePerSecond;

    [ShowNonSerializedField] public static float CoolDown, ActiveTime, TurnSpeed, Range, DamagePerSecond;

    [BoxGroup("Prices")] [SerializeField]
    private float coolDownPrice,
                  activeTimePrice,
                  projectileDamagePerSecondPrice,
                  turnSpeedPrice,
                  rangePrice;

    [ShowNonSerializedField]
    public static float CoolDownPrice,
                        ActiveTimePrice,
                        ProjectileDamagePerSecondPrice,
                        TurnSpeedPrice,
                        RangePrice;

    [ShowNonSerializedField]
    public static int CoolDownUpgradeCount,
                      ActiveTimeUpgradeCount,
                      ProjectileDamagePerSecondUpgradeCount,
                      TurnSpeedUpgradeCount,
                      RangeUpgradeCount;

    [ShowNonSerializedField]
    public static float InitCoolDown,
                        InitActiveTime,
                        InitProjectileDamagePerSecond,
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
        InitCoolDown                  = coolDown;
        InitActiveTime                = activeTime;
        InitProjectileDamagePerSecond = damagePerSecond;
        InitTurnSpeed                 = turnSpeed;
        InitRange                     = range;

        CoolDown        = coolDown;
        ActiveTime      = activeTime;
        DamagePerSecond = damagePerSecond;
        TurnSpeed       = turnSpeed;
        Range           = range;

        CoolDownPrice                  = coolDownPrice;
        ActiveTimePrice                = activeTimePrice;
        ProjectileDamagePerSecondPrice = projectileDamagePerSecondPrice;
        TurnSpeedPrice                 = turnSpeedPrice;
        RangePrice                     = rangePrice;

        ObjectReferenceHolder.Instance.rangeLineLaserTurret.radius = range;
    }

    public override void Overcharge()
    {
        DamagePerSecond *= SkillsController.OverchargePowerup;
        TurnSpeed       *= SkillsController.OverchargePowerup;

        ObjectReferenceHolder.Instance.rangeLineLaserTurret.radius = Range;
    }

    public static void DecreaseCooldown(float value)
    {
        if (SkillUpgradeController.Instance.bSkillLaserCoolDown.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(CoolDownPrice,
                                                                               CoolDownUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeLaserCoolDownPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(CoolDownPrice,
                                                                                CoolDownUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeLaserCoolDownPriceMultiplier);

            if (CoolDown + ActiveTime - value >= 0)
            {
                CoolDownUpgradeCount++;
                CoolDown = InitCoolDown - (CoolDownUpgradeCount * value);
            }
        }

        if (CoolDownUpgradeCount == UpgradeCountController.Instance.SkillLaserCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserCoolDown.interactable = false;
    }

    public static void IncreaseDuration(float value)
    {
        if (SkillUpgradeController.Instance.bSkillLaserActiveTime.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(ActiveTimePrice,
                                                                               ActiveTimeUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeLaserActiveTimePriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(ActiveTimePrice,
                                                                                ActiveTimeUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeLaserActiveTimePriceMultiplier);

            ActiveTimeUpgradeCount++;
            ActiveTime = InitActiveTime + (ActiveTimeUpgradeCount * value);
        }

        if (ActiveTimeUpgradeCount == UpgradeCountController.Instance.SkillLaserActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserActiveTime.interactable = false;
    }

    public static void IncreaseDamage(float value)
    {
        if (SkillUpgradeController.Instance.bSkillLaserDamage.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(ProjectileDamagePerSecondPrice,
                                                                               ProjectileDamagePerSecondUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeLaserDamagePriceMultiplier))
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(ProjectileDamagePerSecondPrice,
                                                                                ProjectileDamagePerSecondUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeLaserDamagePriceMultiplier);

            ProjectileDamagePerSecondUpgradeCount++;
            DamagePerSecond = InitProjectileDamagePerSecond + (ProjectileDamagePerSecondUpgradeCount * value);
        }

        if (ProjectileDamagePerSecondUpgradeCount == UpgradeCountController.Instance.SkillLaserDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserDamage.interactable = false;
    }

    public static void IncreaseTurnSpeed(float value)
    {
        if (SkillUpgradeController.Instance.bSkillLaserTurnSpeed.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(TurnSpeedPrice,
                                                                               TurnSpeedUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeLaserTurnSpeedPriceMultiplier)
        )
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(TurnSpeedPrice,
                                                                                TurnSpeedUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeLaserTurnSpeedPriceMultiplier);

            TurnSpeedUpgradeCount++;
            TurnSpeed = InitTurnSpeed + (TurnSpeedUpgradeCount * value);
        }

        if (TurnSpeedUpgradeCount == UpgradeCountController.Instance.SkillLaserTurnSpeedUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserTurnSpeed.interactable = false;
    }

    public static void IncreaseRange(float value)
    {
        if (SkillUpgradeController.Instance.bSkillLaserRange.interactable == false) return;

        if (ResourcesController.SciencePoints >= PriceController.CalculatePrice(RangePrice,
                                                                               RangeUpgradeCount,
                                                                               PriceController
                                                                                   .Instance
                                                                                   .upgradeLaserRangePriceMultiplier))
        {
            ResourcesController.SciencePoints -= PriceController.CalculatePrice(RangePrice,
                                                                                RangeUpgradeCount,
                                                                                PriceController
                                                                                    .Instance
                                                                                    .upgradeLaserRangePriceMultiplier);

            RangeUpgradeCount++;
            Range = InitRange + (RangeUpgradeCount * value);

            ObjectReferenceHolder.Instance.rangeLineLaserTurret.radius = Range;
            ObjectReferenceHolder.Instance.rangeLineLaserTurret.CreatePoints();
        }

        if (RangeUpgradeCount == UpgradeCountController.Instance.SkillLaserRangeUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserRange.interactable = false;
    }

    public void LoadStats()
    {
        CoolDown   = InitCoolDown   - (CoolDownUpgradeCount   * SkillUpgradeController.Instance.fRaygunCooldown);
        ActiveTime = InitActiveTime + (ActiveTimeUpgradeCount * SkillUpgradeController.Instance.fRaygunDuration);

        DamagePerSecond = InitProjectileDamagePerSecond +
                          (ProjectileDamagePerSecondUpgradeCount * SkillUpgradeController.Instance.fRaygunDamage);

        TurnSpeed = InitTurnSpeed + (TurnSpeedUpgradeCount * SkillUpgradeController.Instance.fRaygunTurnSpeed);
        Range     = InitRange     + (RangeUpgradeCount     * SkillUpgradeController.Instance.fRaygunRange);

        ObjectReferenceHolder.Instance.rangeLineLaserTurret.radius = Range;

        DisableMaxedUpgradeButtons();
        ObjectReferenceHolder.Instance.UpateRangeLines();
    }

    private void DisableMaxedUpgradeButtons()
    {
        if (CoolDownUpgradeCount == UpgradeCountController.Instance.SkillLaserCoolDownUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserCoolDown.interactable = false;
        if (ActiveTimeUpgradeCount == UpgradeCountController.Instance.SkillLaserActiveTimeUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserActiveTime.interactable = false;
        if (ProjectileDamagePerSecondUpgradeCount == UpgradeCountController.Instance.SkillLaserDamageUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserDamage.interactable = false;
        if (TurnSpeedUpgradeCount == UpgradeCountController.Instance.SkillLaserTurnSpeedUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserTurnSpeed.interactable = false;
        if (RangeUpgradeCount == UpgradeCountController.Instance.SkillLaserRangeUpgradeCount)
            SkillUpgradeController.Instance.bSkillLaserRange.interactable = false;
    }
}