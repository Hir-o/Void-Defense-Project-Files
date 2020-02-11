using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using AnimatorParams;
using TMPro;

public class ResourcesController : MonoBehaviour
{
    public static ResourcesController Instance;

    #region non-static variables

    [BoxGroup("Initial Prices")] [SerializeField]
    private float energyBonusPrice,
                  energyEfficiencyPrice,
                  energyDropRatePrice,
                  scienceBonusPrice,
                  scienceEfficiencyPrice,
                  scienceDropRatePrice,
                  energyAddTimerPrice,
                  scienceAddTimerPrice;

    #endregion

    #region static variables

    [ShowNonSerializedField]
    public static float EnergyPoints,
                        SciencePoints,
                        EnergyAddTimer,
                        ScienceAddTimer;

    [ShowNonSerializedField]
    public static int EnergyBonus,
                      EnergyEfficiency,
                      EnergyDropRate,
                      ScienceBonus,
                      ScienceEfficiency,
                      ScienceDropRate;

    [ShowNonSerializedField]
    public static int EnergyBonusUpgradeCount,
                      EnergyEfficiencyUpgradeCount,
                      EnergyDropRateUpgradeCount,
                      ScienceBonusUpgradeCount,
                      ScienceEfficiencyUpgradeCount,
                      ScienceDropRateUpgradeCount;

    [ShowNonSerializedField]
    public static float EnergyBonusPrice,
                        EnergyEfficiencyPrice,
                        EnergyDropRatePrice,
                        ScienceBonusPrice,
                        ScienceEfficiencyPrice,
                        ScienceDropRatePrice;

    private static float InitEnergyBonus,
                         InitEnergyEfficiency,
                         InitEnergyDropRate,
                         InitScienceBonus,
                         InitScienceEfficiency,
                         InitScienceDropRate;

    #endregion

    [BoxGroup("Starting Resources")] public float energyPoints, sciencePoints;

    [BoxGroup("Upgradeable Stats")]
    public float energyBonus, energyEfficiency, energyDropRate, scienceBonus, scienceEfficiency, scienceDropRate;

    private WaveSpawner _waveSpawner;

    [BoxGroup("Energy and Science Add Amounts")] [SerializeField]
    private int energyAddAmount = 10, scienceAddAmount = 5;

    [BoxGroup("Energy and Science Timers")] [SerializeField]
    private float energyAddTimer = 5f, scienceAddTimer = 7f;

    [BoxGroup("Energy & Science")] [SerializeField]
    private GameObject energyTextObj, scienceTextObj;

    [BoxGroup("Energy & Science Period")] [SerializeField]
    private TextMeshPro energyPeriodText, sciencePeriodText;

    private Animator energyTextAnimator, scienceTextAnimator;

    private TextMeshPro energyTMP, scienceTMP;

    private bool addScience;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        EnergyPoints  = energyPoints;
        SciencePoints = sciencePoints;

        EnergyAddTimer  = energyAddTimer;
        ScienceAddTimer = scienceAddTimer;

        energyTextAnimator  = energyTextObj.GetComponent<Animator>();
        scienceTextAnimator = scienceTextObj.GetComponent<Animator>();

        energyTMP  = energyTextObj.GetComponent<TextMeshPro>();
        scienceTMP = scienceTextObj.GetComponent<TextMeshPro>();

        EnergyBonusPrice       = energyBonusPrice;
        EnergyEfficiencyPrice  = energyEfficiencyPrice;
        EnergyDropRatePrice    = energyDropRatePrice;
        ScienceBonusPrice      = scienceBonusPrice;
        ScienceEfficiencyPrice = scienceEfficiencyPrice;
        ScienceDropRatePrice   = scienceDropRatePrice;

        InitEnergyBonus       = energyAddAmount;
        InitScienceBonus      = scienceAddAmount;
        InitEnergyEfficiency  = energyEfficiency;
        InitScienceEfficiency = scienceEfficiency;
        InitEnergyDropRate    = energyDropRate;
        InitScienceDropRate   = scienceDropRate;
    }

    private void Start()
    {
        _waveSpawner = ObjectReferenceHolder.Instance.waveSpawner;
        UIUpdater.Instance.UpdateResourceTexts();

        energyPeriodText.text = (energyAddAmount + energyBonus)                               + " Ep" + " / " +
                                (EnergyAddTimer  - (energyEfficiency / 100) * EnergyAddTimer) + "sec";

        sciencePeriodText.text = (scienceAddAmount + scienceBonus)                                + " Sp" + " / " +
                                 (ScienceAddTimer  - (scienceEfficiency / 100) * ScienceAddTimer) + "sec";
    }

    private void Update()
    {
        if (_waveSpawner.state == WaveSpawner.SpawnState.WAITING && _waveSpawner.currentWaveCount <= 0) return;

        AddResources();
    }

    private void AddResources()
    {
        if (energyAddTimer > 0f)
            energyAddTimer -= Time.deltaTime;
        else
        {
            energyAddTimer = EnergyAddTimer - EnergyEfficiency;

            EnergyPoints = EnergyPoints + energyAddAmount + EnergyBonus;
            UIUpdater.Instance.UpdateResourceTexts();

            energyTMP.text = "+" + (energyAddAmount + EnergyBonus);
            AnimatorParam.SetAnimatorParameter(energyTextAnimator, AnimatorParameters.RESOURCES_SHOW_POPUP,
                                               AnimatorParameters.AnimatorParameterType.Trigger);

            energyPeriodText.text = (energyAddAmount + EnergyBonus) + " Ep" + " / " + energyAddTimer + "sec";
        }

        if (scienceAddTimer > 0f)
            scienceAddTimer -= Time.deltaTime;
        else
        {
            scienceAddTimer = ScienceAddTimer - ScienceEfficiency;

            SciencePoints = SciencePoints + (scienceAddAmount + ScienceBonus);
            UIUpdater.Instance.UpdateResourceTexts();

            scienceTMP.text = "+" + (scienceAddAmount + ScienceBonus);
            AnimatorParam.SetAnimatorParameter(scienceTextAnimator, AnimatorParameters.RESOURCES_SHOW_POPUP,
                                               AnimatorParameters.AnimatorParameterType.Trigger);

            sciencePeriodText.text = (scienceAddAmount + ScienceBonus) + " Sp" + " / " + scienceAddTimer + "sec";
        }
    }

    #region Upgrade Methods

    public static void IncreaseEnergyBonus(int value)
    {
        if (ResourcesUpgradeController.Instance.bEnergyBonus.interactable == false) return;

        if (EnergyPoints >= PriceController.CalculatePrice(EnergyBonusPrice,
                                                           EnergyBonusUpgradeCount,
                                                           PriceController.Instance.upgradeEnergyBonusPriceMultiplier))
        {
            EnergyPoints -= PriceController.CalculatePrice(EnergyBonusPrice,
                                                           EnergyBonusUpgradeCount,
                                                           PriceController.Instance.upgradeEnergyBonusPriceMultiplier);

            EnergyBonusUpgradeCount++;
            EnergyBonus =
                UpgradeResourcesStatsController.IncreaseResourcesEnergyBonusStat(InitEnergyBonus,
                                                                                 EnergyBonusUpgradeCount);
        }

        if (EnergyBonusUpgradeCount == UpgradeCountController.Instance.ResourcesEnergyBonusUpgradeCount)
            ResourcesUpgradeController.Instance.bEnergyBonus.interactable = false;
    }

    public static void IncreaseEnergyEfficiency(int value)
    {
        if (ResourcesUpgradeController.Instance.bEnergyEfficiency.interactable == false) return;

        if (EnergyPoints >= PriceController.CalculatePrice(EnergyEfficiencyPrice,
                                                           EnergyEfficiencyUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeEnergyEfficiencyPriceMultiplier))
        {
            EnergyPoints -= PriceController.CalculatePrice(EnergyEfficiencyPrice,
                                                           EnergyEfficiencyUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeEnergyEfficiencyPriceMultiplier);

            EnergyEfficiencyUpgradeCount++;
            EnergyEfficiency = Mathf.RoundToInt(InitEnergyEfficiency + (EnergyEfficiencyUpgradeCount * value));
        }

        if (EnergyEfficiencyUpgradeCount == UpgradeCountController.Instance.ResourcesEnergyEfficiencyUpgradeCount)
            ResourcesUpgradeController.Instance.bEnergyEfficiency.interactable = false;
    }

    public static void IncreaseEnergyDropRate(int value)
    {
        if (ResourcesUpgradeController.Instance.bEnergyDropRate.interactable == false) return;

        if (EnergyPoints >= PriceController.CalculatePrice(EnergyDropRatePrice,
                                                           EnergyDropRateUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeEnergyDropRatePriceMultiplier))
        {
            EnergyPoints -= PriceController.CalculatePrice(EnergyDropRatePrice,
                                                           EnergyDropRateUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeEnergyDropRatePriceMultiplier);

            EnergyDropRateUpgradeCount++;
            EnergyDropRate = Mathf.RoundToInt(InitEnergyDropRate + (EnergyDropRateUpgradeCount * value));
        }

        if (EnergyDropRateUpgradeCount == UpgradeCountController.Instance.ResourcesEnergyDropRateUpgradeCount)
            ResourcesUpgradeController.Instance.bEnergyDropRate.interactable = false;
    }

    public static void IncreaseScienceBonus(int value)
    {
        if (ResourcesUpgradeController.Instance.bScienceBonus.interactable == false) return;

        if (EnergyPoints >= PriceController.CalculatePrice(ScienceBonusPrice,
                                                           ScienceBonusUpgradeCount,
                                                           PriceController.Instance.upgradeScienceBonusPriceMultiplier))
        {
            EnergyPoints -= PriceController.CalculatePrice(ScienceBonusPrice,
                                                           ScienceBonusUpgradeCount,
                                                           PriceController.Instance.upgradeScienceBonusPriceMultiplier);

            ScienceBonusUpgradeCount++;
            ScienceBonus =
                UpgradeResourcesStatsController.IncreaseResourcesScienceBonusStat(InitScienceBonus,
                                                                                  ScienceBonusUpgradeCount);
        }

        if (ScienceBonusUpgradeCount == UpgradeCountController.Instance.ResourcesScienceBonusUpgradeCount)
            ResourcesUpgradeController.Instance.bScienceBonus.interactable = false;
    }

    public static void IncreaseScienceEfficiency(int value)
    {
        if (ResourcesUpgradeController.Instance.bScienceEfficiency.interactable == false) return;

        if (EnergyPoints >= PriceController.CalculatePrice(ScienceEfficiencyPrice,
                                                           ScienceEfficiencyUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeScienceEfficiencyPriceMultiplier))
        {
            EnergyPoints -= PriceController.CalculatePrice(ScienceEfficiencyPrice,
                                                           ScienceEfficiencyUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeScienceEfficiencyPriceMultiplier);

            ScienceEfficiencyUpgradeCount++;
            ScienceEfficiency = Mathf.RoundToInt(InitScienceEfficiency + (ScienceEfficiencyUpgradeCount * value));
        }

        if (ScienceEfficiencyUpgradeCount == UpgradeCountController.Instance.ResourcesScienceEfficiencyUpgradeCount)
            ResourcesUpgradeController.Instance.bScienceEfficiency.interactable = false;
    }

    public static void IncreaseScienceDropRate(int value)
    {
        if (ResourcesUpgradeController.Instance.bScienceDropRate.interactable == false) return;

        if (EnergyPoints >= PriceController.CalculatePrice(ScienceDropRatePrice,
                                                           ScienceDropRateUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeScienceDropRatePriceMultiplier))
        {
            EnergyPoints -= PriceController.CalculatePrice(ScienceDropRatePrice,
                                                           ScienceDropRateUpgradeCount,
                                                           PriceController
                                                               .Instance.upgradeScienceDropRatePriceMultiplier);

            ScienceDropRateUpgradeCount++;
            ScienceDropRate = Mathf.RoundToInt(InitScienceDropRate + (ScienceDropRateUpgradeCount * value));
        }

        if (ScienceDropRateUpgradeCount == UpgradeCountController.Instance.ResourcesScienceDropRatUpgradeCounte)
            ResourcesUpgradeController.Instance.bScienceDropRate.interactable = false;
    }

    #endregion

    public void LoadStats()
    {
        EnergyBonus =
            UpgradeResourcesStatsController.IncreaseResourcesEnergyBonusStat(InitEnergyBonus,
                                                                             EnergyBonusUpgradeCount);

        EnergyEfficiency = Mathf.RoundToInt(InitEnergyEfficiency +
                                            (EnergyEfficiencyUpgradeCount *
                                             ResourcesUpgradeController.Instance.fEnergyEfficiency));
        EnergyDropRate = Mathf.RoundToInt(InitEnergyDropRate +
                                          (EnergyDropRateUpgradeCount *
                                           ResourcesUpgradeController.Instance.fEnergyDropRate));

        ScienceBonus =
            UpgradeResourcesStatsController.IncreaseResourcesScienceBonusStat(InitScienceBonus,
                                                                              ScienceBonusUpgradeCount);

        ScienceEfficiency = Mathf.RoundToInt(InitScienceEfficiency +
                                             (ScienceEfficiencyUpgradeCount *
                                              ResourcesUpgradeController.Instance.fScienceEfficiency));
        ScienceDropRate = Mathf.RoundToInt(InitScienceDropRate +
                                           (ScienceDropRateUpgradeCount *
                                            ResourcesUpgradeController.Instance.fScienceDropRate));

        DisableMaxedUpgradeButtons();
    }

    private void DisableMaxedUpgradeButtons()
    {
        if (EnergyBonusUpgradeCount == UpgradeCountController.Instance.ResourcesEnergyBonusUpgradeCount)
            ResourcesUpgradeController.Instance.bEnergyBonus.interactable = false;
        if (EnergyEfficiencyUpgradeCount == UpgradeCountController.Instance.ResourcesEnergyEfficiencyUpgradeCount)
            ResourcesUpgradeController.Instance.bEnergyEfficiency.interactable = false;
        if (EnergyDropRateUpgradeCount == UpgradeCountController.Instance.ResourcesEnergyDropRateUpgradeCount)
            ResourcesUpgradeController.Instance.bEnergyDropRate.interactable = false;
        if (ScienceBonusUpgradeCount == UpgradeCountController.Instance.ResourcesScienceBonusUpgradeCount)
            ResourcesUpgradeController.Instance.bScienceBonus.interactable = false;
        if (ScienceEfficiencyUpgradeCount == UpgradeCountController.Instance.ResourcesScienceEfficiencyUpgradeCount)
            ResourcesUpgradeController.Instance.bScienceEfficiency.interactable = false;
        if (ScienceDropRateUpgradeCount == UpgradeCountController.Instance.ResourcesScienceDropRatUpgradeCounte)
            ResourcesUpgradeController.Instance.bScienceDropRate.interactable = false;
    }

    public void ResetAddTimers()
    {
        EnergyEfficiency = Mathf.RoundToInt(InitEnergyEfficiency);
        ScienceEfficiency = Mathf.RoundToInt(InitScienceEfficiency);
    }
}