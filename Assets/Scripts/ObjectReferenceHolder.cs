using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class ObjectReferenceHolder : MonoBehaviour
{
    public static ObjectReferenceHolder Instance; // static instance of ObjectReferenceHolder

    [BoxGroup("Main Camera")]
    public Camera mainCamera; // store instance of main camera

    [BoxGroup("Defensive Structures")]
    public SecondaryTurret secondaryTurret1, secondaryTurret2; // store instances of secondary turrets

    [BoxGroup("Defensive Structures")] public MainTurret   mainTurret;   // store instance of the main turret
    [BoxGroup("Defensive Structures")] public LaserTurret  laserTurret;  //store instance of laser turret
    [BoxGroup("Defensive Structures")] public MortarTurret mortarTurret; // store instance of mortar turret

    [BoxGroup("Defensive Structures")]
    public GameObject mortarTurretChildObject; // store instance of child object of mortar

    [BoxGroup("Turret Animators")]
    public Animator secondaryTurretAnimator_1,
                    secondaryTurretAnimator_2,
                    laserTurretAnimator,
                    mortarTurretAnimator;

    [BoxGroup("Scripts")] public EnemyMovementJobSystem enemyMovementJobSystem; // store enemy job system reference

    [BoxGroup("Scripts")] public WaveSpawner waveSpawner; // store wave spawner reference

    [BoxGroup("Canvas")] public GameObject       canvas;                     // store canvas
    [BoxGroup("Canvas")] public GameObject       blueScreenPanel, menuPanel; // store blue screen panel
    [BoxGroup("Canvas")] public List<GameObject> descriptionPanelObjects;    // store desc panel and its tmpro texts

    [BoxGroup("Enemy Object List")] public List<GameObject> allEnemies = new List<GameObject>();

    [BoxGroup("All Overcharge Particle Disablers")]
    public List<ParticleDisabler> particleDisablers = new List<ParticleDisabler>();

    [BoxGroup("TMP Blocked")]
    public GameObject TMPBlocked;

    [BoxGroup("TMP Current Wave Number")]
    public TextMeshProUGUI tmpWaveNumber;

    [BoxGroup("TMP Enemy Count")]
    public TextMeshProUGUI tmpEnemyCounter;

    [BoxGroup("TMP Game Speed")] [SerializeField]
    public TextMeshProUGUI tmpX0, tmpX1, tmpX2;

    [BoxGroup("TMP Game Speed")]
    public TMP_FontAsset fontAssetWhite,
                         fontAssetBlue,
                         fontAssetRed;

    [BoxGroup("TMP Game Speed")]
    public Material fontMaterialWhite,
                    fontMaterialBlue,
                    fontMaterialRed;

    [BoxGroup("Fade Img Panel")]
    public Animator fadeAnimator;

    [BoxGroup("TMP Auto")] 
    public TextMeshProUGUI tmpOverchargeAuto,
                            tmpEMPAuto,
                            tmpSecondaryTurretsAuto,
                            tmpLaserAuto,
                            tmpMortarAuto,
                            tmpEnergyBlastAuto;

    [BoxGroup("Tutorial Panel")]
    public GameObject tutorialPanel;

    [HideInInspector]
    public LineRenderer
        lineRenderer,
        lineRendererSecondaryTurret1,
        lineRendererSecondaryTurret2,
        lineRendererLaserTurret,
        lineRendererMortarTurret,
        lineRendererMortarTurretChild; // store line renderer of turret

    [HideInInspector]
    public RangeLine
        rangeLineMainTurret,
        rangeLineSecondaryTurret1,
        rangeLineSecondaryTurret2,
        rangeLineLaserTurret,
        rangeLineMortarTurret,
        rangeLineMortarTurretChild; // store range line of turret

    private void Awake()
    {
        if (Instance == null)
            Instance = this; //make a singleton instance of ObjectReferenceHolder
        else
            Destroy(gameObject);

        lineRenderer        = mainTurret.GetComponent<LineRenderer>();
        rangeLineMainTurret = mainTurret.GetComponent<RangeLine>();

        lineRendererSecondaryTurret1 = secondaryTurret1.GetComponent<LineRenderer>();
        rangeLineSecondaryTurret1    = secondaryTurret1.GetComponent<RangeLine>();

        lineRendererSecondaryTurret2 = secondaryTurret2.GetComponent<LineRenderer>();
        rangeLineSecondaryTurret2    = secondaryTurret2.GetComponent<RangeLine>();

        lineRendererLaserTurret = laserTurret.GetComponent<LineRenderer>();
        rangeLineLaserTurret    = laserTurret.GetComponent<RangeLine>();

        lineRendererMortarTurret      = mortarTurret.GetComponent<LineRenderer>();
        rangeLineMortarTurret         = mortarTurret.GetComponent<RangeLine>();
        lineRendererMortarTurretChild = mortarTurretChildObject.GetComponent<LineRenderer>();
        rangeLineMortarTurretChild    = mortarTurretChildObject.GetComponent<RangeLine>();
    }

    public void UpateRangeLines()
    {
        Instance.rangeLineMainTurret.CreatePoints();
        Instance.rangeLineSecondaryTurret1.CreatePoints();
        Instance.rangeLineSecondaryTurret2.CreatePoints();
        Instance.rangeLineLaserTurret.CreatePoints();
        Instance.rangeLineMortarTurret.CreatePoints();
    }
}