using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUpgradeController : MonoBehaviour
{
    public static MainUpgradeController Instance;

    [BoxGroup("Upgrade Variables")]
    public float fMainTurretDamage          = .1f,
                 fMainTurretFireRate        = .1f,
                 fMainTurretBulletSpeed     = .1f,
                 fMainTurretTurnSpeed       = .1f,
                 fMainTurretRange           = .1f,
                 fMainTurretRegen           = .1f,
                 fMainTurretLifeSteal       = .1f,
                 fMainTurretDefense         = .1f,
                 fMainTurretBlockChance     = .1f,
                 fMainTurretCriticalChance  = .1f,
                 fMainTurretCricticalDamage = .1f,
                 fMainTurretBounceChance    = .1f,
                 fMainTurretBounceAmount    = .1f;

    [BoxGroup("Upgrade Buttons")]
    public Button bMainTurretDamage,
                  bMainTurretFireRate,
                  bMainTurretBulletSpeed,
                  bMainTurretTurnSpeed,
                  bMainTurretRange,
                  bMainTurretRegen,
                  bMainTurretLifeSteal,
                  bMainTurretDefense,
                  bMainTurretBlockChance,
                  bMainTurretCriticalChance,
                  bMainTurretCricticalDamage,
                  bMainTurretBounceChance,
                  bMainTurretBounceAmount;

    [BoxGroup("Upgrade Stat Text")]
    public TextMeshProUGUI tmpMainTurretDamageDisplay,
                           tmpMainTurretFireRateDisplay,
                           tmpMainTurretBulletSpeedDisplay,
                           tmpMainTurretTurnSpeedDisplay,
                           tmpMainTurretRangeDisplay,
                           tmpMainTurretRegenDisplay,
                           tmpMainTurretLifeStealDisplay,
                           tmpMainTurretDefenseDisplay,
                           tmpMainTurretBlockChanceDisplay,
                           tmpMainTurretCriticalChanceDisplay,
                           tmpMainTurretCricticalDamageDisplay,
                           tmpMainTurretBounceChanceDisplay,
                           tmpMainTurretBounceAmountDisplay;

    [BoxGroup("Upgrade Price Text")]
    public TextMeshProUGUI tmpMainTurretDamagePriceDisplay,
                           tmpMainTurretFireRatePriceDisplay,
                           tmpMainTurretBulletSpeedPriceDisplay,
                           tmpMainTurretTurnSpeedPriceDisplay,
                           tmpMainTurretRangePriceDisplay,
                           tmpMainTurretRegenPriceDisplay,
                           tmpMainTurretLifeStealPriceDisplay,
                           tmpMainTurretDefensePriceDisplay,
                           tmpMainTurretBlockChancePriceDisplay,
                           tmpMainTurretCriticalChancePriceDisplay,
                           tmpMainTurretCricticalDamagePriceDisplay,
                           tmpMainTurretBounceChancePriceDisplay,
                           tmpMainTurretBounceAmountPriceDisplay;

    [BoxGroup("Upgrade Counters")]
    public TextMeshProUGUI tmp_uc_mainTurretDamage,
                           tmp_uc_mainTurretFireRate,
                           tmp_uc_mainTurretRange,
                           tmp_uc_mainTurretRegen,
                           tmp_uc_mainTurretLifesteal,
                           tmp_uc_mainTurretDefense,
                           tmp_uc_mainTurretBlockChance,
                           tmp_uc_mainTurretCriticalChance,
                           tmp_uc_mainTurretCriticalDamage,
                           tmp_uc_mainTurretBounceChance;

    private int   upgradeCost;
    private float roundedUpgradeValue;

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
        //DMG
        roundedUpgradeValue = Mathf.Round(MainTurretController.ProjectileDamage * 10.0f) / 10.0f;

        tmpMainTurretDamageDisplay.text =
            $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.ProjectileDamagePrice,
                                                     MainTurretController.ProjectileDamageUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretDamagePriceMultiplier);

        tmpMainTurretDamagePriceDisplay.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretDamage,
                                                      MainTurretController.ProjectileDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretProjectileDamageUpgradeCount);

        //FR
        roundedUpgradeValue = Mathf.Round(MainTurretController.FireRate * 10.0f) / 10.0f;

        tmpMainTurretFireRateDisplay.text =
            $"FIRE RATE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.FireRatePrice,
                                                     MainTurretController.FireRateUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretFireRatePriceMultiplier);

        tmpMainTurretFireRatePriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretFireRate,
                                                      MainTurretController.FireRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretFireRateUpgradeCount);

        //PS
        roundedUpgradeValue = Mathf.Round(MainTurretController.ProjectileThrust * 10.0f) / 10.0f;

        tmpMainTurretBulletSpeedDisplay.text =
            $"P. SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.ProjectileThrustPrice,
                                                     MainTurretController.ProjectileThrustUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretProjectileSpeedPriceMultiplier);

        tmpMainTurretBulletSpeedPriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        //TS
        roundedUpgradeValue = Mathf.Round(MainTurretController.TurnSpeed * 10.0f) / 10.0f;

        tmpMainTurretTurnSpeedDisplay.text =
            $"TURN SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.TurnSpeedPrice,
                                                     MainTurretController.TurnSpeedUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretTurnSpeedPriceMultiplier);

        tmpMainTurretTurnSpeedPriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        //R
        roundedUpgradeValue = Mathf.Round(MainTurretController.Range * 10.0f) / 10.0f;

        tmpMainTurretRangeDisplay.text = $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.RangePrice,
                                                     MainTurretController.RangeUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretRangePriceMultiplier);

        tmpMainTurretRangePriceDisplay.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretRange,
                                                      MainTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretRangeUpgradeCount);

        //REGEN
        roundedUpgradeValue = Mathf.Round(MainTurretController.Regen * 10.0f) / 10.0f;

        tmpMainTurretRegenDisplay.text =
            $"REGEN: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}/s</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.RegenPrice,
                                                     MainTurretController.RegenUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretRegenPriceMultiplier);

        tmpMainTurretRegenPriceDisplay.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretRegen,
                                                      MainTurretController.RegenUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretRegenUpgradeCount);

        //lS
        roundedUpgradeValue = Mathf.Round(MainTurretController.Lifesteal * 10.0f) / 10.0f;

        tmpMainTurretLifeStealDisplay.text =
            $"LIFESTEAL: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.LifestealPrice,
                                                     MainTurretController.LifestealUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretLifeStealPriceMultiplier);

        tmpMainTurretLifeStealPriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretLifesteal,
                                                      MainTurretController.LifestealUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretLifestealUpgradeCount);

        //DEF
        roundedUpgradeValue = Mathf.Round(MainTurretController.Defense * 10.0f) / 10.0f;

        tmpMainTurretDefenseDisplay.text =
            $"DEFENSE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.DefensePrice,
                                                     MainTurretController.DefenseUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretDefensePriceMultiplier);

        tmpMainTurretDefensePriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretDefense,
                                                      MainTurretController.DefenseUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretDefenseUpgradeCount);

        //BCH
        roundedUpgradeValue = Mathf.Round(MainTurretController.BlockChance * 10.0f) / 10.0f;

        tmpMainTurretBlockChanceDisplay.text =
            $"BLOCK CH: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.BlockChancePrice,
                                                     MainTurretController.BlockChanceUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretBlockChancePriceMultiplier);

        tmpMainTurretBlockChancePriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretBlockChance,
                                                      MainTurretController.BlockChanceUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretBlockChanceUpgradeCount);

        //CCH
        roundedUpgradeValue = Mathf.Round(MainTurretController.CritChance * 10.0f) / 10.0f;

        tmpMainTurretCriticalChanceDisplay.text =
            $"CRIT CH: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.CritChancePrice,
                                                     MainTurretController.CritChanceUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretCritChancePriceMultiplier);

        tmpMainTurretCriticalChancePriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretCriticalChance,
                                                      MainTurretController.CritChanceUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretCritChanceUpgradeCount);

        //CDMG
        roundedUpgradeValue = Mathf.Round(MainTurretController.CritDamage * 10.0f) / 10.0f;

        tmpMainTurretCricticalDamageDisplay.text =
            $"CRIT DMG: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue * 100}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.CritDamagePrice,
                                                     MainTurretController.CritDamageUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretCritDamagePriceMultiplier);

        tmpMainTurretCricticalDamagePriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretCriticalDamage,
                                                      MainTurretController.CritDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretCritDamageUpgradeCount);

        //BCH
        roundedUpgradeValue = Mathf.Round(MainTurretController.BounceChance * 10.0f) / 10.0f;

        tmpMainTurretBounceChanceDisplay.text =
            $"BOUNCE CH: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.BounceChancePrice,
                                                     MainTurretController.BounceChanceUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretBounceChancePriceMultiplier);

        tmpMainTurretBounceChancePriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretBounceChance,
                                                      MainTurretController.BounceChanceUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretBounceChanceUpgradeCount);

        //BA
        roundedUpgradeValue = Mathf.Round(MainTurretController.BounceAmount * 10.0f) / 10.0f;

        tmpMainTurretBounceAmountDisplay.text =
            $"BOUNCE A: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.BounceAmountPrice,
                                                     MainTurretController.BounceAmountUpgradeCount,
                                                     PriceController.UpgradePriceMultiplier);

        tmpMainTurretBounceAmountPriceDisplay.text =
            $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
    }

    public void ChangeTextForCompletedUpgrades()
    {
        if (bMainTurretDamage.interactable == false) tmpMainTurretDamagePriceDisplay.text = "MAXED";

        if (bMainTurretFireRate.interactable == false) tmpMainTurretFireRatePriceDisplay.text = "MAXED";

        if (bMainTurretBulletSpeed.interactable == false) tmpMainTurretBulletSpeedPriceDisplay.text = "MAXED";

        if (bMainTurretTurnSpeed.interactable == false) tmpMainTurretTurnSpeedPriceDisplay.text = "MAXED";

        if (bMainTurretRange.interactable == false) tmpMainTurretRangePriceDisplay.text = "MAXED";

        if (bMainTurretRegen.interactable == false) tmpMainTurretRegenPriceDisplay.text = "MAXED";

        if (bMainTurretLifeSteal.interactable == false) tmpMainTurretLifeStealPriceDisplay.text = "MAXED";

        if (bMainTurretDefense.interactable == false) tmpMainTurretDefensePriceDisplay.text = "MAXED";

        if (bMainTurretBlockChance.interactable == false) tmpMainTurretBlockChancePriceDisplay.text = "MAXED";

        if (bMainTurretCriticalChance.interactable == false) tmpMainTurretCriticalChancePriceDisplay.text = "MAXED";

        if (bMainTurretCricticalDamage.interactable == false) tmpMainTurretCricticalDamagePriceDisplay.text = "MAXED";

        if (bMainTurretBounceChance.interactable == false) tmpMainTurretBounceChancePriceDisplay.text = "MAXED";

        if (bMainTurretBounceAmount.interactable == false) tmpMainTurretBounceAmountPriceDisplay.text = "MAXED";
    }

    #region Upgradeables

    public void UpgradeMainTurretDamage()
    {
        if (bMainTurretDamage.interactable == false) return;

        MainTurretController.IncreaseProjectileDamage(fMainTurretDamage);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.ProjectileDamage * 10.0f) / 10.0f;

        roundedUpgradeValue = CalculateValue(MainTurretController.InitProjectileDamage,
                                             MainTurretController.ProjectileDamageUpgradeCount);

        tmpMainTurretDamageDisplay.text =
            $"DAMAGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.ProjectileDamagePrice,
                                                     MainTurretController.ProjectileDamageUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretDamagePriceMultiplier);

        if (bMainTurretDamage.interactable == false)
            tmpMainTurretDamagePriceDisplay.text = "MAXED";
        else
            tmpMainTurretDamagePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretDamage,
                                                      MainTurretController.ProjectileDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretProjectileDamageUpgradeCount);
    }

    public void UpgradeMainTurretFireRate()
    {
        if (bMainTurretFireRate.interactable == false) return;

        MainTurretController.IncreaseFireRate(fMainTurretFireRate, fMainTurretBulletSpeed, fMainTurretTurnSpeed);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = CalculateValue(MainTurretController.InitFireRate,
                                             MainTurretController.FireRateUpgradeCount, fMainTurretFireRate);

        tmpMainTurretFireRateDisplay.text =
            $"FIRE RATE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.FireRatePrice,
                                                     MainTurretController.FireRateUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretFireRatePriceMultiplier);

        if (bMainTurretFireRate.interactable == false)
            tmpMainTurretFireRatePriceDisplay.text = "MAXED";
        else
            tmpMainTurretFireRatePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretFireRate,
                                                      MainTurretController.FireRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretFireRateUpgradeCount);
    }

    public void UpgradeMainTurretBulletSpeed()
    {
        if (bMainTurretBulletSpeed.interactable == false) return;

        MainTurretController.IncreaseBulletSpeed(fMainTurretBulletSpeed);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.ProjectileThrust * 10.0f) / 10.0f;

        tmpMainTurretBulletSpeedDisplay.text =
            $"P. SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.ProjectileThrustPrice,
                                                     MainTurretController.ProjectileThrustUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretProjectileSpeedPriceMultiplier);

        if (bMainTurretBulletSpeed.interactable == false)
            tmpMainTurretBulletSpeedPriceDisplay.text = "MAXED";
        else
            tmpMainTurretBulletSpeedPriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
    }

    public void UpgradeMainTurretTurnSpeed()
    {
        if (bMainTurretTurnSpeed.interactable == false) return;

        MainTurretController.IncreaseTurnSpeed(fMainTurretTurnSpeed);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.TurnSpeed * 10.0f) / 10.0f;

        tmpMainTurretTurnSpeedDisplay.text =
            $"TURN SPEED: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.TurnSpeedPrice,
                                                     MainTurretController.TurnSpeedUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretTurnSpeedPriceMultiplier);

        if (bMainTurretTurnSpeed.interactable == false)
            tmpMainTurretTurnSpeedPriceDisplay.text = "MAXED";
        else
            tmpMainTurretTurnSpeedPriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
    }

    public void UpgradeMainTurretRange()
    {
        if (bMainTurretRange.interactable == false) return;

        MainTurretController.IncreaseRange(fMainTurretRange);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.Range * 10.0f) / 10.0f;

        tmpMainTurretRangeDisplay.text = $"RANGE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.RangePrice,
                                                     MainTurretController.RangeUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretRangePriceMultiplier);

        if (bMainTurretRange.interactable == false)
            tmpMainTurretRangePriceDisplay.text = "MAXED";
        else
            tmpMainTurretRangePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretRange,
                                                      MainTurretController.RangeUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretRangeUpgradeCount);
    }

    public void UpgradeMainTurretRegen()
    {
        if (bMainTurretRegen.interactable == false) return;

        MainTurretController.IncreaseRegen(fMainTurretRegen);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.Regen * 10.0f) / 10.0f;

        tmpMainTurretRegenDisplay.text =
            $"REGEN: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}/s</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.RegenPrice,
                                                     MainTurretController.RegenUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretRegenPriceMultiplier);

        if (bMainTurretRegen.interactable == false)
            tmpMainTurretRegenPriceDisplay.text = "MAXED";
        else
            tmpMainTurretRegenPriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretRegen,
                                                      MainTurretController.RegenUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretRegenUpgradeCount);
    }

    public void UpgradeMainTurretLifeSteal()
    {
        if (bMainTurretLifeSteal.interactable == false) return;

        MainTurretController.IncreaseLifeSteal(fMainTurretLifeSteal);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.Lifesteal * 10.0f) / 10.0f;

        tmpMainTurretLifeStealDisplay.text =
            $"LIFESTEAL: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.LifestealPrice,
                                                     MainTurretController.LifestealUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretLifeStealPriceMultiplier);

        if (bMainTurretLifeSteal.interactable == false)
            tmpMainTurretLifeStealPriceDisplay.text = "MAXED";
        else
            tmpMainTurretLifeStealPriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretLifesteal,
                                                      MainTurretController.LifestealUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretLifestealUpgradeCount);
    }

    public void UpgradeMainTurretDefense()
    {
        if (bMainTurretDefense.interactable == false) return;

        MainTurretController.IncreaseDefense(fMainTurretDefense);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.Defense * 10.0f) / 10.0f;

        tmpMainTurretDefenseDisplay.text =
            $"DEFENSE: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.DefensePrice,
                                                     MainTurretController.DefenseUpgradeCount,
                                                     PriceController.Instance.upgradeMainTurretDefensePriceMultiplier);

        if (bMainTurretDefense.interactable == false)
            tmpMainTurretDefensePriceDisplay.text = "MAXED";
        else
            tmpMainTurretDefensePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretDefense,
                                                      MainTurretController.DefenseUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretDefenseUpgradeCount);
    }

    public void UpgradeMainTurretBlockChance()
    {
        if (bMainTurretBlockChance.interactable == false) return;

        MainTurretController.IncreaseBlockChance(fMainTurretBlockChance);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.BlockChance * 10.0f) / 10.0f;

        tmpMainTurretBlockChanceDisplay.text =
            $"BLOCK CH: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.BlockChancePrice,
                                                     MainTurretController.BlockChanceUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretBlockChancePriceMultiplier);

        if (bMainTurretBlockChance.interactable == false)
            tmpMainTurretBlockChancePriceDisplay.text = "MAXED";
        else
            tmpMainTurretBlockChancePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretBlockChance,
                                                      MainTurretController.BlockChanceUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretBlockChanceUpgradeCount);
    }

    public void UpgradeMainTurretCriticalChance()
    {
        if (bMainTurretCriticalChance.interactable == false) return;

        MainTurretController.IncreaseCriticalChance(fMainTurretCriticalChance);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.CritChance * 10.0f) / 10.0f;

        tmpMainTurretCriticalChanceDisplay.text =
            $"CRIT CH: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.CritChancePrice,
                                                     MainTurretController.CritChanceUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretCritChancePriceMultiplier);

        if (bMainTurretCriticalChance.interactable == false)
            tmpMainTurretCriticalChancePriceDisplay.text = "MAXED";
        else
            tmpMainTurretCriticalChancePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretCriticalChance,
                                                      MainTurretController.CritChanceUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretCritChanceUpgradeCount);
    }

    public void UpgradeMainTurretCriticalDamage()
    {
        if (bMainTurretCricticalDamage.interactable == false) return;

        MainTurretController.IncreaseCriticalDamage(fMainTurretCricticalDamage);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.CritDamage * 10.0f) / 10.0f;

        tmpMainTurretCricticalDamageDisplay.text =
            $"CRIT DMG: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue * 100}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.CritDamagePrice,
                                                     MainTurretController.CritDamageUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretCritDamagePriceMultiplier);
        if (bMainTurretCricticalDamage.interactable == false)
            tmpMainTurretCricticalDamagePriceDisplay.text = "MAXED";
        else
            tmpMainTurretCricticalDamagePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretCriticalDamage,
                                                      MainTurretController.CritDamageUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretCritDamageUpgradeCount);
    }

    public void UpgradeMainTurretBounceChance()
    {
        if (bMainTurretBounceChance.interactable == false) return;

        MainTurretController.IncreaseBounceChance(fMainTurretBounceChance);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedUpgradeValue = Mathf.Round(MainTurretController.BounceChance * 10.0f) / 10.0f;

        tmpMainTurretBounceChanceDisplay.text =
            $"BOUNCE CH: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.BounceChancePrice,
                                                     MainTurretController.BounceChanceUpgradeCount,
                                                     PriceController
                                                         .Instance.upgradeMainTurretBounceChancePriceMultiplier);

        if (bMainTurretBounceChance.interactable == false)
            tmpMainTurretBounceChancePriceDisplay.text = "MAXED";
        else
            tmpMainTurretBounceChancePriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";

        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_mainTurretBounceChance,
                                                      MainTurretController.BounceChanceUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.MainTurretBounceChanceUpgradeCount);
    }

    public void UpgradeMainTurretBounceAmount()
    {
        if (bMainTurretBounceAmount.interactable == false) return;

        MainTurretController.IncreaseBounceAmount(fMainTurretBounceAmount);
        UIUpdater.Instance.UpdateResourceTexts();

        tmpMainTurretBounceAmountDisplay.text =
            $"BOUNCE A: <b><color={ColorCodes.COLOR_VALUE}>{roundedUpgradeValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(MainTurretController.BounceAmountPrice,
                                                     MainTurretController.BounceAmountUpgradeCount,
                                                     PriceController.UpgradePriceMultiplier);

        if (bMainTurretBounceAmount.interactable == false)
            tmpMainTurretBounceAmountPriceDisplay.text = "MAXED";
        else
            tmpMainTurretBounceAmountPriceDisplay.text =
                $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
    }

    #endregion

    public void UpdateButtonsAlpha()
    {
        ButtonColorChanger.UpateButtonAlpha(bMainTurretDamage, ResourcesController.EnergyPoints,
                                            MainTurretController.ProjectileDamagePrice,
                                            MainTurretController.ProjectileDamageUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretDamagePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretFireRate, ResourcesController.EnergyPoints,
                                            MainTurretController.FireRatePrice,
                                            MainTurretController.FireRateUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretFireRatePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretBulletSpeed, ResourcesController.EnergyPoints,
                                            MainTurretController.ProjectileThrustPrice,
                                            MainTurretController.ProjectileThrustUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretProjectileSpeedPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretTurnSpeed, ResourcesController.EnergyPoints,
                                            MainTurretController.TurnSpeedPrice,
                                            MainTurretController.TurnSpeedUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretTurnSpeedPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretRange, ResourcesController.EnergyPoints,
                                            MainTurretController.RangePrice,
                                            MainTurretController.RangeUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretRangePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretRegen, ResourcesController.EnergyPoints,
                                            MainTurretController.RegenPrice,
                                            MainTurretController.RegenUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretRegenPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretLifeSteal, ResourcesController.EnergyPoints,
                                            MainTurretController.LifestealPrice,
                                            MainTurretController.LifestealUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretLifeStealPriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretDefense, ResourcesController.EnergyPoints,
                                            MainTurretController.DefensePrice,
                                            MainTurretController.DefenseUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretDefensePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretBlockChance, ResourcesController.EnergyPoints,
                                            MainTurretController.BlockChancePrice,
                                            MainTurretController.BlockChanceUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretBlockChancePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretCriticalChance, ResourcesController.EnergyPoints,
                                            MainTurretController.CritChancePrice,
                                            MainTurretController.CritChanceUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretCritChancePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretCricticalDamage, ResourcesController.EnergyPoints,
                                            MainTurretController.CritDamagePrice,
                                            MainTurretController.CritDamageUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretCritDamagePriceMultiplier);

        ButtonColorChanger.UpateButtonAlpha(bMainTurretBounceChance, ResourcesController.EnergyPoints,
                                            MainTurretController.BounceChancePrice,
                                            MainTurretController.BounceChanceUpgradeCount,
                                            PriceController.Instance.upgradeMainTurretBounceChancePriceMultiplier);
    }

    private float CalculateValue(float initValue, float upgradeCount, float valueIncreaseFactor)
    {
        return Mathf.Round(initValue + (upgradeCount * valueIncreaseFactor));
    }

    public static float CalculateValue(float initUpgrade, float upgradeCount)
    {
        return Mathf.Round(initUpgrade                                                                     *
                           Mathf.Pow(UpgradeMainTurretStatsController.UpgradeStatMultiplier, upgradeCount) * 10f) / 10f;
    }
}