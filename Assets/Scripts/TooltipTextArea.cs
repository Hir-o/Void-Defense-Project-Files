using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class TooltipTextArea : MonoBehaviour
{
    [ResizableTextArea] public string tooltipText;

    private Button thisButton;

    private void Start() { thisButton = GetComponent<Button>(); }

    public void DisplayTooltip()
    {
        if (thisButton.enabled) Tooltip.ShowTooltip_Static(tooltipText);
    }

    public void HIdeTooltip() { Tooltip.HideTooltip_static(); }
}