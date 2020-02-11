using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyResourceDrop : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;

    private void OnEnable()
    {
        _textMeshPro.text = "";   
    }

    public void UpdateText(bool isBoss)
    {
        if (isBoss)
            _textMeshPro.text = "+" + (EnemyController.BossRewardPower + ((EnemyController.BossRewardPower / 100) * ResourcesController.EnergyDropRate)) + "\n"
                + "+" + (EnemyController.BossRewardScience + ((EnemyController.BossRewardScience / 100) * ResourcesController.ScienceDropRate));    
        else
            _textMeshPro.text = "+" + (EnemyController.RewardPower + ((EnemyController.RewardPower / 100) * ResourcesController.EnergyDropRate));
        
        transform.LookAt(ObjectReferenceHolder.Instance.mainCamera.transform);
    }
}