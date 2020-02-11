using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class PreesAnyKey : MonoBehaviour
{
    [SerializeField] private bool isGameOverBlueScreen;
    
    [SerializeField] private TextMeshProUGUI tmpPressKey;

    private void OnEnable() { StartCoroutine(Flash()); }

    private void OnDisable() { StopAllCoroutines(); }

    private void Update()
    {
        if (isGameOverBlueScreen)
        {
            if (Input.anyKey)
                LevelManager.Instance.LoadMainMenu();
        }
        else
        {
            if (Input.anyKey)
                GameManager.Instance.DisableBlueScreen();
        }
    }

    private IEnumerator Flash()
    {
        while (gameObject.activeSelf)
        {
            tmpPressKey.enabled = !tmpPressKey.enabled;

            yield return new WaitForSeconds(1f);
        }
    }
}