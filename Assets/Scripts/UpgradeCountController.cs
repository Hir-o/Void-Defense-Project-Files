using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class UpgradeCountController : MonoBehaviour
{
    public static UpgradeCountController Instance;

    [BoxGroup("Main Turret")]
    public int MainTurretTotalHealthUpgradeCount,
               MainTurretProjectileDamageUpgradeCount,
               MainTurretProjectileThrustUpgradeCount,
               MainTurretFireRateUpgradeCount,
               MainTurretTurnSpeedUpgradeCount,
               MainTurretRangeUpgradeCount,
               MainTurretRegenUpgradeCount,
               MainTurretLifestealUpgradeCount,
               MainTurretDefenseUpgradeCount,
               MainTurretBlockChanceUpgradeCount,
               MainTurretBounceChanceUpgradeCount,
               MainTurretBounceAmountUpgradeCount,
               MainTurretCritChanceUpgradeCount,
               MainTurretCritDamageUpgradeCount;

    [BoxGroup("Resources")]
    public int ResourcesEnergyBonusUpgradeCount,
               ResourcesEnergyEfficiencyUpgradeCount,
               ResourcesEnergyDropRateUpgradeCount,
               ResourcesScienceBonusUpgradeCount,
               ResourcesScienceEfficiencyUpgradeCount,
               ResourcesScienceDropRatUpgradeCounte;

    [BoxGroup("Skills - Overcharge")]
    public int SkillOverchargeCoolDownUpgradeCount,
               SkillOverchargeActiveTimeUpgradeCount,
               SkillOverchargePowerupUpgradeCount;
    
    [BoxGroup("Skills - EMP")]
    public int SkillEMPCoolDownUpgradeCount,
               SkillEMPStunDurationUpgradeCount;
    
    [BoxGroup("Skills - Secondary Turrets")]
    public int SkillSecondaryTurretsCoolDownUpgradeCount,
               SkillSecondaryTurretsActiveTimeUpgradeCount,
               SkillSecondaryTurretsDamageUpgradeCount,
               SkillSecondaryTurretsProjectileThrustUpgradeCount,
               SkillSecondaryTurretsFireRateUpgradeCount,
               SkillSecondaryTurretsTurnSpeedUpgradeCount,
               SkillSecondaryTurretsRangeUpgradeCount;
    
    [BoxGroup("Skills - Laser")]
    public int SkillLaserCoolDownUpgradeCount,
               SkillLaserActiveTimeUpgradeCount,
               SkillLaserDamageUpgradeCount,
               SkillLaserTurnSpeedUpgradeCount,
               SkillLaserRangeUpgradeCount;
    
    [BoxGroup("Skills - Mortar")]
    public int SkillMortarCoolDownUpgradeCount,
               SkillMortarActiveTimeUpgradeCount,
               SkillMortarDamageUpgradeCount,
               SkillMortarFireRateUpgradeCount,
               SkillMortarRangeUpgradeCount;
    
    [BoxGroup("Skills - Energy Blast")]
    public int SkillEnergyBlastCoolDownUpgradeCount,
               SkillEnergyBlastDamageUpgradeCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}