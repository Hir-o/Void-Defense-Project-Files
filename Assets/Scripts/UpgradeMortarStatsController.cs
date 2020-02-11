using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMortarStatsController : MonoBehaviour
{
    [SerializeField] private float upgradeMortarDamageMultiplier = 1.48f;
    
    public static float UpgradeMortarDamageMultiplier;

    private void Awake()
    {
        UpgradeMortarDamageMultiplier = upgradeMortarDamageMultiplier;
    }

    public static float IncreaseMortarDamageStat(float initUpgrade, float upgradeCount)
    {
        return initUpgrade *
               Mathf.Pow(UpgradeMortarDamageMultiplier, upgradeCount);
    }
}
