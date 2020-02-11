using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class UIElements : MonoBehaviour
{
    public static UIElements Instance;

    [BoxGroup("Main Upgrade Buttons")] [SerializeField]
    private Button btnDamage,
                   btnFireRate,
                   btnProjectileSpeed,
                   btnTurnSpeed,
                   btnRange,
                   btnRegen,
                   btnLifeSteal,
                   btnDefense,
                   btnBlockChance,
                   btnCritChance,
                   btnCritDamage,
                   btnBounceChance,
                   btnBounceAmmount;

    [BoxGroup("Main Upgrade Cost TMP")] [SerializeField]
    private TextMeshProUGUI tmpDamageCost,
                            tmpFireRateCost,
                            tmpProjectileSpeedCost,
                            tmpTurnSpeedCost,
                            tmpRangeCost,
                            tmpRegenCost,
                            tmpLifeStealCost,
                            tmpDefenseCost,
                            tmpBlockChanceCost,
                            tmpCritChanceCost,
                            tmpCritDamageCost,
                            tmpBounceChanceCost,
                            tmpBounceAmountCost;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}