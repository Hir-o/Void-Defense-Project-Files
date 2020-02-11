using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private float soundCapResetSpeed = .55f;

    [SerializeField] private int maxSoundsMainTurret       = 10,
                                 maxSoundsSecondaryTurret  = 10,
                                 maxSoundsMortarTurret     = 10,
                                 maxSoundsMainTurretImpact = 3,
                                 maxSoundsSecondaryImpact  = 5,
                                 maxSoundsMortarImpact     = 2;

    private float _timePassed;
    private int   _soundsPlayedMainTurret, _soundsPlayedSecondaryTurret, _soundsPlayedMortar;

    [BoxGroup("Shoot Sounds")] [SerializeField]
    private AudioClip mainTurretProjectileSound,
                      secondaryTurretProjectileSound,
                      mortarTurretProjectileSound;

    [BoxGroup("Impact Sounds")] [SerializeField]
    private AudioClip mainTurretProjectileImpactSound,
                      secondaryTurretProjectileImpactSound,
                      mortarTurretProjectileImpactSound;

    [BoxGroup("Skill Sounds")] [SerializeField]
    private AudioClip overchargeSound,
                      empSound,
                      energyBlastSound;

    [BoxGroup("Turret Deploy Sounds")] [SerializeField]
    private AudioClip secondaryTurretDeploySound,
                      laserDeploySound,
                      mortarDeploySound;

    [BoxGroup("Sound Volumes")] [Range(0, 1f)] [SerializeField]
    private float mainTurretProjectileVolume            = .8f,
                  mainTurretProjectileImpactVolume      = .9f,
                  secondaryTurretProjectileVolume       = .5f,
                  secondaryTurretProjectileImpactVolume = .9f,
                  mortarProjectileVolume                = .75f,
                  mortarProjectileImpactVolume          = .7f,
                  overchargeVolume                      = .8f,
                  empVolume                             = .8f,
                  energyBlastVolume                     = .8f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed > soundCapResetSpeed)
        {
            _soundsPlayedMainTurret = _soundsPlayedSecondaryTurret = _soundsPlayedMortar = 0;
            _timePassed             = 0f;
        }
    }

    public void PlayMainTurretProjectileSound()
    {
        if (Time.timeScale > 1f) return;

        if (_soundsPlayedMainTurret < maxSoundsMainTurret)
        {
            EazySoundManager.PlaySound(mainTurretProjectileSound, mainTurretProjectileVolume);

            _soundsPlayedMainTurret++;
        }
    }

    public void PlaySecondaryTurretProjectileSound()
    {
        if (Time.timeScale > 1f) return;

        if (_soundsPlayedSecondaryTurret < maxSoundsSecondaryTurret)
        {
            EazySoundManager.PlaySound(secondaryTurretProjectileSound, secondaryTurretProjectileVolume);

            _soundsPlayedSecondaryTurret++;
        }
    }

    public void PlayMortarTurretProjectileSound()
    {
        if (Time.timeScale > 1f) return;

        if (_soundsPlayedMortar < maxSoundsMortarTurret)
        {
            EazySoundManager.PlaySound(mortarTurretProjectileSound, mortarProjectileVolume);

            _soundsPlayedMortar++;
        }
    }

    public void PlayMainTurretProjectileImpactSound()
    {
        if (Time.timeScale > 1f) return;

        if (_soundsPlayedMainTurret < maxSoundsMainTurretImpact)
        {
            EazySoundManager.PlaySound(mainTurretProjectileImpactSound, mainTurretProjectileImpactVolume);

            _soundsPlayedMainTurret++;
        }
    }

    public void PlaySecondaryTurretProjectileImpactSound()
    {
        if (Time.timeScale > 1f) return;

        if (_soundsPlayedSecondaryTurret < maxSoundsSecondaryImpact)
        {
            EazySoundManager.PlaySound(secondaryTurretProjectileImpactSound, secondaryTurretProjectileImpactVolume);

            _soundsPlayedSecondaryTurret++;
        }
    }

    public void PlayMortarTurretProjectileImpactSound()
    {
        if (Time.timeScale > 1f) return;

        if (_soundsPlayedMortar < maxSoundsMortarImpact)
        {
            EazySoundManager.PlaySound(mortarTurretProjectileImpactSound, mortarProjectileImpactVolume);

            _soundsPlayedMortar++;
        }
    }

    public void PlayOvercharge()
    {
        if (Time.timeScale > 1f) return;

        EazySoundManager.PlaySound(overchargeSound, overchargeVolume);
    }

    public void PlayEMPBlast()
    {
        if (Time.timeScale > 1f) return;

        EazySoundManager.PlaySound(empSound, empVolume);
    }

    public void PlayEnergyBlast()
    {
        if (Time.timeScale > 1f) return;

        EazySoundManager.PlaySound(energyBlastSound, energyBlastVolume);
    }
    
    public void PlaySecondaryTurretDeploySound()
    {
        if (Time.timeScale > 1f) return;

        EazySoundManager.PlaySound(secondaryTurretDeploySound);
    }
    
    public void PlayLaserDeploySound()
    {
        if (Time.timeScale > 1f) return;

        EazySoundManager.PlaySound(laserDeploySound);
    }
    
    public void PlayMortarDeploySound()
    {
        if (Time.timeScale > 1f) return;

        EazySoundManager.PlaySound(mortarDeploySound);
    }
}