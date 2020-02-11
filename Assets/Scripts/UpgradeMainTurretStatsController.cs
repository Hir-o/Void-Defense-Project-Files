using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMainTurretStatsController : MonoBehaviour
{
    [SerializeField] private float upgradeStatMultiplier = 1.2f;

    [SerializeField] private float upgradeMainTurretRegenStatMultiplier = 1.545f;

    public static float UpgradeStatMultiplier;
    public static float UpgradeMainTurretRegenStatMultiplier;

    private void Awake()
    {
        UpgradeStatMultiplier                = upgradeStatMultiplier;
        UpgradeMainTurretRegenStatMultiplier = upgradeMainTurretRegenStatMultiplier;
    }

    public static float IncreaseUpgradeStat(float initUpgrade, float upgradeCount)
    {
        return initUpgrade *
               Mathf.Pow(UpgradeStatMultiplier, upgradeCount);
    }

    public static float IncreaseMainTurretRegenStat(float initUpgrade, float upgradeCount)
    {
        return initUpgrade *
               Mathf.Pow(UpgradeMainTurretRegenStatMultiplier, upgradeCount);
    }
}