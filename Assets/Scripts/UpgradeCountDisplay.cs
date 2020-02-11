using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class UpgradeCountDisplay : MonoBehaviour
{
    [BoxGroup("Font Properties")] [SerializeField]
    private float newFontSize = 35f;
    
    [BoxGroup("Font Properties")] [SerializeField]
    private Color newFontColor;

    private static float NewFontSize;
    private static Color NewFontColor;

    private void Awake()
    {
        NewFontSize = newFontSize;
        NewFontColor = newFontColor;
    }

    public static void UpdateUpgradeCountDisplay(TextMeshProUGUI upgradeCountText, int currentUpgradeCount,
                                                 int maxUpgradeCount)
    {
        upgradeCountText.text     = currentUpgradeCount + "/" + maxUpgradeCount;
        upgradeCountText.fontSize = NewFontSize;
        upgradeCountText.color = NewFontColor;
    }
}