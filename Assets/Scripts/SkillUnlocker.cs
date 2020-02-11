using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SkillUnlocker : MonoBehaviour
{
    [ShowNonSerializedField] public static bool IsEMPSkillUnlocked,
                                                IsSecondTurretSkillUnlocked,
                                                IsLaserSkillUnlocked,
                                                IsMortarSkillUnlocked,
                                                IsEnergyBlastSkillUnlocked;

    [BoxGroup("Skill Unlock Values")] public bool isEMPSkillUnlocked,
                                                  isSecondTurretSkillUnlocked,
                                                  isLaserSkillUnlocked,
                                                  isMortarSkillUnlocked,
                                                  isEnergyBlastSkillUnlocked;

    [ShowNonSerializedField] public static float EMPSkillCost,
                                                 SecondaryTurretsSkillCost,
                                                 LaserSkillCost,
                                                 MortarSkillCost,
                                                 EnergyBlastSkillCost;

    [BoxGroup("Skill Cost Values")] public float empSkillCost,
                                                 secondaryTurretsSkillCost,
                                                 laserSkillCost,
                                                 mortarSkillCost,
                                                 energyBlastSkillCost;

    private void Awake()
    {
        EMPSkillCost              = empSkillCost;
        SecondaryTurretsSkillCost = secondaryTurretsSkillCost;
        LaserSkillCost            = laserSkillCost;
        MortarSkillCost           = mortarSkillCost;
        EnergyBlastSkillCost      = energyBlastSkillCost;
    }

    private void Start() { CheckIfSkillsUnlocked(); }

    private void CheckIfSkillsUnlocked() { UIUpdater.Instance.UpdateSkillPanel(); }

    #region Unlock Skill Methods

    public void UnlockEmpBlast(GameObject autoPanel)
    {
        if (ResourcesController.EnergyPoints >= EMPSkillCost)
        {
            ResourcesController.EnergyPoints -= EMPSkillCost;

            IsEMPSkillUnlocked = true;
            CheckIfSkillsUnlocked();

            UIUpdater.Instance.UpdateResourceTexts();
            Tooltip.HideTooltip_static();
        }
    }

    public void UnlockSecondaryTurrets(GameObject autoPanel)
    {
        if (ResourcesController.EnergyPoints >= SecondaryTurretsSkillCost)
        {
            ResourcesController.EnergyPoints -= SecondaryTurretsSkillCost;

            IsSecondTurretSkillUnlocked = true;
            CheckIfSkillsUnlocked();

            UIUpdater.Instance.UpdateResourceTexts();
            Tooltip.HideTooltip_static();
        }
    }

    public void UnlockLaser(GameObject autoPanel)
    {
        if (ResourcesController.EnergyPoints >= LaserSkillCost)
        {
            ResourcesController.EnergyPoints -= LaserSkillCost;

            IsLaserSkillUnlocked = true;
            CheckIfSkillsUnlocked();

            UIUpdater.Instance.UpdateResourceTexts();
            Tooltip.HideTooltip_static();
        }
    }

    public void UnlockMortar(GameObject autoPanel)
    {
        if (ResourcesController.EnergyPoints >= MortarSkillCost)
        {
            ResourcesController.EnergyPoints -= MortarSkillCost;

            IsMortarSkillUnlocked = true;
            CheckIfSkillsUnlocked();

            UIUpdater.Instance.UpdateResourceTexts();
            Tooltip.HideTooltip_static();
        }
    }

    public void UnlockEnergyBlast(GameObject autoPanel)
    {
        if (ResourcesController.EnergyPoints >= EnergyBlastSkillCost)
        {
            ResourcesController.EnergyPoints -= EnergyBlastSkillCost;

            IsEnergyBlastSkillUnlocked = true;
            CheckIfSkillsUnlocked();

            UIUpdater.Instance.UpdateResourceTexts();
            Tooltip.HideTooltip_static();
        }
    }

    #endregion
}