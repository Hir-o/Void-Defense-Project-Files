using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PriceController : MonoBehaviour
{
    public static PriceController Instance;

    [SerializeField] private float upgradePriceMultiplier = 1.07f;

    [BoxGroup("Overcharge Upgrade Multipliers")]
    public float upgradeOverchargeCooldownPriceMultiplier   = 3.2f,
                 upgradeOverchargeActiveTimePriceMultiplier = 2.15f,
                 upgradeOverchargePowerupPriceMultiplier    = 8f;

    [BoxGroup("EMP Upgrade Multipliers")]
    public float upgradeEMPCooldownPriceMultiplier     = 3.5f,
                 upgradeEMPStunDurationPriceMultiplier = 2.15f;

    [BoxGroup("Energy Blast Upgrade Multipliers")]
    public float upgradeEnergyBlastCooldownPriceMultiplier = 4.8f,
                 upgradeEnergyBlastDamagePriceMultiplier   = 6.5f;

    [BoxGroup("Main Turret Upgrade Multipliers")]
    public float upgradeMainTurretDamagePriceMultiplier          = 1.5f,
                 upgradeMainTurretProjectileSpeedPriceMultiplier = 3f,
                 upgradeMainTurretFireRatePriceMultiplier        = 2.5f,
                 upgradeMainTurretTurnSpeedPriceMultiplier       = 4f,
                 upgradeMainTurretRangePriceMultiplier           = 2.2f,
                 upgradeMainTurretRegenPriceMultiplier           = 2.15f,
                 upgradeMainTurretLifeStealPriceMultiplier       = 1.85f,
                 upgradeMainTurretDefensePriceMultiplier         = 2.2f,
                 upgradeMainTurretBlockChancePriceMultiplier     = 1.95f,
                 upgradeMainTurretCritChancePriceMultiplier      = 2.15f,
                 upgradeMainTurretCritDamagePriceMultiplier      = 2.15f,
                 upgradeMainTurretBounceChancePriceMultiplier    = 2.15f;

    [BoxGroup("Secondary Turret Upgrade Multipliers")]
    public float upgradeSecondaryTurretsCoolDownPriceMultiplier        = 3f,
                 upgradeSecondaryTurretsActiveTimePriceMultiplier      = 2.2f,
                 upgradeSecondaryTurretsDamagePriceMultiplier          = 2.5f,
                 upgradeSecondaryTurretsProjectileSpeedPriceMultiplier = 3f,
                 upgradeSecondaryTurretsFireRatePriceMultiplier        = 3f,
                 upgradeSecondaryTurretsTurnSpeedPriceMultiplier       = 6f,
                 upgradeSecondaryTurretsRangePriceMultiplier           = 4f;

    [BoxGroup("Laser Turret Upgrade Multipliers")]
    public float upgradeLaserCoolDownPriceMultiplier   = 2.3f,
                 upgradeLaserActiveTimePriceMultiplier = 2.2f,
                 upgradeLaserDamagePriceMultiplier     = 2.8f,
                 upgradeLaserTurnSpeedPriceMultiplier  = 6.5f,
                 upgradeLaserRangePriceMultiplier      = 5.6f;

    [BoxGroup("Mortar Turret Upgrade Multipliers")]
    public float upgradeMortarCoolDownPriceMultiplier   = 2.3f,
                 upgradeMortarActiveTimePriceMultiplier = 2.2f,
                 upgradeMortarDamagePriceMultiplier     = 2.8f,
                 upgradeMortarFireRatePriceMultiplier   = 3f,
                 upgradeMortarRangePriceMultiplier      = 4.8f;

    [BoxGroup("Resource Upgrade Multipliers")]
    public float upgradeEnergyBonusPriceMultiplier       = 2.3f,
                 upgradeEnergyEfficiencyPriceMultiplier  = 2f,
                 upgradeEnergyDropRatePriceMultiplier    = 2f,
                 upgradeScienceBonusPriceMultiplier      = 2.3f,
                 upgradeScienceEfficiencyPriceMultiplier = 2f,
                 upgradeScienceDropRatePriceMultiplier   = 2f;

    public static float UpgradePriceMultiplier = 1.07f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UpgradePriceMultiplier = upgradePriceMultiplier;
    }

    public static int CalculatePrice(float upgradePrice, float upgradeCount, float multiplier)
    {
        return Mathf.RoundToInt(upgradePrice *
                                Mathf.Pow(multiplier, upgradeCount));
    }
}