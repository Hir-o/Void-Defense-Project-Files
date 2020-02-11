using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    private EnemyHealth _enemyHealth;

    [SerializeField] private float maxSpeed = 6f, minSpeed = 3f, stoppingDistance = .8f, stoppingDistanceFactor = .5f;
    private                  float _currentSpeed;

    private Vector3 _targetPos, _flatTargetPos;

    private void Start()
    {
        // Stopping distance factor is used to add an offset for how near the enemy should be to hit the main turret.
        stoppingDistance = EnemyController.StoppingDistance + stoppingDistanceFactor;
        _enemyHealth     = GetComponent<EnemyHealth>();
        _currentSpeed    = Random.Range(minSpeed, maxSpeed);

        _targetPos     = ObjectReferenceHolder.Instance.mainTurret.transform.position;
        _flatTargetPos = new Vector3(_targetPos.x, transform.position.y, _targetPos.z);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, ObjectReferenceHolder.Instance.mainTurret.transform.position) <=
            stoppingDistance)
            _enemyHealth.HitMainTurret();
        else
        {
//            transform.position = Vector3.MoveTowards(transform.position,
//                ObjectReferenceHolder.Instance.mainTurret.transform.position, _currentSpeed * Time.deltaTime);

            transform.LookAt(_flatTargetPos);
        }
    }
    
    
}