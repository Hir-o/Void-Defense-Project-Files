using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUpgradeController : MonoBehaviour
{
    public static ResourcesUpgradeController Instance;

    [BoxGroup("Upgradeable Stats")] 
    public int fEnergyBonus       = 1,
                fEnergyEfficiency  = 1,
                fEnergyDropRate    = 1,
                fScienceBonus      = 1,
                fScienceEfficiency = 1,
                fScienceDropRate   = 1;

    [BoxGroup("Upgrade Buttons")]
    public Button bEnergyBonus,
                  bEnergyEfficiency,
                  bEnergyDropRate,
                  bScienceBonus,
                  bScienceEfficiency,
                  bScienceDropRate;

    [BoxGroup("Upgrade Stat Text")]
    public TextMeshProUGUI tmpEnergyBonus,
                           tmpEnergyEfficiency,
                           tmpEnergyDropRate,
                           tmpScienceBonus,
                           tmpScienceEfficiency,
                           tmpScienceDropRate;

    [BoxGroup("Upgrade Price Text")]
    public TextMeshProUGUI tmpEnergyBonusPrice,
                           tmpEnergyEfficiencyPrice,
                           tmpEnergyDropRatePrice,
                           tmpScienceBonusPrice,
                           tmpScienceEfficiencyPrice,
                           tmpScienceDropRatePrice;

    [BoxGroup("Upgrade Counters")]
    public TextMeshProUGUI tmp_uc_energyBonus,
                           tmp_uc_energyEfficiency,
                           tmp_uc_energyDropRate,
                           tmp_uc_scienceBonus,
                           tmp_uc_scienceEfficiency;

    private int upgradeCost;

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
        //ENERGY BONUS
        roundedSkillValue = Mathf.Round(ResourcesController.EnergyBonus * 10.0f) / 10.0f;
        
        tmpEnergyBonus.text = $"E. BONUS: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.EnergyBonusPrice,
                                                     ResourcesController.EnergyBonusUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyBonusPriceMultiplier);
        
        tmpEnergyBonusPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyBonus,
                                                      ResourcesController.EnergyBonusUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesEnergyBonusUpgradeCount);

        //E EFF
        roundedSkillValue =
            Mathf.Round((ResourcesController.EnergyAddTimer - ResourcesController.EnergyEfficiency) * 10.0f) / 10.0f;
        
        tmpEnergyEfficiency.text = $"ENERGY EFF: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.EnergyEfficiencyPrice,
                                                     ResourcesController.EnergyEfficiencyUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyEfficiencyPriceMultiplier);
        
        tmpEnergyEfficiencyPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyEfficiency,
                                                      ResourcesController.EnergyEfficiencyUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesEnergyEfficiencyUpgradeCount);

        //E DR
        roundedSkillValue = Mathf.Round(ResourcesController.EnergyDropRate * 10.0f) / 10.0f;
        
        tmpEnergyDropRate.text = $"ENERGY DR: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.EnergyDropRatePrice,
                                                     ResourcesController.EnergyDropRateUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyDropRatePriceMultiplier);
        
        tmpEnergyDropRatePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyDropRate,
                                                      ResourcesController.EnergyDropRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesEnergyDropRateUpgradeCount);

        //SCIENCE BONUS
        roundedSkillValue = Mathf.Round(ResourcesController.ScienceBonus * 10.0f) / 10.0f;
        
        tmpScienceBonus.text = $"S. BONUS: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.ScienceBonusPrice,
                                                     ResourcesController.ScienceBonusUpgradeCount,
                                                     PriceController.Instance.upgradeScienceBonusPriceMultiplier);
        
        tmpScienceBonusPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_scienceBonus,
                                                      ResourcesController.ScienceBonusUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesScienceBonusUpgradeCount);

        //SCIENCE EFF
        roundedSkillValue =
            Mathf.Round((ResourcesController.ScienceAddTimer - ResourcesController.ScienceEfficiency) * 10.0f) / 10.0f;
        
        tmpScienceEfficiency.text = $"SCIENCE EFF: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}s</color></b>";
        
        upgradeCost = PriceController.CalculatePrice(ResourcesController.ScienceEfficiencyPrice,
                                                     ResourcesController.ScienceEfficiencyUpgradeCount,
                                                     PriceController.Instance.upgradeScienceEfficiencyPriceMultiplier);
        
        tmpScienceEfficiencyPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_scienceEfficiency,
                                                      ResourcesController.ScienceEfficiencyUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesScienceEfficiencyUpgradeCount);

        //SCIENCE DR
        roundedSkillValue = Mathf.Round(ResourcesController.ScienceDropRate * 10.0f) / 10.0f;
        
        tmpScienceDropRate.text = $"SCIENCE DR: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.ScienceDropRatePrice,
                                                     ResourcesController.ScienceDropRateUpgradeCount,
                                                     PriceController.Instance.upgradeScienceDropRatePriceMultiplier);
        
        tmpScienceDropRatePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
    }

    public void ChangeTextForCompletedUpgrades()
    {
        if (bEnergyBonus.interactable == false) tmpEnergyBonusPrice.text = "MAXED";

        if (bEnergyEfficiency.interactable == false) tmpEnergyEfficiencyPrice.text = "MAXED";

        if (bEnergyDropRate.interactable == false) tmpEnergyDropRatePrice.text = "MAXED";

        if (bScienceBonus.interactable == false) tmpScienceBonusPrice.text = "MAXED";

        if (bScienceEfficiency.interactable == false) tmpScienceEfficiencyPrice.text = "MAXED";

        if (bScienceDropRate.interactable == false) tmpScienceDropRatePrice.text = "MAXED";
    }

    #region Upgrade methods

    //Energy
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeEnergyBonus()
    {
        if (bEnergyBonus.interactable == false) return;
        
        ResourcesController.IncreaseEnergyBonus(fEnergyBonus);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(ResourcesController.EnergyBonus * 10.0f) / 10.0f;

        tmpEnergyBonus.text = $"E. BONUS: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.EnergyBonusPrice,
                                                     ResourcesController.EnergyBonusUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyBonusPriceMultiplier);

        if (bEnergyBonus.interactable == false)
            tmpEnergyBonusPrice.text = "MAXED";
        else
            tmpEnergyBonusPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyBonus,
                                                      ResourcesController.EnergyBonusUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesEnergyBonusUpgradeCount);
    }

    public void UpgradeEnergyEfficiency()
    {
        if (bEnergyEfficiency.interactable == false) return;
        
        ResourcesController.IncreaseEnergyEfficiency(fEnergyEfficiency);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue =
            Mathf.Round((ResourcesController.EnergyAddTimer - ResourcesController.EnergyEfficiency) * 10.0f) / 10.0f;

        tmpEnergyEfficiency.text = $"ENERGY EFF: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.EnergyEfficiencyPrice,
                                                     ResourcesController.EnergyEfficiencyUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyEfficiencyPriceMultiplier);

        if (bEnergyEfficiency.interactable == false)
            tmpEnergyEfficiencyPrice.text = "MAXED";
        else
            tmpEnergyEfficiencyPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyEfficiency,
                                                      ResourcesController.EnergyEfficiencyUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesEnergyEfficiencyUpgradeCount);
    }

    public void UpgradeEnergyDropRate()
    {
        if (bEnergyDropRate.interactable == false) return;
        
        ResourcesController.IncreaseEnergyDropRate(fEnergyDropRate);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(ResourcesController.EnergyDropRate * 10.0f) / 10.0f;

        tmpEnergyDropRate.text = $"ENERGY DR: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.EnergyDropRatePrice,
                                                     ResourcesController.EnergyDropRateUpgradeCount,
                                                     PriceController.Instance.upgradeEnergyDropRatePriceMultiplier);

        if (bEnergyDropRate.interactable == false)
            tmpEnergyDropRatePrice.text = "MAXED";
        else
            tmpEnergyDropRatePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_energyDropRate,
                                                      ResourcesController.EnergyDropRateUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesEnergyDropRateUpgradeCount);
    }

    //Science
    //////////////////////////////////////////////////////////////////////////
    public void UpgradeScienceBonus()
    {
        if (bScienceBonus.interactable == false) return;
        
        ResourcesController.IncreaseScienceBonus(fScienceBonus);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(ResourcesController.ScienceBonus * 10.0f) / 10.0f;

        tmpScienceBonus.text = $"S. BONUS: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.ScienceBonusPrice,
                                                     ResourcesController.ScienceBonusUpgradeCount,
                                                     PriceController.Instance.upgradeScienceBonusPriceMultiplier);

        if (bScienceBonus.interactable == false)
            tmpScienceBonusPrice.text = "MAXED";
        else
            tmpScienceBonusPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_scienceBonus,
                                                      ResourcesController.ScienceBonusUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesScienceBonusUpgradeCount);
    }

    public void UpgradeScienceEfficiency()
    {
        if (bScienceEfficiency.interactable == false) return;
        
        ResourcesController.IncreaseScienceEfficiency(fScienceEfficiency);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue =
            Mathf.Round((ResourcesController.ScienceAddTimer - ResourcesController.ScienceEfficiency) * 10.0f) / 10.0f;

        tmpScienceEfficiency.text = $"SCIENCE EFF: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}s</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.ScienceEfficiencyPrice,
                                                     ResourcesController.ScienceEfficiencyUpgradeCount,
                                                     PriceController.Instance.upgradeScienceEfficiencyPriceMultiplier);

        if (bScienceEfficiency.interactable == false)
            tmpScienceEfficiencyPrice.text = "MAXED";
        else
            tmpScienceEfficiencyPrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
        
        UpgradeCountDisplay.UpdateUpgradeCountDisplay(tmp_uc_scienceEfficiency,
                                                      ResourcesController.ScienceEfficiencyUpgradeCount,
                                                      UpgradeCountController
                                                          .Instance.ResourcesScienceEfficiencyUpgradeCount);
    }

    public void UpgradeScienceDropRate()
    {
        if (bScienceDropRate.interactable == false) return;
        
        ResourcesController.IncreaseScienceDropRate(fScienceDropRate);
        UIUpdater.Instance.UpdateResourceTexts();

        roundedSkillValue = Mathf.Round(ResourcesController.ScienceDropRate * 10.0f) / 10.0f;

        tmpScienceDropRate.text = $"SCIENCE DR: <b><color={ColorCodes.COLOR_VALUE}>{roundedSkillValue}%</color></b>";

        upgradeCost = PriceController.CalculatePrice(ResourcesController.ScienceDropRatePrice,
                                                     ResourcesController.ScienceDropRateUpgradeCount,
                                                     PriceController.Instance.upgradeScienceDropRatePriceMultiplier);

        if (bScienceDropRate.interactable == false)
            tmpScienceDropRatePrice.text = "MAXED";
        else
            tmpScienceDropRatePrice.text = $"COST: <b><color={ColorCodes.COLOR_PRICE}>{upgradeCost} Ep</color></b>";
    }

    #endregion

    public void UpdateButtonsAlpha()
    {
        ButtonColorChanger.UpateButtonAlpha(bEnergyBonus, ResourcesController.EnergyPoints,
                                            ResourcesController.EnergyBonusPrice,
                                            ResourcesController.EnergyBonusUpgradeCount,
                                            PriceController.Instance.upgradeEnergyBonusPriceMultiplier);
        
        ButtonColorChanger.UpateButtonAlpha(bEnergyEfficiency, ResourcesController.EnergyPoints,
                                            ResourcesController.EnergyEfficiencyPrice,
                                            ResourcesController.EnergyEfficiencyUpgradeCount,
                                            PriceController.Instance.upgradeEnergyEfficiencyPriceMultiplier);
        
        ButtonColorChanger.UpateButtonAlpha(bEnergyDropRate, ResourcesController.EnergyPoints,
                                            ResourcesController.EnergyDropRatePrice,
                                            ResourcesController.EnergyDropRateUpgradeCount,
                                            PriceController.Instance.upgradeEnergyDropRatePriceMultiplier);
        
        ButtonColorChanger.UpateButtonAlpha(bScienceBonus, ResourcesController.EnergyPoints,
                                            ResourcesController.ScienceBonusPrice,
                                            ResourcesController.ScienceBonusUpgradeCount,
                                            PriceController.Instance.upgradeScienceBonusPriceMultiplier);
        
        ButtonColorChanger.UpateButtonAlpha(bScienceEfficiency, ResourcesController.EnergyPoints,
                                            ResourcesController.ScienceEfficiencyPrice,
                                            ResourcesController.ScienceEfficiencyUpgradeCount,
                                            PriceController.Instance.upgradeScienceEfficiencyPriceMultiplier);
        
        ButtonColorChanger.UpateButtonAlpha(bScienceDropRate, ResourcesController.EnergyPoints,
                                            ResourcesController.ScienceDropRatePrice,
                                            ResourcesController.ScienceDropRateUpgradeCount,
                                            PriceController.Instance.upgradeScienceDropRatePriceMultiplier);
    }
}