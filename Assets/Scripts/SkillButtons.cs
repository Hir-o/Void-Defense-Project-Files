using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AnimatorParams;
using DG.Tweening;

public class SkillButtons : MonoBehaviour
{
    public static SkillButtons Instance;

    [SerializeField] private Button[] skillButtons;

    [BoxGroup("Overlay Images Colors")]
    public Color originalColor,
                 activeColor,
                 cooldownColor;

    [BoxGroup("Overlay Images")] [Required]
    public Image overchargeOverlay,
                 empOverlay,
                 secondaryTurretOverlay,
                 laserOverlay,
                 mortarOverlay,
                 energyOverlay;

    [BoxGroup("Overlay Materials")]
    public Material overchargeOverlayMaterial,
                    empOverlayMaterial,
                    secondaryTurretOverlayMaterial,
                    laserOverlayMaterial,
                    mortarOverlayMaterial,
                    energyOverlayMaterial;

    [BoxGroup("Active Skill Images")] [Required]
    public GameObject overchargeActiveImg,
                      secondaryTurretActiveImg,
                      laserActiveImg,
                      mortarActiveImg;

    [BoxGroup("TMPro Timers")] [Required] public TextMeshProUGUI tmpOverchargeTimer,
                                                                 tmpEmpTimer,
                                                                 tmpSecondaryTurretTimer,
                                                                 tmpLaserTimer,
                                                                 tmpMortarTimer,
                                                                 tmpEnergyTimer;

    [SerializeField] private Animator skillPanelAnimator;

    [BoxGroup("Active Skills Icons")] [Required]
    public RectTransform activeSkillsPanel, nextWavePanel;

    private float tempPositionVal, originalActiveSkillsPanelYPos, originalNextWavePanelYPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DisableSkillButtons();

        originalActiveSkillsPanelYPos = activeSkillsPanel.anchoredPosition.y;
        originalNextWavePanelYPos     = nextWavePanel.anchoredPosition.y;

        overchargeOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);
        empOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);
        secondaryTurretOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);
        laserOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);
        mortarOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);
        energyOverlayMaterial.SetFloat("_Hologram_Value_1", 0f);
    }

    #region Enable/Disable Skill Buttons

    public void EnableSkillbuttons()
    {
//        AnimatorParam.SetAnimatorParameter(skillPanelAnimator, AnimatorParameters.SKILL_PANEL_CLOSE, false,
//                                           AnimatorParameters.AnimatorParameterType.Bool);
//        AnimatorParam.SetAnimatorParameter(skillPanelAnimator, AnimatorParameters.SKILL_PANEL_OPEN, true,
//                                           AnimatorParameters.AnimatorParameterType.Bool);

        foreach (Button button in skillButtons) button.interactable = true;
    }

    public void DisableSkillButtons()
    {
//        AnimatorParam.SetAnimatorParameter(skillPanelAnimator, AnimatorParameters.SKILL_PANEL_OPEN, false,
//                                           AnimatorParameters.AnimatorParameterType.Bool);
//        AnimatorParam.SetAnimatorParameter(skillPanelAnimator, AnimatorParameters.SKILL_PANEL_CLOSE, true,
//                                           AnimatorParameters.AnimatorParameterType.Bool);

        foreach (Button button in skillButtons) button.interactable = false;
    }

    public void SetNewActiveSkillsPanelPosition()
    {
        activeSkillsPanel.DOAnchorPosY(originalActiveSkillsPanelYPos + 150f, .25f);
    }

    public void ResetActiveSkillsPanelPosition()
    {
        activeSkillsPanel.DOAnchorPosY(originalActiveSkillsPanelYPos, .25f);
    }

    #endregion

    #region Enable/Disable Next Wave Button

    public void SetNewNextWavePanelPosition() { nextWavePanel.DOAnchorPosY(originalNextWavePanelYPos + 200f, .4f); }

    public void ResetNextWavePanelPosition() { nextWavePanel.DOAnchorPosY(originalNextWavePanelYPos, .4f); }

    #endregion
}