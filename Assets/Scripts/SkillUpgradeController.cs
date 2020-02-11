using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeController : MonoBehaviour
{
    public static SkillUpgradeController Instance;

    [BoxGroup("Overcharge")]
    public float fOverchargeCooldown = .1f,
                 fOverchargeDuration = .1f,
                 fOverchargePowerup  = .1f;

    [BoxGroup("EMP Burst")]
    public float fEMPCooldown     = .1f,
                 fEMPStunDuration = .1f;

    [BoxGroup("Inception Cannons")]
    public float fCannonCooldown    = .1f,
                 fCannonDuration    = .1f,
                 fCannonDamage      = .1f,
                 fCannonBulletSpeed = .1f,
                 fCannonFireRate    = .1f,
                 fCannonTurnSpeed   = .1f,
                 fCannonRange       = .1f;

    [BoxGroup("Ronnie Raygun")]
    public float fRaygunCooldown  = .1f,
                 fRaygunDuration  = .1f,
                 fRaygunDamage    = .1f,
                 fRaygunTurnSpeed = .1f,
                 fRaygunRange     = .1f;

    [BoxGroup("Lucky Mortar")]
    public float fMortarCooldown = .1f,
                 fMortarDuration = .1f,
                 fMortarDamage   = .1f,
                 fMortarFireRate = .1f,
                 fMortarRange    = .1f;

    [BoxGroup("Plan B")]
    public float fEnergyWaveCooldown = .1f,
                 fEnergyWaveDamage   = .1f;

    [BoxGroup("Upgrade Buttons - Overcharge")]
    public Button bSkillOverchargeCoolDown,
                  bSkillOverchargeActiveTime,
                  bSkilloverchargePowerup;

    [BoxGroup("Upgrade Buttons - EMP")]
    public Button bSkillEMPCoolDown,
                  bSkillEMPStunDuration;

    [BoxGroup("Upgrade Buttons - Secondary Turrets")]
    public Button bSkillSecondaryTurretsCoolDown,
                  bSkillSecondaryTurretsActiveTime,
                  bSkillSecondaryTurretsDamage,
                  bSkillSecondaryTurretsProjectileThrust,
                  bSkillSecondaryTurretsFireRate,
                  bSkillSecondaryTurretsTurnSpeed,
                  bSkillSecondaryTurretsRange;

    [BoxGroup("Upgrade Buttons - Laser")]
    public Button bSkillLaserCoolDown,
                  bSkillLaserActiveTime,
                  bSkillLaserDamage,
                  bSkillLaserTurnSpeed,
                  bSkillLaserRange;

    [BoxGroup("Upgrade Buttons - Mortar")]
    public Button bSkillMortarCoolDown,
                  bSkillMortarActiveTime,
                  bSkillMortarDamage,
                  bSkillMortarFireRate,
                  bSkillMortarRange;

    [BoxGroup("Upgrade Buttons - Energy Blast")]
    public Button bSkillEnergyBlastCoolDown,
                  bSkillEnergyBlastDamage;

    [BoxGroup("Upgrade Stat Text - Overcharge")]
    public TextMeshProUGUI tmpSkillOverchargeCoolDown,
                           tmpSkillOverchargeActiveTime,
                           tmpSkillOverchargePowerup;

    [BoxGroup("Upgrade Stat Text - EMP")]
    public TextMeshProUGUI tmpSkillEMPCoolDown,
                           tmpSkillEMPStunDuration;

    [BoxGroup("Upgrade Stat Text - Secondary Turrets")]
    public TextMeshProUGUI tmpSkillSecondaryTurretsCoolDown,
                           tmpSkillSecondaryTurretsActiveTime,
                           tmpSkillSecondaryTurretsDamage,
                           tmpSkillSecondaryTurretsProjectileThrust,
                           tmpSkillSecondaryTurretsFireRate,
                           tmpSkillSecondaryTurretsTurnSpeed,
                           tmpSkillSecondaryTurretsRange;

    [BoxGroup("Upgrade Stat Text - Laser")]
    public TextMeshProUGUI tmpSkillLaserCoolDown,
                           tmpSkillLaserActiveTime,
                           tmpSkillLaserDamage,
                           tmpSkillLaserTurnSpeed,
                           tmpSkillLaserRange;

    [BoxGroup("Upgrade Stat Text - Mortar")]
    public TextMeshProUGUI tmpSkillMortarCoolDown,
                           tmpSkillMortarActiveTime,
                           tmpSkillMortarDamage,
                           tmpSkillMortarFireRate,
                           tmpSkillMortarRange;

    [BoxGroup("Upgrade Stat Text - Energy Blast")]
    public TextMeshProUGUI tmpSkillEnergyBlastCoolDown,
                           tmpSkillEnergyBlastDamage;

    [BoxGroup("Upgrade Price Text - Overcharge")]
    public TextMeshProUGUI tmpSkillOverchargeCoolDownPrice,
                           tmpSkillOverchargeActiveTimePrice,
                           tmpSkillOverchargePowerupPrice;

    [BoxGroup("Upgrade Price Text - EMP")]
    public TextMeshProUGUI tmpSkillEMPCoolDownPrice,
                           tmpSkillEMPStunDurationPrice;

    [BoxGroup("Upgrade Price Text - Secondary Turrets")]
    public TextMeshProUGUI tmpSkillSecondaryTurretsCoolDownPrice,
                           tmpSkillSecondaryTurretsActiveTimePrice,
                           tmpSkillSecondaryTurretsDamagePrice,
                           tmpSkillSecondaryTurretsProjectileThrustPrice,
                           tmpSkillSecondaryTurretsFireRatePrice,
                           tmpSkillSecondaryTurretsTurnSpeedPrice,
                           tmpSkillSecondaryTurretsRangePrice;

    [BoxGroup("Upgrade Price Text - Laser")]
    public TextMeshProUGUI tmpSkillLaserCoolDownPrice,
                           tmpSkillLaserActiveTimePrice,
                           tmpSkillLaserDamagePrice,
                           tmpSkillLaserTurnSpeedPrice,
                           tmpSkillLaserRangePrice;

    [BoxGroup("Upgrade Price Text - Mortar")]
    public TextMeshProUGUI tmpSkillMortarCoolDownPrice,
                           tmpSkillMortarActiveTimePrice,
                           tmpSkillMortarDamagePrice,
                           tmpSkillMortarFireRatePrice,
                           tmpSkillMortarRangePrice;

    [BoxGroup("Upgrade Price Text - Energy Blast")]
    public TextMeshProUGUI tmpSkillEnergyBlastCoolDownPrice,
                           tmpSkillEnergyBlastDamagePrice;

    [BoxGroup("Upgrade Counters")]
    public TextMeshProUGUI tmp_uc_overchargeCooldown,
                           tmp_uc_overchargeActiveTime,
                           tmp_uc_overchargePowerUp,
                           tmp_uc_empCooldown,
                           tmp_uc_empStunDuration,
                           tmp_uc_secondaryTurretCooldown,
                           tmp_uc_secondaryTurretActiveTime,
                           tmp_uc_secondaryTurretDamage,
                           tmp_uc_secondaryTurretFireRate,
                           tmp_uc_secondaryTurretRange,
                           tmp_uc_laserCooldown,
                           tmp_uc_laserActiveTime,
                           tmp_uc_laserDamage,
                           tmp_uc_laserTurnSpeed,
                           tmp_uc_laserRange,
                           tmp_uc_mortarCooldown,
                           tmp_uc_mortarActiveTime,
                           tmp_uc_mortarDamage,
                           tmp_uc_mortarFireRate,
                           tmp_uc_mortarRange,
                           tmp_uc_energyBlastCooldown,
                           tmp_uc_energyBlastDamage;

    private int   upgradeCost;
    private float roundedSkillValue;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetInitialValues();
        ChangeTextForCompletedUpgrades();
        UpdateButtonsAlpha();
    }

    public void SetInitialValues()
    {
        // OV cool
        roundedSkillValue =
            Mathf.Round((SkillsController.OverchargeCoolDown) * 10.0f) / 10.0f;

        tmpSkillOverchargeCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.OverchargeCoolDownPrice,
                                                     SkillsController.OverchargeCoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeOverchargeCooldownPriceMultiplier);

        tmpSkillOverchargeCoolDownPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_overchargeCooldown,
                                                      SkillsController.OverchargeCoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillOverchargeCoolDownUpgradeCount);

        //OV AT
        roundedSkillValue = Mathf.Round(SkillsController.OverchargeActiveTime * 10.0f) / 10.0f;

        tmpSkillOverchargeActiveTime.text =
            $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.OverchargeActiveDurationPrice,
                                                     SkillsController.OverchargeActiveDurationUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeOverchargeActiveTimePriceMultiplier);

        tmpSkillOverchargeActiveTimePrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_overchargeActiveTime,
                                                      SkillsController.OverchargeActiveDurationUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillOverchargeActiveTimeUpgradeCount);

        //OV PW
        roundedSkillValue = Mathf.Round(SkillsController.OverchargePowerup * 1000.0f) / 10.0f;

        tmpSkillOverchargePowerup.text =
            $"POWERUP: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.OverchargePowerupPrice,
                                                     SkillsController.OverchargePowerupUpgradeCount,
                                                     PriceController.Instance.upgradeOverchargePowerupPriceMultiplier);

        tmpSkillOverchargePowerupPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_overchargePowerUp,
                                                      SkillsController.OverchargePowerupUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillOverchargePowerupUpgradeCount);

        //EMP COOL
        roundedSkillValue = Mathf.Round(SkillsController.EmpCoolDown * 10.0f) / 10.0f;

        tmpSkillEMPCoolDown.text = $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EmpCoolDownPrice,
                                                     SkillsController.EmpCoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeEMPCooldownPriceMultiplier);

        tmpSkillEMPCoolDownPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_empCooldown,
                                                      SkillsController.EmpCoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEMPCoolDownUpgradeCount);

        //EMP SD
        roundedSkillValue = Mathf.Round((EnemyController.DisabledTimer + SkillsController.EmpStunDuration) * 10.0f) / 10.0f;

        tmpSkillEMPStunDuration.text = $"STUN DUR: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EmpStunDurationPrice,
                                                     SkillsController.EmpStunDurationUpgradeCount,
                                                     PriceController.Instance.upgradeEMPStunDurationPriceMultiplier);

        tmpSkillEMPStunDurationPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_empStunDuration,
                                                      SkillsController.EmpStunDurationUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEMPStunDurationUpgradeCount);

        //ST cool
        roundedSkillValue =
            Mathf.Round((SecondaryTurretController.CoolDown) * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.CoolDownPrice,
                                                     SecondaryTurretController.CoolDownUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsCoolDownPriceMultiplier);

        tmpSkillSecondaryTurretsCoolDownPrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretCooldown,
                                                      SecondaryTurretController.CoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsCoolDownUpgradeCount);

        //ST AT
        roundedSkillValue = Mathf.Round(SecondaryTurretController.ActiveTime * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsActiveTime.text =
            $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.ActiveTimePrice,
                                                     SecondaryTurretController.ActiveTimeUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsActiveTimePriceMultiplier);

        tmpSkillSecondaryTurretsActiveTimePrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretActiveTime,
                                                      SecondaryTurretController.ActiveTimeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsActiveTimeUpgradeCount);

        //ST DMG
        roundedSkillValue = Mathf.Round(SecondaryTurretController.ProjectileDamage * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsDamage.text =
            $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.ProjectileDamagePrice,
                                                     SecondaryTurretController.ProjectileDamageUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsDamagePriceMultiplier);

        tmpSkillSecondaryTurretsDamagePrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretDamage,
                                                      SecondaryTurretController.ProjectileDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsDamageUpgradeCount);

        //ST PT
        roundedSkillValue = Mathf.Round(SecondaryTurretController.ProjectileThrust * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsProjectileThrust.text =
            $"P. SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.ProjectileThrustPrice,
                                                     SecondaryTurretController.ProjectileThrustUpgradeCount,
                                                     PriceController
                                                         .Instance
                                                         .upgradeSecondaryTurretsProjectileSpeedPriceMultiplier);

        tmpSkillSecondaryTurretsProjectileThrustPrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        //ST FR
        roundedSkillValue = Mathf.Round(SecondaryTurretController.FireRate * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsFireRate.text =
            $"FIRE RATE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.FireRatePrice,
                                                     SecondaryTurretController.FireRateUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsFireRatePriceMultiplier);

        tmpSkillSecondaryTurretsFireRatePrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretFireRate,
                                                      SecondaryTurretController.FireRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsFireRateUpgradeCount);

        //ST TS
        roundedSkillValue = Mathf.Round(SecondaryTurretController.TurnSpeed * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsTurnSpeed.text =
            $"TURN SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.TurnSpeedPrice,
                                                     SecondaryTurretController.TurnSpeedUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsTurnSpeedPriceMultiplier);

        tmpSkillSecondaryTurretsTurnSpeedPrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        //ST R
        roundedSkillValue = Mathf.Round(SecondaryTurretController.Range * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsRange.text =
            $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.RangePrice,
                                                     SecondaryTurretController.RangeUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsRangePriceMultiplier);

        tmpSkillSecondaryTurretsRangePrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretRange,
                                                      SecondaryTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsRangeUpgradeCount);

        //LS COOL
        roundedSkillValue = Mathf.Round((LaserTurretController.CoolDown) * 10.0f) /
                            10.0f;

        tmpSkillLaserCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.CoolDownPrice,
                                                     LaserTurretController.CoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeLaserCoolDownPriceMultiplier);

        tmpSkillLaserCoolDownPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserCooldown,
                                                      LaserTurretController.CoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserCoolDownUpgradeCount);

        //LS AT
        roundedSkillValue = Mathf.Round(LaserTurretController.ActiveTime * 10.0f) / 10.0f;

        tmpSkillLaserActiveTime.text = $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.ActiveTimePrice,
                                                     LaserTurretController.ActiveTimeUpgradeCount,
                                                     PriceController.Instance.upgradeLaserActiveTimePriceMultiplier);

        tmpSkillLaserActiveTimePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserActiveTime,
                                                      LaserTurretController.ActiveTimeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserActiveTimeUpgradeCount);

        //LS DMG
        roundedSkillValue = LaserTurretController.DamagePerSecond;

        tmpSkillLaserDamage.text = $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.ProjectileDamagePerSecondPrice,
                                                     LaserTurretController.ProjectileDamagePerSecondUpgradeCount,
                                                     PriceController.Instance.upgradeLaserDamagePriceMultiplier);

        tmpSkillLaserDamagePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserDamage,
                                                      LaserTurretController.ProjectileDamagePerSecondUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserDamageUpgradeCount);

        //LS TS
        roundedSkillValue = Mathf.Round(LaserTurretController.TurnSpeed * 10.0f) / 10.0f;

        tmpSkillLaserTurnSpeed.text = $"TURN SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.TurnSpeedPrice,
                                                     LaserTurretController.TurnSpeedUpgradeCount,
                                                     PriceController.Instance.upgradeLaserTurnSpeedPriceMultiplier);

        tmpSkillLaserTurnSpeedPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserTurnSpeed,
                                                      LaserTurretController.TurnSpeedUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserTurnSpeedUpgradeCount);

        //LS R
        roundedSkillValue = Mathf.Round(LaserTurretController.Range * 10.0f) / 10.0f;

        tmpSkillLaserRange.text = $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.RangePrice,
                                                     LaserTurretController.RangeUpgradeCount,
                                                     PriceController.Instance.upgradeLaserRangePriceMultiplier);

        tmpSkillLaserRangePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserRange,
                                                      LaserTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserRangeUpgradeCount);

        //M COOl
        roundedSkillValue = Mathf.Round((MortarTurretController.CoolDown) * 10.0f) /
                            10.0f;

        tmpSkillMortarCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.CoolDownPrice,
                                                     MortarTurretController.CoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeMortarCoolDownPriceMultiplier);

        tmpSkillMortarCoolDownPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarCooldown,
                                                      MortarTurretController.CoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarCoolDownUpgradeCount);

        //M AT
        roundedSkillValue = Mathf.Round(MortarTurretController.ActiveTime * 10.0f) / 10.0f;

        tmpSkillMortarActiveTime.text =
            $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.ActiveTimePrice,
                                                     MortarTurretController.ActiveTimeUpgradeCount,
                                                     PriceController.Instance.upgradeMortarActiveTimePriceMultiplier);

        tmpSkillMortarActiveTimePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarActiveTime,
                                                      MortarTurretController.ActiveTimeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarActiveTimeUpgradeCount);

        //M DMG
        roundedSkillValue = Mathf.Round(MortarTurretController.ProjectileDamage * 10.0f) / 10.0f;

        tmpSkillMortarDamage.text = $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.ProjectileDamagePrice,
                                                     MortarTurretController.ProjectileDamageUpgradeCount,
                                                     PriceController.Instance.upgradeMortarDamagePriceMultiplier);

        tmpSkillMortarDamagePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarDamage,
                                                      MortarTurretController.ProjectileDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarDamageUpgradeCount);

        //M FR
        roundedSkillValue = Mathf.Round(MortarTurretController.FireRate * 10.0f) / 10.0f;

        tmpSkillMortarFireRate.text = $"FIRE RATE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.FireRatePrice,
                                                     MortarTurretController.FireRateUpgradeCount,
                                                     PriceController.Instance.upgradeMortarFireRatePriceMultiplier);

        tmpSkillMortarFireRatePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarFireRate,
                                                      MortarTurretController.FireRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarFireRateUpgradeCount);

        //M R
        roundedSkillValue = Mathf.Round(MortarTurretController.Range * 10.0f) / 10.0f;

        tmpSkillMortarRange.text = $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.RangePrice,
                                                     MortarTurretController.RangeUpgradeCount,
                                                     PriceController.Instance.upgradeMortarRangePriceMultiplier);

        tmpSkillMortarRangePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarRange,
                                                      MortarTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarRangeUpgradeCount);

        //E COOL
        roundedSkillValue = Mathf.Round(SkillsController.EnergyCoolDown * 10.0f) / 10.0f;

        tmpSkillEnergyBlastCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EnergyBlastCoolDownPrice,
                                                     SkillsController.EnergyBlastCoolDownUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeEnergyBlastCooldownPriceMultiplier);

        tmpSkillEnergyBlastCoolDownPrice.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyBlastCooldown,
                                                      SkillsController.EnergyBlastCoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEnergyBlastCoolDownUpgradeCount);

        //E DMG
        roundedSkillValue = Mathf.Round(SkillsController.EnergyBlastDamage * 10.0f) / 10.0f;

        tmpSkillEnergyBlastDamage.text = $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EnergyBlastDamagePrice,
                                                     SkillsController.EnergyBlastDamageUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyBlastDamagePriceMultiplier);

        tmpSkillEnergyBlastDamagePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyBlastDamage,
                                                      SkillsController.EnergyBlastDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEnergyBlastDamageUpgradeCount);
    }

    public void ChangeTextForCompletedUpgrades()
    {
        if (bSkillOverchargeCoolDown.interactable == false) tmpSkillOverchargeCoolDownPrice.text = "MAXED";

        if (bSkillOverchargeActiveTime.interactable == false) tmpSkillOverchargeActiveTimePrice.text = "MAXED";

        if (bSkilloverchargePowerup.interactable == false) tmpSkillOverchargePowerupPrice.text = "MAXED";

        if (bSkillEMPCoolDown.interactable == false) tmpSkillEMPCoolDownPrice.text = "MAXED";

        if (bSkillEMPStunDuration.interactable == false) tmpSkillEMPStunDurationPrice.text = "MAXED";

        if (bSkillSecondaryTurretsCoolDown.interactable == false) tmpSkillSecondaryTurretsCoolDownPrice.text = "MAXED";

        if (bSkillSecondaryTurretsActiveTime.interactable == false)
            tmpSkillSecondaryTurretsActiveTimePrice.text = "MAXED";

        if (bSkillSecondaryTurretsDamage.interactable == false) tmpSkillSecondaryTurretsDamagePrice.text = "MAXED";

        if (bSkillSecondaryTurretsProjectileThrust.interactable == false)
            tmpSkillSecondaryTurretsProjectileThrustPrice.text = "MAXED";

        if (bSkillSecondaryTurretsFireRate.interactable == false) tmpSkillSecondaryTurretsFireRatePrice.text = "MAXED";

        if (bSkillSecondaryTurretsTurnSpeed.interactable == false)
            tmpSkillSecondaryTurretsTurnSpeedPrice.text = "MAXED";

        if (bSkillSecondaryTurretsRange.interactable == false) tmpSkillSecondaryTurretsRangePrice.text = "MAXED";

        if (bSkillLaserCoolDown.interactable == false) tmpSkillLaserCoolDownPrice.text = "MAXED";

        if (bSkillLaserActiveTime.interactable == false) tmpSkillLaserActiveTimePrice.text = "MAXED";

        if (bSkillLaserDamage.interactable == false) tmpSkillLaserDamagePrice.text = "MAXED";

        if (bSkillLaserTurnSpeed.interactable == false) tmpSkillLaserTurnSpeedPrice.text = "MAXED";

        if (bSkillLaserRange.interactable == false) tmpSkillLaserRangePrice.text = "MAXED";

        if (bSkillMortarCoolDown.interactable == false) tmpSkillMortarCoolDownPrice.text = "MAXED";

        if (bSkillMortarActiveTime.interactable == false) tmpSkillMortarActiveTimePrice.text = "MAXED";

        if (bSkillMortarDamage.interactable == false) tmpSkillMortarDamagePrice.text = "MAXED";

        if (bSkillMortarFireRate.interactable == false) tmpSkillMortarFireRatePrice.text = "MAXED";

        if (bSkillMortarRange.interactable == false) tmpSkillMortarRangePrice.text = "MAXED";

        if (bSkillEnergyBlastCoolDown.interactable == false) tmpSkillEnergyBlastCoolDownPrice.text = "MAXED";

        if (bSkillEnergyBlastDamage.interactable == false) tmpSkillEnergyBlastDamagePrice.text = "MAXED";
    }

    #region Upgrade Methods

    //Overcharge
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeOverchargeCooldown()
    {
        if (bSkillOverchargeCoolDown.interactable == false) return;

        SkillsController.DecreaseOverchargeCooldown(fOverchargeCooldown);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue =
            Mathf.Round((SkillsController.OverchargeCoolDown) * 10.0f) / 10.0f;

        tmpSkillOverchargeCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.OverchargeCoolDownPrice,
                                                     SkillsController.OverchargeCoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeOverchargeCooldownPriceMultiplier);

        if (bSkillOverchargeCoolDown.interactable == false)
            tmpSkillOverchargeCoolDownPrice.text = "MAXED";
        else
            tmpSkillOverchargeCoolDownPrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_overchargeCooldown,
                                                      SkillsController.OverchargeCoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillOverchargeCoolDownUpgradeCount);
    }

    public void UpgradeOverchargeDuration()
    {
        if (bSkillOverchargeActiveTime.interactable == false) return;

        SkillsController.IncreaseOverchargeDuration(fOverchargeDuration);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SkillsController.OverchargeActiveTime * 10.0f) / 10.0f;

        tmpSkillOverchargeActiveTime.text =
            $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.OverchargeActiveDurationPrice,
                                                     SkillsController.OverchargeActiveDurationUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeOverchargeActiveTimePriceMultiplier);

        if (bSkillOverchargeActiveTime.interactable == false)
            tmpSkillOverchargeActiveTimePrice.text = "MAXED";
        else
            tmpSkillOverchargeActiveTimePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_overchargeActiveTime,
                                                      SkillsController.OverchargeActiveDurationUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillOverchargeActiveTimeUpgradeCount);
    }

    public void UpgradeOverchargePowerup()
    {
        if (bSkilloverchargePowerup.interactable == false) return;

        SkillsController.IncreaseOverchargePowerup(fOverchargePowerup);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SkillsController.OverchargePowerup * 1000.0f) / 10.0f;

        tmpSkillOverchargePowerup.text =
            $"POWERUP: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.OverchargePowerupPrice,
                                                     SkillsController.OverchargePowerupUpgradeCount,
                                                     PriceController.Instance.upgradeOverchargePowerupPriceMultiplier);

        if (bSkilloverchargePowerup.interactable == false)
            tmpSkillOverchargePowerupPrice.text = "MAXED";
        else
            tmpSkillOverchargePowerupPrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_overchargePowerUp,
                                                      SkillsController.OverchargePowerupUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillOverchargePowerupUpgradeCount);
    }

    //EMP Burst
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeEMPCooldown()
    {
        if (bSkillEMPCoolDown.interactable == false) return;

        SkillsController.DecreaseEMPCooldown(fEMPCooldown);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SkillsController.EmpCoolDown * 10.0f) / 10.0f;

        tmpSkillEMPCoolDown.text = $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EmpCoolDownPrice,
                                                     SkillsController.EmpCoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeEMPCooldownPriceMultiplier);

        if (bSkillEMPCoolDown.interactable == false)
            tmpSkillEMPCoolDownPrice.text = "MAXED";
        else
            tmpSkillEMPCoolDownPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_empCooldown,
                                                      SkillsController.EmpCoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEMPCoolDownUpgradeCount);
    }

    public void UpgradeEMPStunDuration()
    {
        if (bSkillEMPStunDuration.interactable == false) return;

        SkillsController.IncreaseEMPStunDuration(fEMPStunDuration);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round((EnemyController.DisabledTimer + SkillsController.EmpStunDuration) * 10.0f) / 10.0f;
//        roundedSkillValue = Mathf.Round(SkillsController.OverchargeActiveTime * 10.0f) / 10.0f;

        tmpSkillEMPStunDuration.text = $"STUN DUR: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EmpStunDurationPrice,
                                                     SkillsController.EmpStunDurationUpgradeCount,
                                                     PriceController.Instance.upgradeEMPStunDurationPriceMultiplier);

        if (bSkillEMPStunDuration.interactable == false)
            tmpSkillEMPStunDurationPrice.text = "MAXED";
        else
            tmpSkillEMPStunDurationPrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_empStunDuration,
                                                      SkillsController.EmpStunDurationUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEMPStunDurationUpgradeCount);
    }

    //Inception Cannons
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeCannonCooldown()
    {
        if (bSkillSecondaryTurretsCoolDown.interactable == false) return;

        SecondaryTurretController.DecreaseCooldown(fCannonCooldown);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue =
            Mathf.Round((SecondaryTurretController.CoolDown) * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.CoolDownPrice,
                                                     SecondaryTurretController.CoolDownUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsCoolDownPriceMultiplier);

        if (bSkillSecondaryTurretsCoolDown.interactable == false)
            tmpSkillSecondaryTurretsCoolDownPrice.text = "MAXED";
        else
            tmpSkillSecondaryTurretsCoolDownPrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretCooldown,
                                                      SecondaryTurretController.CoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsCoolDownUpgradeCount);
    }

    public void UpgradeCannonDuration()
    {
        if (bSkillSecondaryTurretsActiveTime.interactable == false) return;

        SecondaryTurretController.IncreaseActiveDuration(fCannonDuration);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SecondaryTurretController.ActiveTime * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsActiveTime.text =
            $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.ActiveTimePrice,
                                                     SecondaryTurretController.ActiveTimeUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsActiveTimePriceMultiplier);

        if (bSkillSecondaryTurretsActiveTime.interactable == false)
            tmpSkillSecondaryTurretsActiveTimePrice.text = "MAXED";
        else
            tmpSkillSecondaryTurretsActiveTimePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretActiveTime,
                                                      SecondaryTurretController.ActiveTimeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsActiveTimeUpgradeCount);
    }

    public void UpgradeCannonDamage()
    {
        if (bSkillSecondaryTurretsDamage.interactable == false) return;

        SecondaryTurretController.IncreaseDamage(fCannonDamage);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = CalculateCannonDamage(SecondaryTurretController.InitProjectileDamage,
                                                  SecondaryTurretController.ProjectileDamageUpgradeCount);

        tmpSkillSecondaryTurretsDamage.text =
            $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.ProjectileDamagePrice,
                                                     SecondaryTurretController.ProjectileDamageUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsDamagePriceMultiplier);

        if (bSkillSecondaryTurretsDamage.interactable == false)
            tmpSkillSecondaryTurretsDamagePrice.text = "MAXED";
        else
            tmpSkillSecondaryTurretsDamagePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretDamage,
                                                      SecondaryTurretController.ProjectileDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsDamageUpgradeCount);
    }

    public void UpgradeCannonBulletSpeed()
    {
        if (bSkillSecondaryTurretsProjectileThrust.interactable == false) return;

        SecondaryTurretController.IncreaseBulletSpeed(fCannonBulletSpeed);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SecondaryTurretController.ProjectileThrust * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsProjectileThrust.text =
            $"P. SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.ProjectileThrustPrice,
                                                     SecondaryTurretController.ProjectileThrustUpgradeCount,
                                                     PriceController
                                                         .Instance
                                                         .upgradeSecondaryTurretsProjectileSpeedPriceMultiplier);

        if (bSkillSecondaryTurretsProjectileThrust.interactable == false)
            tmpSkillSecondaryTurretsProjectileThrustPrice.text = "MAXED";
        else
            tmpSkillSecondaryTurretsProjectileThrustPrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";
    }

    public void UpgradeCannonFireRate()
    {
        if (bSkillSecondaryTurretsFireRate.interactable == false) return;

        SecondaryTurretController.IncreaseFireRate(fCannonFireRate, fCannonBulletSpeed, fCannonTurnSpeed);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = CalculateCannonFireRate(SecondaryTurretController.InitFireRate,
                                           SecondaryTurretController.FireRateUpgradeCount, fCannonFireRate);

        tmpSkillSecondaryTurretsFireRate.text =
            $"FIRE RATE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.FireRatePrice,
                                                     SecondaryTurretController.FireRateUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsFireRatePriceMultiplier);

        if (bSkillSecondaryTurretsFireRate.interactable == false)
            tmpSkillSecondaryTurretsFireRatePrice.text = "MAXED";
        else
            tmpSkillSecondaryTurretsFireRatePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretFireRate,
                                                      SecondaryTurretController.FireRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsFireRateUpgradeCount);
    }

    public void UpgradeCannonTurnSpeed()
    {
        if (bSkillSecondaryTurretsTurnSpeed.interactable == false) return;

        SecondaryTurretController.IncreaseTurnSpeed(fCannonTurnSpeed);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SecondaryTurretController.TurnSpeed * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsTurnSpeed.text =
            $"TURN SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.TurnSpeedPrice,
                                                     SecondaryTurretController.TurnSpeedUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsTurnSpeedPriceMultiplier);

        if (bSkillSecondaryTurretsTurnSpeed.interactable == false)
            tmpSkillSecondaryTurretsTurnSpeedPrice.text = "MAXED";
        else
            tmpSkillSecondaryTurretsTurnSpeedPrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";
    }

    public void UpgradeCannonRange()
    {
        if (bSkillSecondaryTurretsRange.interactable == false) return;

        SecondaryTurretController.IncreaseRange(fCannonRange);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SecondaryTurretController.Range * 10.0f) / 10.0f;

        tmpSkillSecondaryTurretsRange.text =
            $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SecondaryTurretController.RangePrice,
                                                     SecondaryTurretController.RangeUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeSecondaryTurretsRangePriceMultiplier);

        if (bSkillSecondaryTurretsRange.interactable == false)
            tmpSkillSecondaryTurretsRangePrice.text = "MAXED";
        else
            tmpSkillSecondaryTurretsRangePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_secondaryTurretRange,
                                                      SecondaryTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillSecondaryTurretsRangeUpgradeCount);
    }

    //Ronnie Raygun
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeRaygunCooldown()
    {
        if (bSkillLaserCoolDown.interactable == false) return;

        LaserTurretController.DecreaseCooldown(fRaygunCooldown);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round((LaserTurretController.CoolDown) * 10.0f) /
                            10.0f;

        tmpSkillLaserCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.CoolDownPrice,
                                                     LaserTurretController.CoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeLaserCoolDownPriceMultiplier);

        if (bSkillLaserCoolDown.interactable == false)
            tmpSkillLaserCoolDownPrice.text = "MAXED";
        else
            tmpSkillLaserCoolDownPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserCooldown,
                                                      LaserTurretController.CoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserCoolDownUpgradeCount);
    }

    public void UpgradeRaygunDuration()
    {
        if (bSkillLaserActiveTime.interactable == false) return;

        LaserTurretController.IncreaseDuration(fRaygunDuration);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(LaserTurretController.ActiveTime * 10.0f) / 10.0f;

        tmpSkillLaserActiveTime.text = $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.ActiveTimePrice,
                                                     LaserTurretController.ActiveTimeUpgradeCount,
                                                     PriceController.Instance.upgradeLaserActiveTimePriceMultiplier);

        if (bSkillLaserActiveTime.interactable == false)
            tmpSkillLaserActiveTimePrice.text = "MAXED";
        else
            tmpSkillLaserActiveTimePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserActiveTime,
                                                      LaserTurretController.ActiveTimeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserActiveTimeUpgradeCount);
    }

    public void UpgradeRaygunDamage()
    {
        if (bSkillLaserDamage.interactable == false) return;

        LaserTurretController.IncreaseDamage(fRaygunDamage);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = CalculateValue(LaserTurretController.InitProjectileDamagePerSecond,
                                           LaserTurretController.ProjectileDamagePerSecondUpgradeCount + 1,
                                           fRaygunDamage);

        tmpSkillLaserDamage.text = $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.ProjectileDamagePerSecondPrice,
                                                     LaserTurretController.ProjectileDamagePerSecondUpgradeCount,
                                                     PriceController.Instance.upgradeLaserDamagePriceMultiplier);

        if (bSkillLaserDamage.interactable == false)
            tmpSkillLaserDamagePrice.text = "MAXED";
        else
            tmpSkillLaserDamagePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserDamage,
                                                      LaserTurretController.ProjectileDamagePerSecondUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserDamageUpgradeCount);
    }

    public void UpgradeRaygunTurnSpeed()
    {
        if (bSkillLaserTurnSpeed.interactable == false) return;

        LaserTurretController.IncreaseTurnSpeed(fRaygunTurnSpeed);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = CalculateLaserTurnSpeed(LaserTurretController.InitTurnSpeed,
                                                    LaserTurretController.TurnSpeedUpgradeCount, fRaygunTurnSpeed);

        tmpSkillLaserTurnSpeed.text = $"TURN SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.TurnSpeedPrice,
                                                     LaserTurretController.TurnSpeedUpgradeCount,
                                                     PriceController.Instance.upgradeLaserTurnSpeedPriceMultiplier);

        if (bSkillLaserTurnSpeed.interactable == false)
            tmpSkillLaserTurnSpeedPrice.text = "MAXED";
        else
            tmpSkillLaserTurnSpeedPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserTurnSpeed,
                                                      LaserTurretController.TurnSpeedUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserTurnSpeedUpgradeCount);
    }

    public void UpgradeRaygunRange()
    {
        if (bSkillLaserRange.interactable == false) return;

        LaserTurretController.IncreaseRange(fRaygunRange);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(LaserTurretController.Range * 10.0f) / 10.0f;

        tmpSkillLaserRange.text = $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(LaserTurretController.RangePrice,
                                                     LaserTurretController.RangeUpgradeCount,
                                                     PriceController.Instance.upgradeLaserRangePriceMultiplier);

        if (bSkillLaserRange.interactable == false)
            tmpSkillLaserRangePrice.text = "MAXED";
        else
            tmpSkillLaserRangePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_laserRange,
                                                      LaserTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillLaserRangeUpgradeCount);
    }

    //Lucky Mortar
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeMortarCooldown()
    {
        if (bSkillMortarCoolDown.interactable == false) return;

        MortarTurretController.DecreaseCooldown(fMortarCooldown);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round((MortarTurretController.CoolDown) * 10.0f) /
                            10.0f;

        tmpSkillMortarCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.CoolDownPrice,
                                                     MortarTurretController.CoolDownUpgradeCount,
                                                     PriceController.Instance.upgradeMortarCoolDownPriceMultiplier);

        if (bSkillMortarCoolDown.interactable == false)
            tmpSkillMortarCoolDownPrice.text = "MAXED";
        else
            tmpSkillMortarCoolDownPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarCooldown,
                                                      MortarTurretController.CoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarCoolDownUpgradeCount);
    }

    public void UpgradeMortarDuration()
    {
        if (bSkillMortarActiveTime.interactable == false) return;

        MortarTurretController.IncreaseDuration(fMortarDuration);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(MortarTurretController.ActiveTime * 10.0f) / 10.0f;

        tmpSkillMortarActiveTime.text =
            $"DURATION: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.ActiveTimePrice,
                                                     MortarTurretController.ActiveTimeUpgradeCount,
                                                     PriceController.Instance.upgradeMortarActiveTimePriceMultiplier);

        if (bSkillMortarActiveTime.interactable == false)
            tmpSkillMortarActiveTimePrice.text = "MAXED";
        else
            tmpSkillMortarActiveTimePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarActiveTime,
                                                      MortarTurretController.ActiveTimeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarActiveTimeUpgradeCount);
    }

    public void UpgradeMortarDamage()
    {
        if (bSkillMortarDamage.interactable == false) return;

        MortarTurretController.IncreaseDamage(fMortarDamage);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = CalculateValue(MortarTurretController.InitProjectileDamage,
                                           MortarTurretController.ProjectileDamageUpgradeCount);

        tmpSkillMortarDamage.text = $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.ProjectileDamagePrice,
                                                     MortarTurretController.ProjectileDamageUpgradeCount,
                                                     PriceController.Instance.upgradeMortarDamagePriceMultiplier);
        if (bSkillMortarDamage.interactable == false)
            tmpSkillMortarDamagePrice.text = "MAXED";
        else
            tmpSkillMortarDamagePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarDamage,
                                                      MortarTurretController.ProjectileDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarDamageUpgradeCount);
    }

    public void UpgradeMortarFireRate()
    {
        if (bSkillMortarFireRate.interactable == false) return;

        MortarTurretController.IncreaseFireRate(fMortarFireRate);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = CalculateValue(MortarTurretController.InitFireRate,
                                           MortarTurretController.FireRateUpgradeCount + 1, fMortarFireRate);

        tmpSkillMortarFireRate.text = $"FIRE RATE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.FireRatePrice,
                                                     MortarTurretController.FireRateUpgradeCount,
                                                     PriceController.Instance.upgradeMortarFireRatePriceMultiplier);

        if (bSkillMortarFireRate.interactable == false)
            tmpSkillMortarFireRatePrice.text = "MAXED";
        else
            tmpSkillMortarFireRatePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarFireRate,
                                                      MortarTurretController.FireRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarFireRateUpgradeCount);
    }

    public void UpgradeMortarRange()
    {
        if (bSkillMortarRange.interactable == false) return;

        MortarTurretController.IncreaseRange(fMortarRange);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(MortarTurretController.Range * 10.0f) / 10.0f;

        tmpSkillMortarRange.text = $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MortarTurretController.RangePrice,
                                                     MortarTurretController.RangeUpgradeCount,
                                                     PriceController.Instance.upgradeMortarRangePriceMultiplier);

        if (bSkillMortarRange.interactable == false)
            tmpSkillMortarRangePrice.text = "MAXED";
        else
            tmpSkillMortarRangePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mortarRange,
                                                      MortarTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillMortarRangeUpgradeCount);
    }

    //Plan B
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeEnergyWaveCooldown()
    {
        if (bSkillEnergyBlastCoolDown.interactable == false) return;

        SkillsController.DecreaseEnergyWaveCooldown(fEnergyWaveCooldown);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SkillsController.EnergyCoolDown * 10.0f) / 10.0f;

        tmpSkillEnergyBlastCoolDown.text =
            $"COOLDOWN: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue + 1}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EnergyBlastCoolDownPrice,
                                                     SkillsController.EnergyBlastCoolDownUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeEnergyBlastCooldownPriceMultiplier);

        if (bSkillEnergyBlastCoolDown.interactable == false)
            tmpSkillEnergyBlastCoolDownPrice.text = "MAXED";
        else
            tmpSkillEnergyBlastCoolDownPrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyBlastCooldown,
                                                      SkillsController.EnergyBlastCoolDownUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEnergyBlastCoolDownUpgradeCount);
    }

    public void UpgradeEnergyWaveDamage()
    {
        if (bSkillEnergyBlastDamage.interactable == false) return;

        SkillsController.IncreaseEnergyWaveDamage(fEnergyWaveDamage);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(SkillsController.EnergyBlastDamage * 10.0f) / 10.0f;

        tmpSkillEnergyBlastDamage.text = $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(SkillsController.EnergyBlastDamagePrice,
                                                     SkillsController.EnergyBlastDamageUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyBlastDamagePriceMultiplier);

        if (bSkillEnergyBlastDamage.interactable == false)
            tmpSkillEnergyBlastDamagePrice.text = "MAXED";
        else
            tmpSkillEnergyBlastDamagePrice.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Sp</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyBlastDamage,
                                                      SkillsController.EnergyBlastDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.SkillEnergyBlastDamageUpgradeCount);
    }

    #endregion

    public void UpdateButtonsAlpha()
    {
        ButtonColorChanger.UpateButtonAlpha(bSkillOverchargeCoolDown, ResourcesController.SciencePoints,
                                            SkillsController.OverchargeCoolDownPrice,
                                            SkillsController.OverchargeCoolDownUpgradeCount,
                                            PriceController.Instance.upgradeOverchargeCooldownPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillOverchargeActiveTime, ResourcesController.SciencePoints,
                                            SkillsController.OverchargeActiveDurationPrice,
                                            SkillsController.OverchargeActiveDurationUpgradeCount,
                                            PriceController.Instance.upgradeOverchargeActiveTimePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkilloverchargePowerup, ResourcesController.SciencePoints,
                                            SkillsController.OverchargePowerupPrice,
                                            SkillsController.OverchargePowerupUpgradeCount,
                                            PriceController.Instance.upgradeOverchargePowerupPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillEMPCoolDown, ResourcesController.SciencePoints,
                                            SkillsController.EmpCoolDownPrice,
                                            SkillsController.EmpCoolDownUpgradeCount,
                                            PriceController.Instance.upgradeEMPCooldownPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillEMPStunDuration, ResourcesController.SciencePoints,
                                            SkillsController.EmpStunDurationPrice,
                                            SkillsController.EmpStunDurationUpgradeCount,
                                            PriceController.Instance.upgradeEMPStunDurationPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillSecondaryTurretsCoolDown, ResourcesController.SciencePoints,
                                            SecondaryTurretController.CoolDownPrice,
                                            SecondaryTurretController.CoolDownUpgradeCount,
                                            PriceController.Instance.upgradeSecondaryTurretsCoolDownPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillSecondaryTurretsActiveTime, ResourcesController.SciencePoints,
                                            SecondaryTurretController.ActiveTimePrice,
                                            SecondaryTurretController.ActiveTimeUpgradeCount,
                                            PriceController.Instance.upgradeSecondaryTurretsActiveTimePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillSecondaryTurretsDamage, ResourcesController.SciencePoints,
                                            SecondaryTurretController.ProjectileDamagePrice,
                                            SecondaryTurretController.ProjectileDamageUpgradeCount,
                                            PriceController.Instance.upgradeSecondaryTurretsDamagePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillSecondaryTurretsProjectileThrust, ResourcesController.SciencePoints,
                                            SecondaryTurretController.ProjectileThrustPrice,
                                            SecondaryTurretController.ProjectileThrustUpgradeCount,
                                            PriceController
                                                .Instance.upgradeSecondaryTurretsProjectileSpeedPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillSecondaryTurretsFireRate, ResourcesController.SciencePoints,
                                            SecondaryTurretController.FireRatePrice,
                                            SecondaryTurretController.FireRateUpgradeCount,
                                            PriceController.Instance.upgradeSecondaryTurretsFireRatePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillSecondaryTurretsTurnSpeed, ResourcesController.SciencePoints,
                                            SecondaryTurretController.TurnSpeedPrice,
                                            SecondaryTurretController.TurnSpeedUpgradeCount,
                                            PriceController.Instance.upgradeSecondaryTurretsTurnSpeedPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillSecondaryTurretsRange, ResourcesController.SciencePoints,
                                            SecondaryTurretController.RangePrice,
                                            SecondaryTurretController.RangeUpgradeCount,
                                            PriceController.Instance.upgradeSecondaryTurretsRangePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillLaserCoolDown, ResourcesController.SciencePoints,
                                            LaserTurretController.CoolDownPrice,
                                            LaserTurretController.CoolDownUpgradeCount,
                                            PriceController.Instance.upgradeLaserCoolDownPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillLaserActiveTime, ResourcesController.SciencePoints,
                                            LaserTurretController.ActiveTimePrice,
                                            LaserTurretController.ActiveTimeUpgradeCount,
                                            PriceController.Instance.upgradeLaserActiveTimePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillLaserDamage, ResourcesController.SciencePoints,
                                            LaserTurretController.ProjectileDamagePerSecondPrice,
                                            LaserTurretController.ProjectileDamagePerSecondUpgradeCount,
                                            PriceController.Instance.upgradeLaserDamagePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillLaserTurnSpeed, ResourcesController.SciencePoints,
                                            LaserTurretController.TurnSpeedPrice,
                                            LaserTurretController.TurnSpeedUpgradeCount,
                                            PriceController.Instance.upgradeLaserTurnSpeedPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillLaserRange, ResourcesController.SciencePoints,
                                            LaserTurretController.RangePrice,
                                            LaserTurretController.RangeUpgradeCount,
                                            PriceController.Instance.upgradeLaserRangePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillMortarCoolDown, ResourcesController.SciencePoints,
                                            MortarTurretController.CoolDownPrice,
                                            MortarTurretController.CoolDownUpgradeCount,
                                            PriceController.Instance.upgradeMortarCoolDownPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillMortarActiveTime, ResourcesController.SciencePoints,
                                            MortarTurretController.ActiveTimePrice,
                                            MortarTurretController.ActiveTimeUpgradeCount,
                                            PriceController.Instance.upgradeMortarActiveTimePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillMortarDamage, ResourcesController.SciencePoints,
                                            MortarTurretController.ProjectileDamagePrice,
                                            MortarTurretController.ProjectileDamageUpgradeCount,
                                            PriceController.Instance.upgradeMortarDamagePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillMortarFireRate, ResourcesController.SciencePoints,
                                            MortarTurretController.FireRatePrice,
                                            MortarTurretController.FireRateUpgradeCount,
                                            PriceController.Instance.upgradeMortarFireRatePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillMortarRange, ResourcesController.SciencePoints,
                                            MortarTurretController.RangePrice,
                                            MortarTurretController.RangeUpgradeCount,
                                            PriceController.Instance.upgradeMortarRangePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillEnergyBlastCoolDown, ResourcesController.SciencePoints,
                                            SkillsController.EnergyBlastCoolDownPrice,
                                            SkillsController.EnergyBlastCoolDownUpgradeCount,
                                            PriceController.Instance.upgradeEnergyBlastCooldownPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bSkillEnergyBlastDamage, ResourcesController.SciencePoints,
                                            SkillsController.EnergyBlastDamagePrice,
                                            SkillsController.EnergyBlastDamageUpgradeCount,
                                            PriceController.Instance.upgradeEnergyBlastDamagePriceMultiplier);
    }

    private float CalculateValue(float initValue, int upgradeCount, float valueIncreaseFactor)
    {
        return Mathf.Round(initValue + (upgradeCount * valueIncreaseFactor) * 10f) / 10f;
    }

    public static float CalculateValue(float initUpgrade, float upgradeCount)
    {
        return Mathf.Round(initUpgrade                                                                         *
                           Mathf.Pow(UpgradeMortarStatsController.UpgradeMortarDamageMultiplier, upgradeCount) * 10f) /
               10f;
    }

    private float CalculateLaserTurnSpeed(float initUpgrade, int upgradeCount, float valueIncreaseFactor)
    {
        return initUpgrade + upgradeCount * valueIncreaseFactor;
    }

    private float CalculateCannonDamage(float initUpgrade, int upgradeCount)
    {
        return Mathf.Round(initUpgrade *
                           Mathf.Pow(UpgradeSecondaryTurretsStatsController.UpgradeSecondaryTurretsDamageMultiplier,
                                     upgradeCount) * 10f) /
               10f;
    }
    
    private float CalculateCannonFireRate(float initValue, int upgradeCount, float valueIncreaseFactor)
    {
        return initValue + (upgradeCount * valueIncreaseFactor) ;
    }
}