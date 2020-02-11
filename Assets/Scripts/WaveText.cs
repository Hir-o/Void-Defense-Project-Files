using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    public static WaveText Instance;

    [SerializeField] private float typeTimer = .2f;

    [SerializeField] private TextMeshProUGUI waveText;

    private WaveSpawner _waveSpawner;

    private string _textToWrite, _tempText;

    private char[] charArr;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() { _waveSpawner = ObjectReferenceHolder.Instance.waveSpawner; }

    public void DisplayWaveText()
    {
        StopAllCoroutines();
        waveText.text = String.Empty;
        
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        _textToWrite = "WAVE ";

        charArr = _textToWrite.ToCharArray();

        foreach (char ch in charArr)
        {
            waveText.text = _tempText + "_";

            yield return new WaitForSecondsRealtime(typeTimer);

            _tempText += ch;
        }

        _tempText += (_waveSpawner.nextWave + 1);
        
        waveText.text = _tempText + "_";

        StartCoroutine(DeleteText());
    }

    private IEnumerator DeleteText()
    {
        yield return new WaitForSecondsRealtime(1f);
        
        charArr = _tempText.ToCharArray();

        foreach (char ch in charArr)
        {
            _tempText     = _tempText.Substring(0, _tempText.Length - 1);
            waveText.text = _tempText + "_";

            yield return new WaitForSecondsRealtime(typeTimer);
        }

        waveText.text = String.Empty;
    }
}