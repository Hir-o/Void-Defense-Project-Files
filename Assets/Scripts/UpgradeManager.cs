using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [ShowNonSerializedField]
    public static float TotalHealthPrice,
                        ProjectileDamagePrice,
                        TurnSpeedPrice,
                        RangePrice,
                        RegenPrice,
                        LifestealPrice,
                        DefensePrice,
                        BlockChancePrice,
                        BounceChancePrice,
                        BounceAmountPrice,
                        CritChancePrice,
                        CritDamagePrice;

    [ShowNonSerializedField]
    public static int TotalHealthUpgradeCount      = 1,
                      ProjectileDamageUpgradeCount = 1,
                      TurnSpeedUpgradeCount        = 1,
                      RangeUpgradeCount            = 1,
                      RegenUpgradeCount            = 1,
                      LifestealUpgradeCount        = 1,
                      DefenseUpgradeCount          = 1,
                      BlockChanceUpgradeCount      = 1,
                      BounceChanceUpgradeCount     = 1,
                      BounceAmountUpgradeCount     = 1,
                      CritChanceUpgradeCount       = 1,
                      CritDamageUpgradeCount       = 1;
}
