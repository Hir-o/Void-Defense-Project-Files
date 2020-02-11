using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeResourcesStatsController : MonoBehaviour
{
    [SerializeField] private float upgradeResourcesEnergyBonusMultiplier  = 1.48f,
                                   upgradeResourcesScienceBonusMultiplier = 1.48f;

    public static float UpgradeResourcesEnergyBonusMultiplier, UpgradeResourcesScienceBonusMultiplier;

    private void Awake()
    {
        UpgradeResourcesEnergyBonusMultiplier  = upgradeResourcesEnergyBonusMultiplier;
        UpgradeResourcesScienceBonusMultiplier = upgradeResourcesScienceBonusMultiplier;
    }

    public static int IncreaseResourcesEnergyBonusStat(float initUpgrade, float upgradeCount)
    {
        return Mathf.RoundToInt(initUpgrade *
                                Mathf.Pow(UpgradeResourcesEnergyBonusMultiplier, upgradeCount));
    }
    
    public static int IncreaseResourcesScienceBonusStat(float initUpgrade, float upgradeCount)
    {
        return Mathf.RoundToInt(initUpgrade *
                                Mathf.Pow(UpgradeResourcesScienceBonusMultiplier, upgradeCount));
    }
}