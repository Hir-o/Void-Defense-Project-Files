using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameController : MonoBehaviour
{
    private void Start()
    {
        if (LoadGameController.IsContinueGame)
            LoadGame();
        else
            DeleteSaveGame();
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("IsGameSaved", 1);

        PlayerPrefs.SetFloat("EnergyPoints",  ResourcesController.EnergyPoints);
        PlayerPrefs.SetFloat("SciencePoints", ResourcesController.SciencePoints);

        PlayerPrefs.SetInt("WaveNumber", ObjectReferenceHolder.Instance.waveSpawner.nextWave);

        SaveMainTurret();
        SaveSecondaryTurrets();
        SaveLaserTurret();
        SaveMortarTurret();
        SaveSkills();
        SaveResources();
    }

    public static void LoadGame()
    {
        ObjectReferenceHolder.Instance.tutorialPanel.SetActive(false);

        if (PlayerPrefs.HasKey("EnergyPoints")) ResourcesController.EnergyPoints = PlayerPrefs.GetFloat("EnergyPoints");

        if (PlayerPrefs.HasKey("SciencePoints"))
            ResourcesController.SciencePoints = PlayerPrefs.GetFloat("SciencePoints");

        if (PlayerPrefs.HasKey("WaveNumber"))
            ObjectReferenceHolder.Instance.waveSpawner.nextWave = PlayerPrefs.GetInt("WaveNumber");

        LoadMainTurret();
        LoadSecondaryTurrets();
        LoadLaserTurret();
        LoadMortarTurret();
        LoadSkills();
        LoadResources();

        UIUpdater.Instance.UpdateResourceTexts();
        ObjectReferenceHolder.Instance.tmpWaveNumber.text =
            "WAVE " + (ObjectReferenceHolder.Instance.waveSpawner.nextWave + 1);

        LoadStats();
    }

    public static void DeleteSaveGame()
    {
        PlayerPrefs.DeleteAll();
        SkillUnlocker.IsEMPSkillUnlocked          = false;
        SkillUnlocker.IsSecondTurretSkillUnlocked = false;
        SkillUnlocker.IsLaserSkillUnlocked        = false;
        SkillUnlocker.IsMortarSkillUnlocked       = false;
        SkillUnlocker.IsEnergyBlastSkillUnlocked  = false;

        MainTurretController.ProjectileDamageUpgradeCount = 0;
        MainTurretController.ProjectileThrustUpgradeCount = 0;
        MainTurretController.FireRateUpgradeCount         = 0;
        MainTurretController.TurnSpeedUpgradeCount        = 0;
        MainTurretController.RangeUpgradeCount            = 0;
        MainTurretController.RegenUpgradeCount            = 0;
        MainTurretController.LifestealUpgradeCount        = 0;
        MainTurretController.DefenseUpgradeCount          = 0;
        MainTurretController.BlockChanceUpgradeCount      = 0;
        MainTurretController.BounceChanceUpgradeCount     = 0;
        MainTurretController.CritChanceUpgradeCount       = 0;
        MainTurretController.CritDamageUpgradeCount       = 0;

        SecondaryTurretController.CoolDownUpgradeCount         = 0;
        SecondaryTurretController.ActiveTimeUpgradeCount       = 0;
        SecondaryTurretController.ProjectileDamageUpgradeCount = 0;
        SecondaryTurretController.ProjectileThrustUpgradeCount = 0;
        SecondaryTurretController.FireRateUpgradeCount         = 0;
        SecondaryTurretController.TurnSpeedUpgradeCount        = 0;
        SecondaryTurretController.RangeUpgradeCount            = 0;

        LaserTurretController.CoolDownUpgradeCount                  = 0;
        LaserTurretController.ActiveTimeUpgradeCount                = 0;
        LaserTurretController.ProjectileDamagePerSecondUpgradeCount = 0;
        LaserTurretController.TurnSpeedUpgradeCount                 = 0;
        LaserTurretController.RangeUpgradeCount                     = 0;

        MortarTurretController.CoolDownUpgradeCount         = 0;
        MortarTurretController.ActiveTimeUpgradeCount       = 0;
        MortarTurretController.ProjectileDamageUpgradeCount = 0;
        MortarTurretController.FireRateUpgradeCount         = 0;
        MortarTurretController.RangeUpgradeCount            = 0;

        SkillsController.OverchargeCoolDownUpgradeCount       = 0;
        SkillsController.OverchargeActiveDurationUpgradeCount = 0;
        SkillsController.OverchargePowerupUpgradeCount        = 0;
        SkillsController.EmpCoolDownUpgradeCount              = 0;
        SkillsController.EmpStunDurationUpgradeCount          = 0;
        SkillsController.EnergyBlastCoolDownUpgradeCount      = 0;
        SkillsController.EnergyBlastDamageUpgradeCount        = 0;

        ResourcesController.EnergyBonusUpgradeCount       = 0;
        ResourcesController.EnergyEfficiencyUpgradeCount  = 0;
        ResourcesController.EnergyDropRateUpgradeCount    = 0;
        ResourcesController.ScienceBonusUpgradeCount      = 0;
        ResourcesController.ScienceEfficiencyUpgradeCount = 0;

        ResourcesController.EnergyBonus  = 0;
        ResourcesController.ScienceBonus = 0;
        ResourcesController.Instance.ResetAddTimers();
        
        MainTurretController.Instance.LoadStats();
        SkillsController.Instance.LoadStats();
        SecondaryTurretController.Instance.LoadStats();
        LaserTurretController.Instance.LoadStats();
        MortarTurretController.Instance.LoadStats();
        ResourcesController.Instance.LoadStats();

        MainUpgradeController.Instance.SetInitialValues();
        SkillUpgradeController.Instance.SetInitialValues();
        ResourcesUpgradeController.Instance.SetInitialValues();

        UIUpdater.Instance.UpdateSkillPanel();
        
        Application.ExternalCall("kongregate.stats.submit", "Waves Completed", 0);
    }

    #region Save Variables

    private static void SaveMainTurret()
    {
        PlayerPrefs.SetInt("MTProjectileDamage",
                           MainTurretController.ProjectileDamageUpgradeCount);
        PlayerPrefs.SetInt("MTProjectileThrust",
                           MainTurretController.ProjectileThrustUpgradeCount);
        PlayerPrefs.SetInt("MTFireRate",     MainTurretController.FireRateUpgradeCount);
        PlayerPrefs.SetInt("MTTurnSpeed",    MainTurretController.TurnSpeedUpgradeCount);
        PlayerPrefs.SetInt("MTRange",        MainTurretController.RangeUpgradeCount);
        PlayerPrefs.SetInt("MTRegen",        MainTurretController.RegenUpgradeCount);
        PlayerPrefs.SetInt("MTLifesteal",    MainTurretController.LifestealUpgradeCount);
        PlayerPrefs.SetInt("MTDefense",      MainTurretController.DefenseUpgradeCount);
        PlayerPrefs.SetInt("MTBlockChance",  MainTurretController.BlockChanceUpgradeCount);
        PlayerPrefs.SetInt("MTBounceChance", MainTurretController.BounceChanceUpgradeCount);
        PlayerPrefs.SetInt("MTCritChance",   MainTurretController.CritChanceUpgradeCount);
        PlayerPrefs.SetInt("MTCritDamage",   MainTurretController.CritDamageUpgradeCount);
    }

    private static void SaveSecondaryTurrets()
    {
        PlayerPrefs.SetInt("STUnlocked", SkillUnlocker.IsSecondTurretSkillUnlocked ? 1 : 0);

        PlayerPrefs.SetInt("STCooldown",         SecondaryTurretController.CoolDownUpgradeCount);
        PlayerPrefs.SetInt("STActiveTime",       SecondaryTurretController.ActiveTimeUpgradeCount);
        PlayerPrefs.SetInt("STProjectileDamage", SecondaryTurretController.ProjectileDamageUpgradeCount);
        PlayerPrefs.SetInt("STProjectileThrust", SecondaryTurretController.ProjectileThrustUpgradeCount);
        PlayerPrefs.SetInt("STFireRate",         SecondaryTurretController.FireRateUpgradeCount);
        PlayerPrefs.SetInt("STTurnSpeed",        SecondaryTurretController.TurnSpeedUpgradeCount);
        PlayerPrefs.SetInt("STRange",            SecondaryTurretController.RangeUpgradeCount);
    }

    private static void SaveLaserTurret()
    {
        PlayerPrefs.SetInt("LUnlocked", SkillUnlocker.IsLaserSkillUnlocked ? 1 : 0);

        PlayerPrefs.SetInt("LCoolDown",   LaserTurretController.CoolDownUpgradeCount);
        PlayerPrefs.SetInt("LActiveTime", LaserTurretController.ActiveTimeUpgradeCount);
        PlayerPrefs.SetInt("LDamage",     LaserTurretController.ProjectileDamagePerSecondUpgradeCount);
        PlayerPrefs.SetInt("LTurnSpeed",  LaserTurretController.TurnSpeedUpgradeCount);
        PlayerPrefs.SetInt("LRange",      LaserTurretController.RangeUpgradeCount);
    }

    private static void SaveMortarTurret()
    {
        PlayerPrefs.SetInt("MUnlocked", SkillUnlocker.IsMortarSkillUnlocked ? 1 : 0);

        PlayerPrefs.SetInt("MCoolDown",   MortarTurretController.CoolDownUpgradeCount);
        PlayerPrefs.SetInt("MActiveTime", MortarTurretController.ActiveTimeUpgradeCount);
        PlayerPrefs.SetInt("MDamage",     MortarTurretController.ProjectileDamageUpgradeCount);
        PlayerPrefs.SetInt("MFireRate",   MortarTurretController.FireRateUpgradeCount);
        PlayerPrefs.SetInt("MRange",      MortarTurretController.RangeUpgradeCount);
    }

    private static void SaveSkills()
    {
        PlayerPrefs.SetInt("EMPUnlocked", SkillUnlocker.IsEMPSkillUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("EBUnlocked",  SkillUnlocker.IsEnergyBlastSkillUnlocked ? 1 : 0);

        PlayerPrefs.SetInt("OCoolDown",   SkillsController.OverchargeCoolDownUpgradeCount);
        PlayerPrefs.SetInt("OActiveTime", SkillsController.OverchargeActiveDurationUpgradeCount);
        PlayerPrefs.SetInt("OPowerUp",    SkillsController.OverchargePowerupUpgradeCount);

        PlayerPrefs.SetInt("EMPCoolDown",     SkillsController.EmpCoolDownUpgradeCount);
        PlayerPrefs.SetInt("EMPStunDuration", SkillsController.EmpStunDurationUpgradeCount);

        PlayerPrefs.SetInt("EBCoolDown", SkillsController.EnergyBlastCoolDownUpgradeCount);
        PlayerPrefs.SetInt("EBDamage",   SkillsController.EnergyBlastDamageUpgradeCount);
    }

    private static void SaveResources()
    {
        PlayerPrefs.SetInt("REnergyBonus",       ResourcesController.EnergyBonusUpgradeCount);
        PlayerPrefs.SetInt("REnergyEfficiency",  ResourcesController.EnergyEfficiencyUpgradeCount);
        PlayerPrefs.SetInt("REnergyDropRate",    ResourcesController.EnergyDropRateUpgradeCount);
        PlayerPrefs.SetInt("RScienceBonus",      ResourcesController.ScienceBonusUpgradeCount);
        PlayerPrefs.SetInt("RScienceEfficiency", ResourcesController.ScienceEfficiencyUpgradeCount);
    }

    #endregion

    #region Load Variables

    private static void LoadMainTurret()
    {
        MainTurretController.ProjectileDamageUpgradeCount = PlayerPrefs.GetInt("MTProjectileDamage");
        MainTurretController.ProjectileThrustUpgradeCount = PlayerPrefs.GetInt("MTProjectileThrust");
        MainTurretController.FireRateUpgradeCount         = PlayerPrefs.GetInt("MTFireRate");
        MainTurretController.TurnSpeedUpgradeCount        = PlayerPrefs.GetInt("MTTurnSpeed");
        MainTurretController.RangeUpgradeCount            = PlayerPrefs.GetInt("MTRange");
        MainTurretController.RegenUpgradeCount            = PlayerPrefs.GetInt("MTRegen");
        MainTurretController.LifestealUpgradeCount        = PlayerPrefs.GetInt("MTLifesteal");
        MainTurretController.DefenseUpgradeCount          = PlayerPrefs.GetInt("MTDefense");
        MainTurretController.BlockChanceUpgradeCount      = PlayerPrefs.GetInt("MTBlockChance");
        MainTurretController.BounceChanceUpgradeCount     = PlayerPrefs.GetInt("MTBounceChance");
        MainTurretController.CritChanceUpgradeCount       = PlayerPrefs.GetInt("MTCritChance");
        MainTurretController.CritDamageUpgradeCount       = PlayerPrefs.GetInt("MTCritDamage");
    }

    private static void LoadSecondaryTurrets()
    {
        SkillUnlocker.IsSecondTurretSkillUnlocked = (PlayerPrefs.GetInt("STUnlocked") == 1) ? true : false;
        UIUpdater.Instance.UpdateSkillPanel();

        SecondaryTurretController.CoolDownUpgradeCount         = PlayerPrefs.GetInt("STCooldown");
        SecondaryTurretController.ActiveTimeUpgradeCount       = PlayerPrefs.GetInt("STActiveTime");
        SecondaryTurretController.ProjectileDamageUpgradeCount = PlayerPrefs.GetInt("STProjectileDamage");
        SecondaryTurretController.ProjectileThrustUpgradeCount = PlayerPrefs.GetInt("STProjectileThrust");
        SecondaryTurretController.FireRateUpgradeCount         = PlayerPrefs.GetInt("STFireRate");
        SecondaryTurretController.TurnSpeedUpgradeCount        = PlayerPrefs.GetInt("STTurnSpeed");
        SecondaryTurretController.RangeUpgradeCount            = PlayerPrefs.GetInt("STRange");
    }

    private static void LoadLaserTurret()
    {
        SkillUnlocker.IsLaserSkillUnlocked = (PlayerPrefs.GetInt("LUnlocked") == 1) ? true : false;
        UIUpdater.Instance.UpdateSkillPanel();

        LaserTurretController.CoolDownUpgradeCount                  = PlayerPrefs.GetInt("LCoolDown");
        LaserTurretController.ActiveTimeUpgradeCount                = PlayerPrefs.GetInt("LActiveTime");
        LaserTurretController.ProjectileDamagePerSecondUpgradeCount = PlayerPrefs.GetInt("LDamage");
        LaserTurretController.TurnSpeedUpgradeCount                 = PlayerPrefs.GetInt("LTurnSpeed");
        LaserTurretController.RangeUpgradeCount                     = PlayerPrefs.GetInt("LRange");
    }

    private static void LoadMortarTurret()
    {
        SkillUnlocker.IsMortarSkillUnlocked = (PlayerPrefs.GetInt("MUnlocked") == 1) ? true : false;
        UIUpdater.Instance.UpdateSkillPanel();

        MortarTurretController.CoolDownUpgradeCount         = PlayerPrefs.GetInt("MCoolDown");
        MortarTurretController.ActiveTimeUpgradeCount       = PlayerPrefs.GetInt("MActiveTime");
        MortarTurretController.ProjectileDamageUpgradeCount = PlayerPrefs.GetInt("MDamage");
        MortarTurretController.FireRateUpgradeCount         = PlayerPrefs.GetInt("MFireRate");
        MortarTurretController.RangeUpgradeCount            = PlayerPrefs.GetInt("MRange");
    }

    private static void LoadSkills()
    {
        SkillUnlocker.IsEMPSkillUnlocked         = (PlayerPrefs.GetInt("EMPUnlocked") == 1) ? true : false;
        SkillUnlocker.IsEnergyBlastSkillUnlocked = (PlayerPrefs.GetInt("EBUnlocked")  == 1) ? true : false;
        UIUpdater.Instance.UpdateSkillPanel();

        SkillsController.OverchargeCoolDownUpgradeCount       = PlayerPrefs.GetInt("OCoolDown");
        SkillsController.OverchargeActiveDurationUpgradeCount = PlayerPrefs.GetInt("OActiveTime");
        SkillsController.OverchargePowerupUpgradeCount        = PlayerPrefs.GetInt("OPowerUp");

        SkillsController.EmpCoolDownUpgradeCount     = PlayerPrefs.GetInt("EMPCoolDown");
        SkillsController.EmpStunDurationUpgradeCount = PlayerPrefs.GetInt("EMPStunDuration");

        SkillsController.EnergyBlastCoolDownUpgradeCount = PlayerPrefs.GetInt("EBCoolDown");
        SkillsController.EnergyBlastDamageUpgradeCount   = PlayerPrefs.GetInt("EBDamage");
    }

    private static void LoadResources()
    {
        ResourcesController.EnergyBonusUpgradeCount       = PlayerPrefs.GetInt("REnergyBonus");
        ResourcesController.EnergyEfficiencyUpgradeCount  = PlayerPrefs.GetInt("REnergyEfficiency");
        ResourcesController.EnergyDropRateUpgradeCount    = PlayerPrefs.GetInt("REnergyDropRate");
        ResourcesController.ScienceBonusUpgradeCount      = PlayerPrefs.GetInt("RScienceBonus");
        ResourcesController.ScienceEfficiencyUpgradeCount = PlayerPrefs.GetInt("RScienceEfficiency");
    }

    #endregion

    private static void LoadStats()
    {
        MainTurretController.Instance.LoadStats();
        MainUpgradeController.Instance.SetInitialValues();
        MainUpgradeController.Instance.ChangeTextForCompletedUpgrades();

        SkillsController.Instance.LoadStats();
        SecondaryTurretController.Instance.LoadStats();
        LaserTurretController.Instance.LoadStats();
        MortarTurretController.Instance.LoadStats();
        SkillUpgradeController.Instance.SetInitialValues();
        SkillUpgradeController.Instance.ChangeTextForCompletedUpgrades();

        ResourcesController.Instance.LoadStats();
        ResourcesUpgradeController.Instance.SetInitialValues();
        ResourcesUpgradeController.Instance.ChangeTextForCompletedUpgrades();
    }
}