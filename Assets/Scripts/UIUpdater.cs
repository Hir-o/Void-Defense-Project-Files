using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    public static UIUpdater Instance;

    [SerializeField] private TextMeshPro tmpHP;

    [BoxGroup("Overlay Buttons")] [SerializeField]
    private Button empButtonOverlay, secondaryTurretOverlay, laserOverlay, mortarOverlay, energyBlastOverlay;

    [BoxGroup("Lock Images")] [SerializeField]
    private Image empLockImage, secondaryTurretLockImage, laserLockImage, mortarLockImage, energyBlastLockImage;

    [BoxGroup("Skill Title Objects")] [SerializeField]
    private GameObject empTitle, secondaryTurretTitle, laserTitle, mortarTitle, energyBlastTitle;

    [BoxGroup("Skill Panels")] [SerializeField]
    private GameObject empSkillPanel,
                       secondaryTurretSkillPanel,
                       laserSkillPanel,
                       mortarSkillPanel,
                       energyBlastSkillPanel;

    private float roundedHpVal, roundedEpVal, roundedSpVal;

    [BoxGroup("Resource Texts")] [SerializeField]
    private TextMeshProUGUI energyPointsText, sciencePointsText;

    [BoxGroup("Skill Auto Panels")]
    public GameObject empAuto,
                      secondaryTurretsAuto,
                      laserAuto,
                      mortarAuto,
                      energyBlastAuto;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() { tmpHP.text = "HP: " + MainTurretController.Health; }

    public void UpdateHP()
    {
        roundedHpVal = Mathf.Round(MainTurretController.Health * 10.0f) / 10.0f;

        tmpHP.text = "HP: " + roundedHpVal;
    }

    public void UpdateResourceTexts()
    {
        roundedEpVal = Mathf.Round(ResourcesController.EnergyPoints  * 10.0f) / 10.0f;
        roundedSpVal = Mathf.Round(ResourcesController.SciencePoints * 10.0f) / 10.0f;

        energyPointsText.text  = "Ep: " + roundedEpVal;
        sciencePointsText.text = "Sp: " + roundedSpVal;

        MainUpgradeController.Instance.UpdateButtonsAlpha();
        ResourcesUpgradeController.Instance.UpdateButtonsAlpha();
        SkillUpgradeController.Instance.UpdateButtonsAlpha();
    }

    public void UpdateSkillPanel()
    {
        if (SkillUnlocker.IsEMPSkillUnlocked)
        {
            empButtonOverlay.enabled = false;
            empButtonOverlay.gameObject.SetActive(false);
            empButtonOverlay.GetComponentInParent<Button>().enabled = true;
            empLockImage.gameObject.SetActive(false);

            empTitle.SetActive(true);
            empSkillPanel.SetActive(true);
            empAuto.SetActive(true);
        }
        else
        {
            empButtonOverlay.enabled = true;
            empButtonOverlay.gameObject.SetActive(true);
            empButtonOverlay.GetComponentInParent<Button>().enabled = true;
            empLockImage.gameObject.SetActive(true);

            empTitle.SetActive(false);
            empSkillPanel.SetActive(false);
            empAuto.SetActive(false);
        }

        if (SkillUnlocker.IsSecondTurretSkillUnlocked)
        {
            secondaryTurretOverlay.enabled = false;
            secondaryTurretOverlay.gameObject.SetActive(false);
            secondaryTurretOverlay.GetComponentInParent<Button>().enabled = true;
            secondaryTurretLockImage.gameObject.SetActive(false);

            secondaryTurretTitle.SetActive(true);
            secondaryTurretSkillPanel.SetActive(true);
            secondaryTurretsAuto.SetActive(true);
        }
        else
        {
            secondaryTurretOverlay.enabled = true;
            secondaryTurretOverlay.gameObject.SetActive(true);
            secondaryTurretOverlay.GetComponentInParent<Button>().enabled = true;
            secondaryTurretLockImage.gameObject.SetActive(true);

            secondaryTurretTitle.SetActive(false);
            secondaryTurretSkillPanel.SetActive(false);
            secondaryTurretsAuto.SetActive(false);
        }

        if (SkillUnlocker.IsLaserSkillUnlocked)
        {
            laserOverlay.enabled = false;
            laserOverlay.gameObject.SetActive(false);
            laserOverlay.GetComponentInParent<Button>().enabled = true;
            laserLockImage.gameObject.SetActive(false);

            laserTitle.SetActive(true);
            laserSkillPanel.SetActive(true);
            laserAuto.SetActive(true);
        }
        else
        {
            laserOverlay.enabled = true;
            laserOverlay.gameObject.SetActive(true);
            laserOverlay.GetComponentInParent<Button>().enabled = true;
            laserLockImage.gameObject.SetActive(true);

            laserTitle.SetActive(false);
            laserSkillPanel.SetActive(false);
            laserAuto.SetActive(false);
        }

        if (SkillUnlocker.IsMortarSkillUnlocked)
        {
            mortarOverlay.enabled = false;
            mortarOverlay.gameObject.SetActive(false);
            mortarOverlay.GetComponentInParent<Button>().enabled = true;
            mortarLockImage.gameObject.SetActive(false);

            mortarTitle.SetActive(true);
            mortarSkillPanel.SetActive(true);
            mortarAuto.SetActive(true);
        }
        else
        {
            mortarOverlay.enabled = true;
            mortarOverlay.gameObject.SetActive(true);
            mortarOverlay.GetComponentInParent<Button>().enabled = true;
            mortarLockImage.gameObject.SetActive(true);

            mortarTitle.SetActive(false);
            mortarSkillPanel.SetActive(false);
            mortarAuto.SetActive(false);
        }

        if (SkillUnlocker.IsEnergyBlastSkillUnlocked)
        {
            energyBlastOverlay.enabled = false;
            energyBlastOverlay.gameObject.SetActive(false);
            energyBlastOverlay.GetComponentInParent<Button>().enabled = true;
            energyBlastLockImage.gameObject.SetActive(false);

            energyBlastTitle.SetActive(true);
            energyBlastSkillPanel.SetActive(true);
            energyBlastAuto.SetActive(true);
        }
        else
        {
            energyBlastOverlay.enabled = true;
            energyBlastOverlay.gameObject.SetActive(true);
            energyBlastOverlay.GetComponentInParent<Button>().enabled = true;
            energyBlastLockImage.gameObject.SetActive(true);

            energyBlastTitle.SetActive(false);
            energyBlastSkillPanel.SetActive(false);
            energyBlastAuto.SetActive(false);
        }
    }
}