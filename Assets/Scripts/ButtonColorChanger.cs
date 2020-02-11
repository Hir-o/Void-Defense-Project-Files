using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChanger : MonoBehaviour
{
    private static Color      tempColor;
    private static ColorBlock colors;

    [Range(0, 1)] [SerializeField] private float lowerAlphaValue = .7f;

    private static float LowerAlphaValue;

    private void Awake() { LowerAlphaValue = lowerAlphaValue; }

    public static void UpateButtonAlpha(Button button, float resourcePoints, float upgpradePrice, int upgradeCount,
                                        float multiplier)
    {
        colors = button.colors;

        if (resourcePoints <
            PriceController.CalculatePrice(upgpradePrice,
                                           upgradeCount,
                                           multiplier))
        {
            colors             = button.colors;
            tempColor          = colors.normalColor;
            tempColor.a        = LowerAlphaValue;
            colors.normalColor = tempColor;
            button.colors      = colors;
        }
        else
        {
            colors             = button.colors;
            tempColor          = colors.normalColor;
            tempColor.a        = 1f;
            colors.normalColor = tempColor;
            button.colors      = colors;
        }
    }
}