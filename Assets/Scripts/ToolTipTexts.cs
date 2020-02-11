using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipTexts : MonoBehaviour
{
    public static ToolTipTexts Instance;
    public string Test = "<color=red>Test</color>";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
