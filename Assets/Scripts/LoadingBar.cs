using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    public GameObject blueScreenPanel;

    public void BlueScreen() { blueScreenPanel.SetActive(true); }
}