using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSecondaryTurretsStatsController : MonoBehaviour
{
    [SerializeField] private float upgradeSecondaryTurretsDamageMultiplier = 1.2f;
    
    public static float UpgradeSecondaryTurretsDamageMultiplier;

    private void Awake()
    {
        UpgradeSecondaryTurretsDamageMultiplier = upgradeSecondaryTurretsDamageMultiplier;
    }

    public static float IncreaseSecondaryTurretsDamageStat(float initUpgrade, float upgradeCount)
    {
        return initUpgrade *
               Mathf.Pow(UpgradeSecondaryTurretsDamageMultiplier, upgradeCount);
    }
}
