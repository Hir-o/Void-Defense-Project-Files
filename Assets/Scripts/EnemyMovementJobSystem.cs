using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;
using Random = UnityEngine.Random;

public class EnemyMovementJobSystem : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    private TransformAccessArray transformAccessArray;

    [SerializeField] private List<Enemy> enemyList;

    public class Enemy
    {
        public Transform transform;
        public float moveY;
        public bool canMove = true;
    }

    private void Start()
    {
        TransformAccessArray transformAccessArray;
        enemyList = new List<Enemy>();
    }

    public void AddToEnemyList(Transform enemy)
    {
        enemyList.Add(new Enemy
        {
            transform = enemy,
            canMove = true
        });

        enemy.GetComponent<CheckEnemyInList>().isInList = true;
    }

    public void DisableEnemyMovement()
    {
        for (int i = 0; i < enemyList.Count; i++)
            enemyList[i].canMove = false;
        
        Invoke(nameof(EnableEnemyMovement), EnemyController.DisabledTimer + SkillsController.EmpStunDuration);
    }
    
    public void EnableEnemyMovement()
    {
        for (int i = 0; i < enemyList.Count; i++)
            enemyList[i].canMove = true;
        
        foreach (GameObject enemy in ObjectReferenceHolder.Instance.allEnemies)
            enemy.GetComponent<EnemyHealth>().isTargetable = true;
        
        SkillsController.IsEMPEnabled = false;
    }

    private void Update()
    {
        transformAccessArray = new TransformAccessArray(enemyList.Count);

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].canMove)
                transformAccessArray.Add(enemyList[i].transform);
        }

        JobTransform jobTransform = new JobTransform
        {
            deltaTime = Time.deltaTime,
            target = ObjectReferenceHolder.Instance.mainTurret.transform.position,
//            speed = Random.Range(EnemyController.MinSpeed + EnemyController.SpeedFactor,
//                EnemyController.MaxSpeed + EnemyController.SpeedFactor),
            speed = EnemyController.SpeedFactor,
            stoppingDistance = EnemyController.StoppingDistance
        };

        JobHandle jobHandle = jobTransform.Schedule(transformAccessArray);
        jobHandle.Complete();
        transformAccessArray.Dispose();
    }
}

[BurstCompile]
public struct JobTransform : IJobParallelForTransform
{
    public float deltaTime;

    public Vector3 target;
    public Vector3 dirNormalized;

    public Vector3 flattenTargetPos, flattenDirNormalized;

    public float speed, stoppingDistance;

    public void Execute(int index, TransformAccess transform)
    {
        dirNormalized = (target - transform.position).normalized;
        
        flattenDirNormalized = new Vector3(dirNormalized.x, transform.position.y, dirNormalized.z);
        flattenTargetPos = new Vector3(target.x, transform.position.y, target.z);

        if (Vector3.Distance(transform.position, flattenTargetPos) > stoppingDistance)
            transform.position += deltaTime * speed * flattenDirNormalized;
    }
}