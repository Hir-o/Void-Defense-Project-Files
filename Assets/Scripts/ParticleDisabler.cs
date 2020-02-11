using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDisabler : MonoBehaviour
{
    private ParticleSystem thisParticle;

    public bool isMainTurret;

    private void Awake() { thisParticle = GetComponent<ParticleSystem>(); }

    public void StopParticle(float stopTime) { StartCoroutine(InitializeStopParticle(stopTime)); }

    public IEnumerator InitializeStopParticle(float stopTime)
    {
        yield return new WaitForSeconds(stopTime);

        if (thisParticle.isPlaying)
            thisParticle.Stop(true);
    }

    public void StopParticle()
    {
        StopAllCoroutines();

        if (thisParticle != null && thisParticle.isPlaying)
            thisParticle.Stop(true);
    }
}