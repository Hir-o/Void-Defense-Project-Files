using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkillsStatsController : MonoBehaviour
{
    [SerializeField] private float upgradeEnergyBlastDamageMultiplier = 1.35f;
    
    public static float UpgradeEnergyBlastDamageMultiplier;

    private void Awake()
    {
        UpgradeEnergyBlastDamageMultiplier = upgradeEnergyBlastDamageMultiplier;
    }

    public static float IncreaseEnergyBlastDamageStat(float initUpgrade, float upgradeCount)
    {
        return initUpgrade *
               Mathf.Pow(UpgradeEnergyBlastDamageMultiplier, upgradeCount);
    }
}
