using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTagChanger : MonoBehaviour
{
    [SerializeField] private GameObject enemyBody;

    private void OnEnable() { enemyBody.tag = Tags.ENEMY_TAG; }

    public void ChangeEnemyTag() { enemyBody.tag = Tags.DEAD_ENEMY_TAG; }
}