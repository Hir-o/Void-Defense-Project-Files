using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kino;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver;

    private List<GameObject> _allEnemies, _descriptionPanelElements;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _allEnemies = ObjectReferenceHolder.Instance.allEnemies;

        _descriptionPanelElements = ObjectReferenceHolder.Instance.descriptionPanelObjects;
    }

    public void EndGame()
    {
        if (isGameOver) return;

        isGameOver = true;

        ObjectReferenceHolder.Instance.canvas.SetActive(false);
        ObjectReferenceHolder.Instance.menuPanel.SetActive(false);
        ObjectReferenceHolder.Instance.waveSpawner.StopSpawningWave();
        Tooltip.HideTooltip_static();

        foreach (GameObject enemy in _allEnemies)
            if (enemy.activeSelf)
                enemy.GetComponent<EnemyHealth>().Kill();

        foreach (GameObject element in _descriptionPanelElements)
            if (element.activeSelf)
                element.SetActive(false);

        DOVirtual.Float(0f, .4f,  .25f, DigitalGlitchIntensity);
        DOVirtual.Float(0f, .1f,  .25f, AnalogGlitchScanLineJitter);
        DOVirtual.Float(0f, .15f, .25f, AnalogGlitchVerticalJump);
        DOVirtual.Float(0f, .1f,  .25f, AnalogGlitchHorizontalShake);
        DOVirtual.Float(0f, .4f,  .25f, AnalogGlitchColorDrift);

        Invoke(nameof(DisplayBlueScreen), 1f);
    }

    public void ResetGame()
    {
        isGameOver = false;

        MainTurretController.Health = MainTurretController.TotalHealth;
        UIUpdater.Instance.UpdateHP();

        ObjectReferenceHolder.Instance.canvas.SetActive(true);
        ObjectReferenceHolder.Instance.waveSpawner.nextWave--;
        ObjectReferenceHolder.Instance.waveSpawner.ResetCurrentWaveText();

        DigitalGlitch.Instance.intensity      = 0f;
        AnalogGlitch.Instance.scanLineJitter  = 0f;
        AnalogGlitch.Instance.verticalJump    = 0f;
        AnalogGlitch.Instance.horizontalShake = 0f;
        AnalogGlitch.Instance.colorDrift      = 0f;

        DissolveShaderController.Instance.MakeWorldVisible();
    }

    private void DisplayBlueScreen()
    {
        ObjectReferenceHolder.Instance.canvas.SetActive(true);
        ObjectReferenceHolder.Instance.blueScreenPanel.SetActive(true);

        EnemyStatsPanel.Instance.HideEnemyPanel();
        SkillStatsPanel.Instance.HideStatPanel();

        DissolveShaderController.Instance.MakeWorldInvisible();
    }

    public void DisableBlueScreen()
    {
        ObjectReferenceHolder.Instance.blueScreenPanel.SetActive(false);
        ObjectReferenceHolder.Instance.waveSpawner.ResetLastWave();
        ResetGame();
    }

    private void DigitalGlitchIntensity(float value)      { DigitalGlitch.Instance.intensity      = value; }
    private void AnalogGlitchScanLineJitter(float value)  { AnalogGlitch.Instance.scanLineJitter  = value; }
    private void AnalogGlitchVerticalJump(float value)    { AnalogGlitch.Instance.verticalJump    = value; }
    private void AnalogGlitchHorizontalShake(float value) { AnalogGlitch.Instance.horizontalShake = value; }
    private void AnalogGlitchColorDrift(float value)      { AnalogGlitch.Instance.colorDrift      = value; }

    public void SetGameSpeed(float value)
    {
        Time.timeScale = value;

        if (Time.timeScale > 1.5f)
        {
            ObjectReferenceHolder.Instance.tmpX0.font = ObjectReferenceHolder.Instance.fontAssetWhite;
            ObjectReferenceHolder.Instance.tmpX1.font = ObjectReferenceHolder.Instance.fontAssetWhite;
            ObjectReferenceHolder.Instance.tmpX2.font = ObjectReferenceHolder.Instance.fontAssetBlue;

            ObjectReferenceHolder.Instance.tmpX0.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialWhite;
            ObjectReferenceHolder.Instance.tmpX1.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialWhite;
            ObjectReferenceHolder.Instance.tmpX2.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialBlue;
        }
        else if (Time.timeScale <= 1.5f)
        {
            ObjectReferenceHolder.Instance.tmpX0.font = ObjectReferenceHolder.Instance.fontAssetWhite;
            ObjectReferenceHolder.Instance.tmpX1.font = ObjectReferenceHolder.Instance.fontAssetBlue;
            ObjectReferenceHolder.Instance.tmpX2.font = ObjectReferenceHolder.Instance.fontAssetWhite;

            ObjectReferenceHolder.Instance.tmpX0.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialWhite;
            ObjectReferenceHolder.Instance.tmpX1.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialBlue;
            ObjectReferenceHolder.Instance.tmpX2.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialWhite;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        
        ObjectReferenceHolder.Instance.tmpX0.font = ObjectReferenceHolder.Instance.fontAssetBlue;
        ObjectReferenceHolder.Instance.tmpX1.font = ObjectReferenceHolder.Instance.fontAssetWhite;
        ObjectReferenceHolder.Instance.tmpX2.font = ObjectReferenceHolder.Instance.fontAssetWhite;
        
        ObjectReferenceHolder.Instance.tmpX0.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialBlue;
        ObjectReferenceHolder.Instance.tmpX1.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialWhite;
        ObjectReferenceHolder.Instance.tmpX2.fontSharedMaterial = ObjectReferenceHolder.Instance.fontMaterialWhite;
    }
}