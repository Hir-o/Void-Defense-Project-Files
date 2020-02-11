using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class SkillStatsPanel : MonoBehaviour
{
    public static SkillStatsPanel Instance;

    [BoxGroup("Skill Stat Panel")] [SerializeField]
    private GameObject skillStatPanel;

    [BoxGroup("Stat Text")] [SerializeField]
    private TextMeshProUGUI _tmpStat;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowStatOvercharge()
    {
        skillStatPanel.SetActive(true);

        _tmpStat.text = "COOLDOWN: "   + (SkillsController.OverchargeCoolDown   + 1) + "s" + "\n"
                        + "DURATION: " + (SkillsController.OverchargeActiveTime + 1) + "s" + "\n"
                        + "POWERUP: "  + (SkillsController.OverchargePowerup * 100)  + "%";
    }

    public void ShowStatEMP()
    {
        if (SkillUnlocker.IsEMPSkillUnlocked == false) return;

        skillStatPanel.SetActive(true);

        _tmpStat.text = "COOLDOWN: " + (SkillsController.EmpCoolDown + 1) + "s" +
                        "\n"
                        + "STUN DUR: " + (EnemyController.DisabledTimer + SkillsController.EmpStunDuration) + "s";
    }

    public void ShowStatSecondaryTurrets()
    {
        if (SkillUnlocker.IsSecondTurretSkillUnlocked == false) return;

        skillStatPanel.SetActive(true);

        _tmpStat.text = "COOLDOWN: "   + (SecondaryTurretController.CoolDown   + 1) + "s" + "\n"
                        + "DURATION: " + (SecondaryTurretController.ActiveTime + 1) + "s" + "\n"
                        + "DAMAGE: "
                        + Mathf.Round(SecondaryTurretController.ProjectileDamage * 10.0f) / 10.0f + "\n"
                        + "P.SPEED: "                                                             +
                        SecondaryTurretController.ProjectileThrust                                + "\n"
                        + "FIRE RATE: "                                                           +
                        SecondaryTurretController.FireRate                                        + "\n"
                        + "TURN SPEED: "                                                          +
                        SecondaryTurretController.TurnSpeed                                       + "\n"
                        + "RANGE: "                                                               +
                        SecondaryTurretController.Range;
    }

    public void ShowStatLaser()
    {
        if (SkillUnlocker.IsLaserSkillUnlocked == false) return;

        skillStatPanel.SetActive(true);

        _tmpStat.text = "COOLDOWN: "   + (LaserTurretController.CoolDown   + 1) + "s" + "\n"
                        + "DURATION: " + (LaserTurretController.ActiveTime + 1) + "s" + "\n"
                        + "DAMAGE: "
                        + Mathf.Round(LaserTurretController.DamagePerSecond * 10.0f) / 10.0f + "\n"
                        + "TURN SPEED: "                                                     +
                        LaserTurretController.TurnSpeed                                      + "\n"
                        + "RANGE: "                                                          +
                        LaserTurretController.Range;
    }

    public void ShowStatMortar()
    {
        if (SkillUnlocker.IsMortarSkillUnlocked == false) return;

        skillStatPanel.SetActive(true);

        _tmpStat.text = "COOLDOWN: "   + (MortarTurretController.CoolDown   + 1) + "s" + "\n"
                        + "DURATION: " + (MortarTurretController.ActiveTime + 1) + "s" + "\n"
                        + "DAMAGE: "
                        + Mathf.Round(MortarTurretController.ProjectileDamage * 10.0f) / 10.0f + "\n"
                        + "FIRE RATE: "                                                        +
                        MortarTurretController.FireRate                                        + "\n"
                        + "RANGE: "                                                            +
                        MortarTurretController.Range;
    }

    public void ShowStatEnergyBlast()
    {
        if (SkillUnlocker.IsEnergyBlastSkillUnlocked == false) return;

        skillStatPanel.SetActive(true);

        _tmpStat.text = "COOLDOWN: " + (SkillsController.EnergyCoolDown + 1) + "s" + "\n"
                        + "DAMAGE: " + Mathf.Round(SkillsController.EnergyBlastDamage * 10.0f) / 10.0f;
    }

    public void HideStatPanel() { skillStatPanel.SetActive(false); }
}