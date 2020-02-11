using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkipWaves : MonoBehaviour
{
    [SerializeField] private TMP_InputField text;

    private int textNumber;

    private WaveSpawner _waveSpawner;

    private float blastRadius = 9999999f;

    private Collider[] hitColliders;

    private void Start() { _waveSpawner = ObjectReferenceHolder.Instance.waveSpawner; }

    public void SkipWaveTo()
    {
        hitColliders = Physics.OverlapSphere(new Vector3(0f, 0f, 0f), blastRadius);

        foreach (Collider coll in hitColliders)
        {
            if (coll.gameObject.CompareTag(Tags.ENEMY_TAG))
                coll.GetComponentInParent<EnemyHealth>().ReduceHealth(99999999f, false);
        }

        int.TryParse(text.text, out textNumber);

        if (textNumber < 1)
            textNumber = 1;
        else if
            (textNumber > 50)
            textNumber = 50;

        _waveSpawner.StopSpawningWave();

        _waveSpawner.nextWave = textNumber - 2;

        ObjectReferenceHolder.Instance.tmpWaveNumber.text = "WAVE " + (_waveSpawner.nextWave + 1);
    }
}